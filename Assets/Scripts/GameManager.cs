using System;
using Openworld.Binding;
using Openworld.Models;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld
{

  public enum SceneName
  {
    Start, Character, Battle, Inventory, Terrain
  }

  static class SceneNameExtensions
  {
    private static string[] names = new string[] {
      "Start", "Character", "Battle", "Inventory" , "Terrain Generator"
    };
    public static string name(this SceneName scene)
    {
      return names[(int)scene];
    }
  }

  public class GameManager : ObservableMonoBehaviour
  {
    public static GameManager instance;
    [SerializeField] bool localServer;
    [SerializeField] public bool debugApi;
    [SerializeField] Player player;
    [SerializeField] string authToken;
    [SerializeField] public string currentGame;
    [SerializeField] public string currentBattle;
    [SerializeField] GameObject spinnerPrefab;
    [SerializeField] public bool offlineMode;
    [SerializeField] public bool autoLogin;
    [SerializeField] public string autoEmail;
    [SerializeField] public string autoUsername;
    [SerializeField] public string autoPassword;
    private const string prodUrl = "https://www.openworld-game.com";
    private const string devUrl = "http://localhost:3000";
    [SerializeField] public string baseUrl = prodUrl;

    private Communicator _communicator;

    public GameObject GetSpinner()
    {
      return spinnerPrefab;
    }

    public Player GetPlayer()
    {
      return player;
    }
    public void SetPlayer(PlayerDetailResponse pr)
    {
      player.playerId = pr.id;
      player.playerName = pr.username;
    }

    private CharacterDetail _character = new CharacterDetail();
    public CharacterDetail character
    {
      get { return _character; }
      set
      {
        if (_character == null)
        {
          Set(ref _character, value);
          return;
        }
        foreach (var prop in _character.GetType().GetProperties())
        {
          var val = prop.GetValue(value);
          if (val != null && !val.Equals(prop.GetValue(_character)))
          {
            prop.SetValue(_character, val);
          }
        }
      }
    }

    public Communicator GetCommunicator()
    {
      if (_communicator == null)
      {
        if (offlineMode)
        {
          _communicator = FindObjectOfType<OfflineCommunicator>();
        }
        else
        {
          _communicator = FindObjectOfType<Communicator>();
          _communicator.SetUrl(this.localServer ? devUrl : prodUrl);
        }
      }
      return _communicator;
    }
    public string GetAuthToken()
    {
      return authToken;
    }
    public void SetToken(string token)
    {
      authToken = token;
    }

    void Awake()
    {
      if (FindObjectsOfType<GameManager>().Length > 1)
      {
        DestroyAll();
      }
      else
      {
        instance = this;
        DontDestroyOnLoad(gameObject);
      }
    }

    public void Start()
    {
      // Attempting to fix the click handling for UI Toolkit
      // I don't feel real good about this, but the event handling broke with the
      // recent upgrade to the 2021 editor version.  Disabling the PanelRaycaster
      // fixes it, so that the UI Toolkit click events and the gameobject swipe
      // events both work.  Just noting here that at some point this might cause
      // other weird errors, and then we'll have to find a better solution
      var es = FindObjectOfType<EventSystem>();
      es.gameObject.GetComponentInChildren<PanelRaycaster>().enabled = false;

      EventSystem.SetUITookitEventSystemOverride(es, false, false);
    }

    public void Reset()
    {
      DestroyAll();
      SceneManager.LoadScene(SceneName.Start.name());
    }

    void DestroyAll()
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        var child = transform.GetChild(i);
        Destroy(child.gameObject);
      }
      Destroy(gameObject);
    }

    public void CloseApplication()
    {
      // Quit game
      Application.Quit();
#if UNITY_EDITOR
      if (EditorApplication.isPlaying)
      {
        UnityEditor.EditorApplication.isPlaying = false;
      }
#endif
    }

    public void Logout()
    {
      SetPlayer(new PlayerDetailResponse());
      SetToken("");
    }

    public void LogMessage(string title, string message)
    {
#if UNITY_EDITOR
      EditorUtility.DisplayDialog(title, message, "Ok");
#else
      Debug.Log(message);
#endif
    }

    public void LogWarning(string title, string message)
    {
#if UNITY_EDITOR
      EditorUtility.DisplayDialog(title, message, "Ok");
#else
      Debug.LogWarning(message);
#endif
    }

    public void LogError(string title, string message)
    {
#if UNITY_EDITOR
      LogMessage(title, message);
#else
      Debug.LogError(message);
#endif
    }

    public void LogMessage(string message)
    {
      LogMessage("", message);
    }

    /**
    ** Logs in a user, or attempts to register them if the login fails
    */
    public void LoginOrRegister(string email, string password, string username, Action FinalSuccess, Action<RequestException> FinalError)
    {
      Login(username, password, FinalSuccess, (RequestException ex) =>
      {
        GetCommunicator().Register(email, password, username, (ResponseHelper r) =>
        {
          Login(username, password, FinalSuccess, FinalError);
        }, FinalError);
      });
    }

    /**
    **  Logs in a user
    */
    public void Login(string username, string password, Action FinalSuccess, Action<RequestException> FinalError)
    {
      GetCommunicator().Login(username, password, (resp) => LoginSuccess(resp, FinalSuccess, FinalError), FinalError);
    }

    private void LoginSuccess(LoginResponse resp, Action FinalSuccess, Action<RequestException> FinalError)
    {
      SetToken(resp.token);
      GetCommunicator().GetPlayerDetail(resp.player, (resp) => PlayerDetailSuccess(resp, FinalSuccess), FinalError);
    }

    private void PlayerDetailSuccess(PlayerDetailResponse resp, Action FinalSuccess)
    {
      SetPlayer(resp);
      FinalSuccess();
    }
  }
}
