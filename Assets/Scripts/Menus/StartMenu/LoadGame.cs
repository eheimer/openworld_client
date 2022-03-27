using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using UnityEngine;

namespace Openworld.Menus
{
  public class LoadGame : MenuBase
  {
    protected override void RegisterButtonHandlers()
    {
      HandleClick("cancel", CancelClick);
    }

    protected override void LoadData()
    {
      communicator.GetGames(gameManager.GetPlayer().playerId, LoadDataSuccess, RequestException);
    }

    void LoadDataSuccess(GamesResponse[] games)
    {
      foreach (var item in games)
      {
        Debug.Log(item);
      }
    }

    void CancelClick(){
      ui.CloseMenu();
    }

  }

}