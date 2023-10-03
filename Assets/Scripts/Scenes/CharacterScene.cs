
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Openworld.Menus;
using Openworld.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes

{
  public class CharacterScene : BaseScene
  {

    public enum SceneStates { INITIALIZE, NEW_CHARACTER, LOAD_CHARACTER, WAITING, EXIT }
    public Dictionary<SceneStates, List<SceneStates>> validTransitions = new Dictionary<SceneStates, List<SceneStates>>();
    protected StateMachine<SceneStates> stateMachine;
    private GameMenuManager ui;
    protected RacesResponse[] races;

    private void Awake()
    {
      validTransitions.Clear();
      validTransitions.Add(SceneStates.INITIALIZE, new List<SceneStates> { SceneStates.NEW_CHARACTER, SceneStates.LOAD_CHARACTER });
      validTransitions.Add(SceneStates.NEW_CHARACTER, new List<SceneStates> { SceneStates.LOAD_CHARACTER, SceneStates.EXIT });
      validTransitions.Add(SceneStates.LOAD_CHARACTER, new List<SceneStates> { SceneStates.WAITING, SceneStates.EXIT });
      validTransitions.Add(SceneStates.WAITING, new List<SceneStates> { SceneStates.EXIT });
      this.stateMachine = new StateMachine<SceneStates>(validTransitions);
    }

    private void OnEnable()
    {
      stateMachine.OnEnterState += HandleEnterState;
      stateMachine.OnExitState += HandleExitState;
    }

    private void OnDisable()
    {
      stateMachine.OnEnterState -= HandleEnterState;
      stateMachine.OnExitState -= HandleExitState;
    }

    protected override void Start()
    {
      base.Start();
      this.ui = menu as GameMenuManager;
      this.ui.CloseMenu();
      stateMachine.InitializeStateMachine(SceneStates.INITIALIZE);
    }

    private void HandleEnterState(SceneStates previousState, SceneStates newState)
    {
      Debug.Log("Entering state " + newState);
      switch (newState)
      {
        case SceneStates.INITIALIZE:
          // check if we have a character id on the GameManager
          // if not, transition to NEW_CHARACTER
          // otherwise, transition to LOAD_CHARACTER
          if (GetGameManager().GetPlayer().character == null)
          {
            stateMachine.ChangeState(SceneStates.NEW_CHARACTER);
          }
          else
          {
            stateMachine.ChangeState(SceneStates.LOAD_CHARACTER);
          }
          break;
        case SceneStates.NEW_CHARACTER:
          // hide the canvas
          var canvas = FindObjectOfType<Canvas>();
          canvas.enabled = false;
          ui.NewCharacter();
          ui.CreateCharacterSuccess += HandleCreateCharacterSuccess;
          ui.CreateCharacterCancel += HandleCreateCharacterCancel;
          ui.CreateCharacterFail += HandleCreateCharacterFail;
          break;
        case SceneStates.LOAD_CHARACTER:
          // load character data from the server
          GetData();
          break;
        case SceneStates.WAITING:
          // waiting for user input
          break;
        case SceneStates.EXIT:
          // exit the scene
          SceneManager.LoadScene(SceneName.Start.name());
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
      }
    }

    private void HandleExitState(SceneStates previousState, SceneStates newState)
    {
      Debug.Log("Exiting state " + previousState);
      switch (previousState)
      {
        case SceneStates.INITIALIZE:
          break;
        case SceneStates.NEW_CHARACTER:
          ui.CreateCharacterSuccess -= HandleCreateCharacterSuccess;
          ui.CreateCharacterCancel -= HandleCreateCharacterCancel;
          ui.CreateCharacterFail -= HandleCreateCharacterFail;
          break;
        case SceneStates.LOAD_CHARACTER:
          break;
        case SceneStates.WAITING:
          break;
        case SceneStates.EXIT:
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(previousState), previousState, null);
      }
    }

    private void GetData()
    {
      var gameManager = GetGameManager();
      var communicator = gameManager.GetCommunicator();
      var player = gameManager.GetPlayer();

      //get the character detail from the server and put it on the gameManager
      if (!String.IsNullOrEmpty(player.character))
      {
        //add the spinner to the canvas
        var spinner = Instantiate(gameManager.GetSpinner(), FindObjectOfType<Canvas>().transform) as GameObject;
        communicator.GetCharacterDetail(player.character, (CharacterDetailResponse resp) =>
        {
          Destroy(spinner);
          gameManager.character = resp;
          stateMachine.ChangeState(SceneStates.WAITING);
        }, RequestException);
        Debug.Log("fetching character detail");
      }
    }

    private void HandleCreateCharacterSuccess()
    {
      Debug.Log("HandleCreateCharacterSuccess");
      stateMachine.ChangeState(SceneStates.LOAD_CHARACTER);
    }

    private void HandleCreateCharacterCancel()
    {
      Debug.Log("HandleCreateCharacterCancel");
      stateMachine.ChangeState(SceneStates.EXIT);
    }

    private void HandleCreateCharacterFail(Exception e)
    {
      Debug.Log("HandleCreateCharacterFail");
      stateMachine.ChangeState(SceneStates.EXIT);
    }
  }
}