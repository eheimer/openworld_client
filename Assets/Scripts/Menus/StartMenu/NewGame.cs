using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class NewGame : FormBase
  {
    protected override void RegisterButtonHandlers()
    {
      HandleClick("newgame-submit", CreateGame);
      HandleClick("newgame-cancel", CancelClick);
    }
    protected override void ClearForm()
    {
      var me = GetVisualElement();
      me.Q<TextField>("name").value = "";
    }

    void CreateGame()
    {
      var me = GetVisualElement();
      GetGameManager().GetCommunicator().CreateGame(
          me.Q<TextField>("name").value,
          CreateSuccess, RequestException);
    }

    void CreateSuccess(Game resp)
    {
      var gameManager = GetGameManager();
      gameManager.currentGame = resp.id;
      getUI().ShowMenu();
    }

    void CancelClick()
    {
      getUI().ShowMenu();
    }
  }
}
