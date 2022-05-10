using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Menus;
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
    Start,WorldMap,Character,Battle,Store
  }

  static class SceneNameExtensions
  {
    private static string[] names = new string[] {
      "Start", "WorldMap", "Character.eric", "Battle", "Store"
    };
    public static string name(this SceneName scene)
    {
      return names[(int)scene];
    }
  }

  public class GameManager : MonoBehaviour
  {
    public static GameManager instance;
    [SerializeField] bool localServer;
    [SerializeField] public bool debugApi;
    [SerializeField] Player player;
    [SerializeField] Communicator communicator;
    [SerializeField] string authToken;
    [SerializeField] public string currentGame;
    [SerializeField] public string currentBattle;


    public Player GetPlayer()
    {
      return player;
    }
    public void SetPlayer(PlayerDetailResponse pr)
    {
      player.playerId = pr.id;
      player.playerName = pr.name;
    }

    private CharacterDetail _character;
    public CharacterDetail character{ get; set; }

    public Communicator GetCommunicator()
    {
      return communicator;
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

    public void Start(){
      // Attempting to fix the click handling for UI Toolkit
      // I don't feel real good about this, but the event handling broke with the
      // recent upgrade to the 2021 editor version.  Disabling the PanelRaycaster
      // fixes it, so that the UI Toolkit click events and the gameobject swipe
      // events both work.  Just noting here that at some point this might cause
      // other weird errors, and then we'll have to find a better solution
      var es = FindObjectOfType<EventSystem>();
      es.gameObject.GetComponentInChildren<PanelRaycaster>().enabled = false;

      EventSystem.SetUITookitEventSystemOverride(es, false, false);
      //temporarily forcing dev.  Remove this line to default to prod
      communicator.SetIsDevUrl(this.localServer);
      // temporarily auto logging in.  Remove this line
      Login("eric@heimerman.org", "eric", () => { FindObjectOfType<UIManagerBase>(true).CloseMenu(); }, (RequestException ex) => { });
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
      if(EditorApplication.isPlaying) 
      {
        UnityEditor.EditorApplication.isPlaying = false;
      }
      #endif
    }

    public void Logout(){
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

    public void LogMessage(string message)
    {
      LogMessage("", message);
    }

    // this is here, because we have to do it in two different places
    public void Login(string username, string password, Action FinalSuccess, Action<RequestException> Error){
      communicator.Login(username.Trim(), password, (resp)=> { LoginSuccess(resp, FinalSuccess, Error);}, Error);
    }

    void LoginSuccess(LoginResponse resp, Action FinalSuccess, Action<RequestException> Error){
      SetToken(resp.token);
      communicator.GetPlayerDetail(resp.player, (resp) => { PlayerDetailSuccess(resp, FinalSuccess, Error); }, Error);
    }

    void PlayerDetailSuccess(PlayerDetailResponse resp, Action FinalSuccess, Action<RequestException> Error){
      SetPlayer(resp);
      FinalSuccess();
    }
  }
}
