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

  public class CreateCharacter : FormBase
  {
    List<RacesResponse> races;

    protected override void RegisterButtonHandlers()
    {
      HandleClick("createcharacter-submit", Submit);
      HandleClick("createcharacter-cancel", CancelClick);
    }

    protected override void ClearForm()
    {
      try
      {
        var me = GetVisualElement();
        me.Q<TextField>("name").value = "";
      }
      catch (Exception ex)
      {
        Debug.Log("[CreateCharacter] ClearForm: " + ex.Message);
      }
    }

    void Submit()
    {
      var me = GetVisualElement();
      GetGameManager().GetCommunicator().CreateCharacter(
        GetGameManager().currentGame,
        me.Q<TextField>("name").text,
        (resp) => CreateSuccess(resp),
        RequestException);
    }

    void CreateSuccess(CharacterDetail resp)
    {
      GetGameManager().character = resp;
      RaiseSuccess();
    }

    void CancelClick()
    {
      RaiseFail(new FormCancelException());
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