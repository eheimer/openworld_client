using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class StartMenu : MenuBase {

    [SerializeField]
    UIDocument loadGame;
    [SerializeField]
    UIDocument newGame;


    protected override void RegisterButtonHandlers()
    {
      HandleClick("load-button", LoadGameClick);
      HandleClick("new-button", NewGameClick);
      HandleClick("logout-button", LogoutClick);
      HandleClick("quit-button", QuitClick);
    }

    void LoadGameClick(){
      loadGame.GetComponent<MenuBase>().Show();
    }

    void NewGameClick(){
      newGame.GetComponent<MenuBase>().Show();
    }

    void LogoutClick(){
      gameManager.Logout();
      ui.CloseMenu();
    }

    void QuitClick(){
        // Quit game
    }
  }
}