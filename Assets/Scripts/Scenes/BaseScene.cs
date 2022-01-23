using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{
    public enum SceneName
    {
      LogIn, YourGames, Register, NewGame, Invite, Character, Battles, BattleBoard
    }

  static class SceneNameExtensions
  {
    private static string[] names = new string[] { "login", "games", "register", "newgame", "Invite", "Character", "Battles", "BattleBoard" };
    public static string name(this SceneName scene)
    {
      return names[(int)scene];
    }
  }

  public abstract class BaseScene : MonoBehaviour
  {
    protected GameManager gameManager;
    protected Communicator communicator;
    protected Player player;
    protected virtual bool ShouldValidate { get { return true; } }

    [SerializeField] protected Selectable[] tabStops;

    // Start is called before the first frame update
    protected virtual void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
      if (gameManager != null)
      {
        communicator = gameManager.GetCommunicator();
        player = gameManager.GetPlayer();
      }

      if (gameManager == null || communicator == null || (ShouldValidate && !Validate()))
      {
        ValidateFail();
      }

      GetData();

      FocusGameObject(false);
    }

    protected virtual bool Validate()
    {
      return (player != null && !string.IsNullOrEmpty(player.playerId));
    }

    protected virtual void ValidateFail()
    {
      if (gameManager == null)
      {
        SceneManager.LoadScene(SceneName.LogIn.name());
      } else
      {
        gameManager.LoadScene(SceneName.LogIn);
      }
    }

    void Update()
    {

      if (Input.GetKeyDown(KeyCode.Tab))
      {
        FocusGameObject(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift));
      }
    }

    void FocusGameObject(bool isBackward)
    {
      GameObject selectedObject = EventSystem.current.currentSelectedGameObject;
      bool none = !selectedObject;
      int currentIndex = 0;
      int nextIndex;
      if (!none)
      {
        //find the current tab index
        for (var i = 0; i < tabStops.Length; i++)
        {
          if (tabStops[i].gameObject == selectedObject.gameObject)
          {
            currentIndex = i;
            break;
          }
          // none = true;
        }
      }
      if (none)
      {
        nextIndex = isBackward ? tabStops.Length - 1 : 0;
      }
      else
      {
        nextIndex = (isBackward ? currentIndex - 1 : currentIndex + 1);
        if (nextIndex < 0) nextIndex = tabStops.Length - 1;
        if (nextIndex > tabStops.Length - 1) nextIndex = 0;
      }
      if (tabStops.Length > nextIndex)
      {
        Selectable next = tabStops[nextIndex];
        InputField inputField = next.GetComponent<InputField>();
        if (inputField != null)
        {
          inputField.OnPointerClick(new PointerEventData(EventSystem.current));
        }
        next.Select();
      }
    }

    public virtual void RequestException(RequestException err)
    {
      Error(UnityEngine.JsonUtility.FromJson<ErrorResponse>(err.Response).error.message);
    }

    protected virtual void Error(string message)
    {
      gameManager.LogMessage("Scene load error:", message);
    }

    protected abstract void GetData();
  }
}