using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
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
       me.Q<TextField>("name").value = "";
       me.Q<TextField>("players").value = ""; 
    }

    void CreateGame(){
      communicator.CreateGame(
          me.Q<TextField>("name").value,
          Int32.Parse(me.Q<TextField>("players").value),
          CreateSuccess, RequestException);
    }

    void CreateSuccess(ResponseHelper resp){
      Debug.Log(resp.GetHeader("location"));
      var locParts = resp.GetHeader("location").Split('/');
      gameManager.currentGame = locParts[locParts.Length - 1];
      gameManager.LoadScene(SceneName.Character);
    }

    void CancelClick(){
      ui.ShowMenu();
    }
  }
}
