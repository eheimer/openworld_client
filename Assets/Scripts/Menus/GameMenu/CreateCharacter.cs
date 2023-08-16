using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class CreateCharacter : MenuBase
  {
    List<RacesResponse> races;
    protected override void Start()
    {
      base.Start();
    }

    protected override void RegisterButtonHandlers()
    {
      HandleClick("createcharacter-submit", Submit);
      HandleClick("createcharacter-cancel", CancelClick);
    }

    void Submit()
    {
      var gameManager = GetGameManager();
      var communicator = gameManager.GetCommunicator();
      var me = GetVisualElement();
      communicator.CreateCharacter(gameManager.currentGame,
        me.Q<TextField>("name").text,
        CreateSuccess, RequestException);
    }

    void CreateSuccess(ResponseHelper resp)
    {
      var locParts = resp.GetHeader("location").Split('/');
      GetGameManager().GetPlayer().character = locParts[locParts.Length - 1];
      //show the character scene
      getUI().CloseMenu();
    }

    void CancelClick()
    {
      getUI().CloseMenu();
    }

    protected override void GetData()
    {
      GetGameManager().GetCommunicator().GetRaces(GetDataSuccess, RequestException);
    }

    void GetDataSuccess(RacesResponse[] resp)
    {
      races = new List<RacesResponse>();
      List<string> raceNames = new List<string>();

      foreach (var race in resp)
      {
        races.Add(race);
        raceNames.Add(race.name);
      }
      //populate the drop-down with the races from the CharacterScene
      var me = GetVisualElement();
      var dropdown = me.Q<DropdownField>("race");
      dropdown.choices = raceNames;
    }
  }
}