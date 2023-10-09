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
  /**
  ** BaseScene is a base class for scenes that have a UI (basically every scene)
  */
  public abstract class BaseScene<T> : MonoBehaviour where T : UIBase
  {
    private GameManager gameManager;

    [SerializeField] protected Selectable[] tabStops;
    [SerializeField] protected Selectable[] tabStop;
    protected MenuButton menuButton;
    protected T ui;

    private void OnEnable()
    {
      // this check is here to ensure that the start scene is always loaded first to initialize the game manager
      if (GetGameManager() == null)
      {
        SceneManager.LoadScene(SceneName.Start.name());
      }
    }

    protected virtual void Start()
    {
      menuButton = FindObjectOfType<MenuButton>();
      ui = FindObjectOfType<T>(true);
      gameManager = GetGameManager();
      ui.CloseMenu();

      if (!Validate())
      {
        ValidateFail();
      }
      // else
      // {
      //   menu.CloseMenu();
      //   GetData();
      // }
    }

    protected GameManager GetGameManager()
    {
      if (gameManager == null)
      {
        gameManager = FindObjectOfType<GameManager>();
      }
      return gameManager;
    }

    public UIDocument GetMenuButton()
    {
      return menuButton.GetComponent<UIDocument>();
    }

    protected virtual bool Validate()
    {
      try
      {
        if (SceneManager.GetActiveScene().name == "Start") return true;
        // If we're on any scene other than Start, we should absolutely have an auth token and a current game.
        return !string.IsNullOrEmpty(gameManager.GetAuthToken()) && !string.IsNullOrEmpty(gameManager.currentGame);
      }
      catch (Exception e)
      {
        // if anything inside the try fails, validation fails
        Warn("Validate", e.Message);
        return false;
      }
    }

    protected virtual void ValidateFail()
    {
      try
      {
        SceneManager.LoadScene(SceneName.Start.name());
        Info("ValidateFail", "Validation failed");
      }
      catch (Exception e)
      {
        Error("ValidateFail", e.Message);
      }
    }

    public virtual void RequestException(RequestException err)
    {
      Error("RequestException", UnityEngine.JsonUtility.FromJson<FailResponse>(err.Response).error.message);
    }

    protected void Info(string method, string message) { Log(LogType.Info, method, message); }
    protected void Warn(string method, string message) { Log(LogType.Warning, method, message); }
    protected void Error(string method, string message) { Log(LogType.Error, method, message); }

    private void Log(LogType logType, string method, string message)
    {
      switch (logType)
      {
        case LogType.Info:
          if (GetGameManager() != null)
          {
            GetGameManager().LogMessage("[" + this.GetType().Name + "] [" + method + "]:", message);
          }
          else
          {
            Debug.Log("[" + this.GetType().Name + "] [" + method + "]: " + message);
          }
          break;
        case LogType.Warning:
          if (GetGameManager() != null)
          {
            GetGameManager().LogWarning("[" + this.GetType().Name + "] [" + method + "] warning: ", message);
          }
          else
          {
            Debug.LogWarning("[" + this.GetType().Name + "] [" + method + "] warning: " + message);
          }
          break;
        case LogType.Error:
          if (GetGameManager() != null)
          {
            GetGameManager().LogError("[" + this.GetType().Name + "] [" + method + "] error: ", message);
          }
          else
          {
            Debug.LogError("[" + this.GetType().Name + "] [" + method + "] error: " + message);
          }
          break;
      }
    }

    enum LogType
    {
      Info,
      Warning,
      Error
    }

    // protected virtual void GetData() { }
  }
}