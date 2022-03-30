using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Menus;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{


  public abstract class BaseScene : MonoBehaviour
  {
    private GameManager gameManager;

    protected UIManagerBase uiManager;
    [SerializeField] protected Selectable[] tabStops;
    [SerializeField] protected Selectable[] tabStop;

    protected virtual void Start()
    {
      uiManager = FindObjectOfType<UIManagerBase>();
      gameManager = GetGameManager();

      if (gameManager == null || gameManager.GetCommunicator() == null || !Validate())
      {
        ValidateFail();
      } else
      {
        GetData();
      }
    }

    protected GameManager GetGameManager(){
      if(gameManager == null){
        gameManager = FindObjectOfType<GameManager>();
      }
      return gameManager;
    }

    protected virtual bool Validate()
    {
      // By default, we only validate authorization and currentGame.  
      // But this should only fail if the current scene is not Start
      var gameManager = GetGameManager();
      if(SceneManager.GetActiveScene().name == "Start") return true;
      return !string.IsNullOrEmpty(gameManager.GetAuthToken()) && !string.IsNullOrEmpty(gameManager.currentGame);
    }

    protected virtual void ValidateFail() {
      try{
        GetGameManager().LoadScene(SceneName.Start);
      } catch(Exception e){
        Debug.Log("[BaseScene] ValidateFail: " + e.Message);
      }
    }

    public virtual void RequestException(RequestException err)
    {
      Error(UnityEngine.JsonUtility.FromJson<FailResponse>(err.Response).error.message);
    }

    protected virtual void Error(string message)
    {
      GetGameManager().LogMessage("Scene load error:", message);
    }

    protected virtual void GetData() { }
  }
}