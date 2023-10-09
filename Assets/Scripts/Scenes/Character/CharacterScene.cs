
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
  public enum CharacterSceneStates { INITIALIZE, NEW_CHARACTER, LOAD_CHARACTER, INTERACTIVE, EXIT }

  public class CharacterScene : StatefulScene<CharacterSceneStates, CharacterUI>
  {

    protected RacesResponse[] races;

    protected override Dictionary<CharacterSceneStates, List<CharacterSceneStates>> GetStateTransitions()
    {
      return new Dictionary<CharacterSceneStates, List<CharacterSceneStates>> {
        { CharacterSceneStates.INITIALIZE, new List<CharacterSceneStates> { CharacterSceneStates.NEW_CHARACTER, CharacterSceneStates.LOAD_CHARACTER } },
        { CharacterSceneStates.NEW_CHARACTER, new List<CharacterSceneStates> { CharacterSceneStates.LOAD_CHARACTER, CharacterSceneStates.EXIT } },
        { CharacterSceneStates.LOAD_CHARACTER, new List<CharacterSceneStates> { CharacterSceneStates.INTERACTIVE, CharacterSceneStates.EXIT } },
        { CharacterSceneStates.INTERACTIVE, new List<CharacterSceneStates> { CharacterSceneStates.EXIT } },
      };
    }

    protected override CharacterSceneStates GetInitialState()
    {
      return CharacterSceneStates.INITIALIZE;
    }
    protected override void HandleEnterStateLocal(CharacterSceneStates previousState, CharacterSceneStates newState)
    {
      switch (newState)
      {
        case CharacterSceneStates.INITIALIZE:
          // check if we have a character id on the GameManager
          // if not, transition to NEW_CHARACTER
          // otherwise, transition to LOAD_CHARACTER
          if (GetGameManager().GetPlayer().character == null)
          {
            stateMachine.ChangeState(CharacterSceneStates.NEW_CHARACTER);
          }
          else
          {
            stateMachine.ChangeState(CharacterSceneStates.LOAD_CHARACTER);
          }
          break;
        case CharacterSceneStates.NEW_CHARACTER:
          // hide the canvas
          var canvas = FindObjectOfType<Canvas>();
          canvas.enabled = false;
          ui.NewCharacter();
          ui.CreateCharacterSuccess += HandleCreateCharacterSuccess;
          ui.CreateCharacterCancel += HandleCreateCharacterCancel;
          ui.CreateCharacterFail += HandleCreateCharacterFail;
          break;
        case CharacterSceneStates.LOAD_CHARACTER:
          // load character data from the server
          GetData();
          break;
        case CharacterSceneStates.INTERACTIVE:
          // waiting for user input
          break;
        case CharacterSceneStates.EXIT:
          // exit the scene
          SceneManager.LoadScene(SceneName.Start.name());
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
      }
    }

    protected override void HandleExitStateLocal(CharacterSceneStates previousState, CharacterSceneStates newState)
    {
      switch (previousState)
      {
        case CharacterSceneStates.INITIALIZE:
          break;
        case CharacterSceneStates.NEW_CHARACTER:
          ui.CreateCharacterSuccess -= HandleCreateCharacterSuccess;
          ui.CreateCharacterCancel -= HandleCreateCharacterCancel;
          ui.CreateCharacterFail -= HandleCreateCharacterFail;
          break;
        case CharacterSceneStates.LOAD_CHARACTER:
          break;
        case CharacterSceneStates.INTERACTIVE:
          break;
        case CharacterSceneStates.EXIT:
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
          stateMachine.ChangeState(CharacterSceneStates.INTERACTIVE);
        }, RequestException);
        Debug.Log("fetching character detail");
      }
    }

    private void HandleCreateCharacterSuccess()
    {
      Debug.Log("HandleCreateCharacterSuccess");
      stateMachine.ChangeState(CharacterSceneStates.LOAD_CHARACTER);
    }

    private void HandleCreateCharacterCancel()
    {
      Debug.Log("HandleCreateCharacterCancel");
      stateMachine.ChangeState(CharacterSceneStates.EXIT);
    }

    private void HandleCreateCharacterFail(Exception e)
    {
      Debug.Log("HandleCreateCharacterFail");
      stateMachine.ChangeState(CharacterSceneStates.EXIT);
    }
  }
}