using System;
using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class CreateCharacter : MenuBase {

    protected override void RegisterButtonHandlers()
    {
      HandleClick("createcharacter-submit", Submit);
      HandleClick("createcharacter-cancel", CancelClick);
    }

    void Submit(){
      var gameManager = GetGameManager();
      var communicator = gameManager.GetCommunicator();
      var me = GetVisualElement();
      communicator.CreateCharacter(gameManager.currentGame,
        me.Q<TextField>("name").text,
        int.Parse(me.Q<TextField>("maxHp").text),
        int.Parse(me.Q<TextField>("baseResist").text),
        int.Parse(me.Q<TextField>("inventorySize").text),
        CreateSuccess, RequestException);
    }

    void CreateSuccess(ResponseHelper resp){
      var locParts = resp.GetHeader("location").Split('/');
      GetGameManager().GetPlayer().character = locParts[locParts.Length - 1];
      //show the character menu
      getUI().CloseMenu();
    }

    void CancelClick(){
      getUI().ShowMenu();
    }
  }
}