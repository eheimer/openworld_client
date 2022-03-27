using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Openworld.Scenes;
using Proyecto26;
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

    public void SetPlayer(PlayerDetailResponse pr)
    {
      player.playerId = pr.id;
      player.playerName = pr.name;
    }

    // Start is called before the first frame update
    void Start()
    {
      //player = FindObjectOfType<Player>();
      //temporarily forcing dev.  Remove this line to default to prod
      communicator.SetIsDevUrl(true);
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

    public void Logout(){
      player = new Player();
      SetToken("");
    }

    // this is here, because we have to do it in two different places
    public void Login(string username, string password, Action FinalSuccess, Action<RequestException> Error){
      communicator.Login(username, password, (resp)=> { LoginSuccess(resp, FinalSuccess, Error);}, Error);
    }

    void LoginSuccess(LoginResponse resp, Action FinalSuccess, Action<RequestException> Error){
      Debug.Log("Login success: " + resp);
      SetToken(resp.token);
      communicator.GetPlayerDetail(resp.player, (resp) => { PlayerDetailSuccess(resp, FinalSuccess, Error); }, Error);
    }

    void PlayerDetailSuccess(PlayerDetailResponse resp, Action FinalSuccess, Action<RequestException> Error){
      SetPlayer(resp);
      FinalSuccess();
    }
  }
}
