using System.Collections;
using System.Collections.Generic;
using Openworld.Scenes;
using Openworld.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Openworld 
{
  public class GameManager : MonoBehaviour
  {
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


    public static GameManager instance;
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

    public void SetPlayer(PlayerResponse pr)
    {
      player.playerId = pr.id;
      player.playerName = pr.name;
      LoadScene(SceneName.YourGames);
    }

    // Start is called before the first frame update
    void Start()
    {
      //update references to all child objects
      //communicator = FindObjectOfType<Communicator>();
      player = FindObjectOfType<Player>();
    }

    public void Reset()
    {
      DestroyAll();
      LoadScene(SceneName.LogIn);
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
      LogMessage("If running native, application would close now");
      Application.Quit();
    }

    public void LoadScene(SceneName sceneName)
    {
      SceneManager.LoadScene(sceneName.name());
    }

  }
}
