using System;
using Openworld.Menus;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace Openworld.Scenes
{


  public abstract class BaseScene : MonoBehaviour
  {
    private GameManager gameManager;

    [SerializeField] protected Selectable[] tabStops;
    [SerializeField] protected Selectable[] tabStop;
    protected MenuButton menuButton;
    protected UIManagerBase menu;

    protected virtual void Start()
    {
      menuButton = FindObjectOfType<MenuButton>();
      menu = FindObjectOfType<UIManagerBase>(true);
      gameManager = GetGameManager();

      if (gameManager == null || gameManager.GetCommunicator() == null || !Validate())
      {
        ValidateFail();
      } else
      {
        menu.CloseMenu();
        GetData();
      }
    }

    protected GameManager GetGameManager(){
      if(gameManager == null){
        gameManager = FindObjectOfType<GameManager>();
      }
      return gameManager;
    }

    public void ShowMenuButton()
    {
      menuButton.GetComponent<UIDocument>().rootVisualElement.visible = true;
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
        SceneManager.LoadScene(SceneName.Start.name());
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