using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class StartUI : UIBase
  {

    [SerializeField]
    FormBase loginForm;
    [SerializeField]
    FormBase registerForm;
    [SerializeField]
    FormBase loadGameForm;
    [SerializeField]
    FormBase newGameForm;

    public event Action LoginSuccess;
    public event Action<Exception> LoginFail;
    public event Action LoginRegister;

    public event Action RegisterSuccess;
    public event Action<Exception> RegisterFail;

    public event Action LoadGameSuccess;
    public event Action<Exception> LoadGameFail;
    public event Action LoadGameNewGame;

    public event Action NewGameSuccess;
    public event Action<Exception> NewGameFail;

    public void Login()
    {
      // show the login panel
      loginForm.Show();
      loginForm.FormSuccess += RaiseLoginSuccess;
      loginForm.FormFail += RaiseLoginFail;
    }

    public void Register()
    {
      // show the register panel
      registerForm.Show();
      registerForm.FormSuccess += RaiseRegisterSuccess;
      registerForm.FormFail += RaiseRegisterFail;
    }

    public void LoadGame()
    {
      // show the load game panel
      loadGameForm.Show();
      loadGameForm.FormSuccess += RaiseLoadGameSuccess;
      loadGameForm.FormFail += RaiseLoadGameFail;
    }

    public void NewGame()
    {
      // show the new game panel
      newGameForm.Show();
      newGameForm.FormSuccess += RaiseNewGameSuccess;
      newGameForm.FormFail += RaiseNewGameFail;
    }

    private void RaiseLoginSuccess()
    {
      Debug.Log("[StartMenuManager] RaiseLoginSuccess");
      RaiseEvent(LoginSuccess);
    }

    private void RaiseLoginFail(Exception ex)
    {
      Debug.Log("[StartMenuManager] RaiseLoginFail: " + ex.Message);
      // if ex is of type LoginRegisterException, then we want to raise the LoginRegister event
      if (ex is LoginRegisterException)
      {
        RaiseEvent(LoginRegister);
      }
      else
      {
        RaiseEvent(LoginFail, ex);
      }
    }

    private void RaiseRegisterSuccess()
    {
      Debug.Log("[StartMenuManager] RaiseRegisterSuccess");
      RaiseEvent(RegisterSuccess);
    }

    private void RaiseRegisterFail(Exception ex)
    {
      Debug.Log("[StartMenuManager] RaiseRegisterFail");
      RaiseEvent(RegisterFail, ex);
    }

    private void RaiseLoadGameSuccess()
    {
      Debug.Log("[StartMenuManager] RaiseLoadGameSuccess");
      RaiseEvent(LoadGameSuccess);
    }

    private void RaiseLoadGameFail(Exception ex)
    {
      if (ex is NoGamesException)
      {
        Debug.Log("[StartMenuManager] RaiseLoadGameFail: " + ex.Message);
        RaiseEvent(LoadGameNewGame);
      }
      else
      {
        Debug.Log("[StartMenuManager] RaiseLoadGameFail: " + ex.Message);
        RaiseEvent(LoadGameFail, ex);
      }
    }

    private void RaiseNewGameSuccess()
    {
      Debug.Log("[StartMenuManager] RaiseNewGameSuccess");
      RaiseEvent(NewGameSuccess);
    }

    private void RaiseNewGameFail(Exception ex)
    {
      Debug.Log("[StartMenuManager] RaiseNewGameFail");
      RaiseEvent(NewGameFail, ex);
    }

    protected override void UnsubscribeAllFormEvents()
    {
      Debug.Log("[StartMenuManager] UnsubscribeAllFormEvents");
      // catch any errors with unsubscribing events, in the event that the form is not subscribed to the event
      try
      {
        loginForm.FormSuccess -= RaiseLoginSuccess;
        loginForm.FormFail -= RaiseLoginFail;
        registerForm.FormSuccess -= RaiseRegisterSuccess;
        registerForm.FormFail -= RaiseRegisterFail;
        loadGameForm.FormSuccess -= RaiseLoadGameSuccess;
        loadGameForm.FormFail -= RaiseLoadGameFail;
        newGameForm.FormSuccess -= RaiseNewGameSuccess;
        newGameForm.FormFail -= RaiseNewGameFail;
      }
      catch (Exception ex)
      {
        Debug.Log("[StartMenuManager] UnsubscribeAllFormEvents: " + ex.Message);
      }
    }
  }
}