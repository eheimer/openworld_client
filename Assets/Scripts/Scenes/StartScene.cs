using System;
using System.Collections.Generic;
using Openworld.Menus;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Openworld.Scenes
{
  /*
  ** Now that we have implemented a state machine, our scene manager is essentially
  ** just a state change listener
  */
  public class StartScene : BaseScene
  {
    public enum SceneStates { INITIALIZE, LOGIN, NEW_USER, LOAD_GAME, NEW_GAME, EXIT }
    public Dictionary<SceneStates, List<SceneStates>> validTransitions = new Dictionary<SceneStates, List<SceneStates>>();

    protected StateMachine<SceneStates> stateMachine;

    private StartMenuManager ui;

    private void Awake()
    {
      // initialize the state machine
      validTransitions.Clear();
      validTransitions.Add(SceneStates.INITIALIZE, new List<SceneStates> { SceneStates.LOGIN, SceneStates.LOAD_GAME });
      validTransitions.Add(SceneStates.LOGIN, new List<SceneStates> { SceneStates.NEW_USER, SceneStates.LOAD_GAME });
      validTransitions.Add(SceneStates.NEW_USER, new List<SceneStates> { SceneStates.LOAD_GAME, SceneStates.LOGIN });
      validTransitions.Add(SceneStates.LOAD_GAME, new List<SceneStates> { SceneStates.NEW_GAME, SceneStates.EXIT, SceneStates.LOGIN });
      validTransitions.Add(SceneStates.NEW_GAME, new List<SceneStates> { SceneStates.LOAD_GAME, SceneStates.EXIT, SceneStates.LOGIN });
      this.stateMachine = new StateMachine<SceneStates>(validTransitions);
    }

    private void OnEnable()
    {
      // Subscribe to state machine events
      stateMachine.OnEnterState += HandleEnterState;
      stateMachine.OnExitState += HandleExitState;
    }

    private void OnDisable()
    {
      // Unsubscribe from state machine events to prevent memory leaks
      stateMachine.OnEnterState -= HandleEnterState;
      stateMachine.OnExitState -= HandleExitState;
    }

    protected override void Start()
    {
      base.Start();
      this.ui = menu as StartMenuManager;
      this.ui.CloseMenu();
      stateMachine.InitializeStateMachine(SceneStates.INITIALIZE);
    }

    private void HandleEnterState(SceneStates previousState, SceneStates newState)
    {
      // log the event
      Debug.Log("Entering state " + newState);
      switch (newState)
      {
        case SceneStates.INITIALIZE:
          // check if we have "remember me" enabled and have a player id and token stored
          // if so, try to load the player data
          //   if successful, transition to LOAD_GAME
          //   if unsuccessful, transition to LOGIN
          // if not, transition to LOGIN
          stateMachine.ChangeState(SceneStates.LOGIN);
          break;
        case SceneStates.LOGIN:
          // show the login panel
          ui.Login();
          // subscribe to the login_success
          ui.LoginSuccess += HandleLoginSuccess;
          ui.LoginFail += HandleLoginFail;
          ui.LoginRegister += HandleLoginRegister;
          break;
        case SceneStates.NEW_USER:
          // show the register panel
          ui.Register();
          // subscribe to the register_success and register_fail events
          ui.RegisterSuccess += HandleRegisterSuccess;
          ui.RegisterFail += HandleRegisterFail;
          break;
        case SceneStates.LOAD_GAME:
          // show the load game panel
          ui.LoadGame();
          //   subscribe to the load_game_success and load_game_fail events
          ui.LoadGameSuccess += HandleLoadGameSuccess;
          ui.LoadGameFail += HandleLoadGameFail;
          ui.LoadGameNewGame += HandleLoadGameNewGame;
          break;
        case SceneStates.NEW_GAME:
          // show the new game panel
          ui.NewGame();
          //   subscribe to the new_game_success and new_game_fail events
          ui.NewGameSuccess += HandleNewGameSuccess;
          ui.NewGameFail += HandleNewGameFail;
          break;
        case SceneStates.EXIT:
          // load the character scene
          SceneManager.LoadScene(SceneName.Character.name());
          break;
      }
    }

    private void HandleExitState(SceneStates previousState, SceneStates newState)
    {
      Debug.Log("Exiting state " + previousState);
      switch (previousState)
      {
        case SceneStates.INITIALIZE:
          break;
        case SceneStates.LOGIN:
          ui.LoginSuccess -= HandleLoginSuccess;
          ui.LoginFail -= HandleLoginFail;
          ui.LoginRegister -= HandleLoginRegister;
          break;
        case SceneStates.NEW_USER:
          ui.RegisterSuccess -= HandleRegisterSuccess;
          ui.RegisterFail -= HandleRegisterFail;
          break;
        case SceneStates.LOAD_GAME:
          ui.LoadGameSuccess -= HandleLoadGameSuccess;
          ui.LoadGameFail -= HandleLoadGameFail;
          ui.LoadGameNewGame -= HandleLoadGameNewGame;
          break;
        case SceneStates.NEW_GAME:
          ui.NewGameSuccess -= HandleNewGameSuccess;
          ui.NewGameFail -= HandleNewGameFail;
          break;
        case SceneStates.EXIT:
          break;
      }
    }

    private void HandleLoginSuccess()
    {
      stateMachine.ChangeState(SceneStates.LOAD_GAME);
    }

    private void HandleLoginFail(Exception ex)
    {
      Debug.Log("HandleLoginFail: " + ex.Message);
    }

    private void HandleLoginRegister()
    {
      stateMachine.ChangeState(SceneStates.NEW_USER);
    }

    private void HandleRegisterSuccess()
    {
      stateMachine.ChangeState(SceneStates.LOGIN);
    }

    private void HandleRegisterFail(Exception ex)
    {
      Debug.Log("HandleRegisterFail: " + ex.Message);
    }

    private void HandleLoadGameSuccess()
    {
      // TODO: transition to the character scene
      stateMachine.ChangeState(SceneStates.EXIT);
      SceneManager.LoadScene(SceneName.Character.name());
    }

    private void HandleLoadGameFail(Exception ex)
    {
      Debug.Log("HandleLoadGameFail: " + ex.Message);
    }

    private void HandleLoadGameNewGame()
    {
      stateMachine.ChangeState(SceneStates.NEW_GAME);
    }

    private void HandleNewGameSuccess()
    {
      stateMachine.ChangeState(SceneStates.LOAD_GAME);
    }

    private void HandleNewGameFail(Exception ex)
    {
      Debug.Log("HandleNewGameFail: " + ex.Message);
    }

  }
}