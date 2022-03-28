using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class LoadGame : MenuBase
  {
    [SerializeField]
    protected VisualTreeAsset gameItem;

    protected override void RegisterButtonHandlers()
    {
      HandleClick("cancel", CancelClick);
    }

    protected override void GetData()
    {
      communicator.GetGames(gameManager.GetPlayer().playerId, GetDataSuccess, RequestException);
    }

    void GetDataSuccess(GamesResponse[] games)
    {
      var container = me.Q<VisualElement>("unity-content-container");
      container.Clear();
      // Debug.Log(container);
      foreach (var item in games)
      {
        // Debug.Log(item);
        VisualElement disp = gameItem.Instantiate();
        disp.Q<Label>("game_name").text = item.game.name;
        disp.Q<Label>("character_name").text = item.character.name;
        disp.Q<Label>("owner").visible = !item.owner;
        disp.Q<Button>("button").viewDataKey = item.game.id + "|" + (item.character != null ? item.character.id: "");
        disp.Q<Button>("button").clickable.clickedWithEventInfo += LoadGameClick;
        container.Add(disp);
      }
    }

    void LoadGameClick(EventBase e){
      Debug.Log(((Button)e.currentTarget).viewDataKey);
      string[] data = ((Button)e.currentTarget).viewDataKey.Split('|');
      gameManager.currentGame = data[0];
      gameManager.GetPlayer().character = data[1];
      Debug.Log(data);
      gameManager.LoadScene(SceneName.Character);
    }

    void CancelClick(){
      ui.ShowMenu();
    }

  }

}