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
    protected GameManager gameManager;
    protected Communicator communicator;
    protected Player player;

    [SerializeField] protected UIManagerBase uiManager;
    [SerializeField] protected Selectable[] tabStops;
    [SerializeField] protected Selectable[] tabStop;

    protected virtual void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
      if (gameManager != null)
      {
        communicator = gameManager.GetCommunicator();
        player = gameManager.GetPlayer();
      }

      if (gameManager == null || communicator == null || !Validate())
      {
        ValidateFail();
      } else
      {
        GetData();
      }
    }

    protected virtual bool Validate()
    {
      return gameManager.GetAuthToken() != null && !gameManager.GetAuthToken().Equals("");
    }

    protected virtual void ValidateFail() {
      gameManager.LoadScene(SceneName.Start);
    }

    public virtual void RequestException(RequestException err)
    {
      Error(UnityEngine.JsonUtility.FromJson<FailResponse>(err.Response).error.message);
    }

    protected virtual void Error(string message)
    {
      gameManager.LogMessage("Scene load error:", message);
    }

    protected virtual void GetData() { }
  }
}