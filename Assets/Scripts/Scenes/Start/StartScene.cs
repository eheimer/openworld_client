using System;
using System.Collections.Generic;
using Openworld.Menus;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Openworld.Scenes
{

  public enum StartSceneStates { INITIALIZE, LOGIN, NEW_USER, LOAD_GAME, NEW_GAME, EXIT }

  public class StartScene : StatefulScene<StartSceneStates, StartUI>
  {
    protected override Dictionary<StartSceneStates, List<StartSceneStates>> GetStateTransitions()
    {
      return new Dictionary<StartSceneStates, List<StartSceneStates>> {
        { StartSceneStates.INITIALIZE, new List<StartSceneStates> { StartSceneStates.LOGIN, StartSceneStates.LOAD_GAME } },
        { StartSceneStates.LOGIN, new List<StartSceneStates> { StartSceneStates.NEW_USER, StartSceneStates.LOAD_GAME } },
        { StartSceneStates.NEW_USER, new List<StartSceneStates> { StartSceneStates.LOAD_GAME, StartSceneStates.LOGIN } },
        { StartSceneStates.LOAD_GAME, new List<StartSceneStates> { StartSceneStates.NEW_GAME, StartSceneStates.EXIT, StartSceneStates.LOGIN } },
        { StartSceneStates.NEW_GAME, new List<StartSceneStates> { StartSceneStates.LOAD_GAME, StartSceneStates.EXIT, StartSceneStates.LOGIN } }
      };
    }

    protected override StartSceneStates GetInitialState()
    {
      return StartSceneStates.INITIALIZE;
    }

    protected override void HandleEnterStateLocal(StartSceneStates previousState, StartSceneStates newState)
    {
      // log the event
      switch (newState)
      {
        case StartSceneStates.INITIALIZE:
          var gm = GetGameManager();
          if (Application.isEditor && gm.autoLogin)
          {
            gm.LoginOrRegister(gm.autoEmail, gm.autoPassword, gm.autoUsername,
              () => stateMachine.ChangeState(StartSceneStates.LOAD_GAME),
              (ex) => stateMachine.ChangeState(StartSceneStates.LOGIN));
          }
          else
          {
            stateMachine.ChangeState(StartSceneStates.LOGIN);
          }
          break;
        case StartSceneStates.LOGIN:
          // show the login panel
          ui.Login();
          // subscribe to the login_success
          ui.LoginSuccess += HandleLoginSuccess;
          ui.LoginFail += HandleLoginFail;
          ui.LoginRegister += HandleLoginRegister;
          break;
        case StartSceneStates.NEW_USER:
          // show the register panel
          ui.Register();
          // subscribe to the register_success and register_fail events
          ui.RegisterSuccess += HandleLoginSuccess;
          ui.RegisterFail += HandleRegisterFail;
          break;
        case StartSceneStates.LOAD_GAME:
          // show the load game panel
          ui.LoadGame();
          //   subscribe to the load_game_success and load_game_fail events
          ui.LoadGameSuccess += HandleLoadGameSuccess;
          ui.LoadGameFail += HandleLoadGameFail;
          ui.LoadGameNewGame += HandleLoadGameNewGame;
          break;
        case StartSceneStates.NEW_GAME:
          // show the new game panel
          ui.NewGame();
          //   subscribe to the new_game_success and new_game_fail events
          ui.NewGameSuccess += HandleNewGameSuccess;
          ui.NewGameFail += HandleNewGameFail;
          break;
        case StartSceneStates.EXIT:
          // load the character scene
          SceneManager.LoadScene(SceneName.Character.name());
          break;
      }
    }

    protected override void HandleExitStateLocal(StartSceneStates previousState, StartSceneStates newState)
    {
      switch (previousState)
      {
        case StartSceneStates.INITIALIZE:
          break;
        case StartSceneStates.LOGIN:
          ui.LoginSuccess -= HandleLoginSuccess;
          ui.LoginFail -= HandleLoginFail;
          ui.LoginRegister -= HandleLoginRegister;
          break;
        case StartSceneStates.NEW_USER:
          ui.RegisterSuccess -= HandleRegisterSuccess;
          ui.RegisterFail -= HandleRegisterFail;
          break;
        case StartSceneStates.LOAD_GAME:
          ui.LoadGameSuccess -= HandleLoadGameSuccess;
          ui.LoadGameFail -= HandleLoadGameFail;
          ui.LoadGameNewGame -= HandleLoadGameNewGame;
          break;
        case StartSceneStates.NEW_GAME:
          ui.NewGameSuccess -= HandleNewGameSuccess;
          ui.NewGameFail -= HandleNewGameFail;
          break;
        case StartSceneStates.EXIT:
          break;
      }
    }

    private void HandleLoginSuccess()
    {
      stateMachine.ChangeState(StartSceneStates.LOAD_GAME);
    }

    private void HandleLoginFail(Exception ex)
    {
      Error("HandleLoginFail", ex.Message);
    }

    private void HandleLoginRegister()
    {
      stateMachine.ChangeState(StartSceneStates.NEW_USER);
    }

    private void HandleRegisterSuccess()
    {
      stateMachine.ChangeState(StartSceneStates.LOGIN);
    }

    private void HandleRegisterFail(Exception ex)
    {
      Error("HandleRegisterFail", ex.Message);
    }

    private void HandleLoadGameSuccess()
    {
      stateMachine.ChangeState(StartSceneStates.EXIT);
    }

    private void HandleLoadGameFail(Exception ex)
    {
      if (ex is NoGamesException || ex is NewGameException)
      {
        stateMachine.ChangeState(StartSceneStates.NEW_GAME);
      }
      Error("HandleLoadGameFail", ex.Message);
    }

    private void HandleLoadGameNewGame()
    {
      stateMachine.ChangeState(StartSceneStates.NEW_GAME);
    }

    private void HandleNewGameSuccess()
    {
      if (GetGameManager().currentGame != null)
      {
        stateMachine.ChangeState(StartSceneStates.EXIT);
      }
      stateMachine.ChangeState(StartSceneStates.LOAD_GAME);
    }

    private void HandleNewGameFail(Exception ex)
    {
      Error("HandleNewGameFail", ex.Message);
    }

  }
}