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
        { CharacterSceneStates.NEW_CHARACTER, new List<CharacterSceneStates> { CharacterSceneStates.LOAD_CHARACTER, CharacterSceneStates.INTERACTIVE, CharacterSceneStates.EXIT } },
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
          FindObjectOfType<CharacterCreator>()?.gameObject.SetActive(false);
          // check if we have a character id on the GameManager
          // if not, transition to NEW_CHARACTER
          // otherwise, transition to LOAD_CHARACTER
          if (GetGameManager().GetPlayer().character == null || GetGameManager().GetPlayer().character.Trim().Equals(""))
          {
            stateMachine.ChangeState(CharacterSceneStates.NEW_CHARACTER);
          }
          else
          {
            stateMachine.ChangeState(CharacterSceneStates.LOAD_CHARACTER);
          }
          break;
        case CharacterSceneStates.NEW_CHARACTER:
          var characterCreator = FindObjectOfType<CharacterCreator>(true);
          characterCreator.gameObject.SetActive(true);
          characterCreator.CreateCharacterSuccess += HandleCreateCharacterSuccess;
          characterCreator.CreateCharacterFail += HandleCreateCharacterFail;
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
          var characterCreator = FindObjectOfType<CharacterCreator>();
          characterCreator.gameObject.SetActive(false);
          characterCreator.CreateCharacterSuccess -= HandleCreateCharacterSuccess;
          characterCreator.CreateCharacterFail -= HandleCreateCharacterFail;
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
      }
    }

    private void HandleCreateCharacterSuccess()
    {
      stateMachine.ChangeState(CharacterSceneStates.INTERACTIVE);
    }

    private void HandleCreateCharacterFail(Exception e)
    {
      stateMachine.ChangeState(CharacterSceneStates.EXIT);
    }
  }
}