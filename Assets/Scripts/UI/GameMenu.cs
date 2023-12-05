using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class GameMenu : MenuBase
  {

    protected override void RegisterButtonHandlers()
    {
      HandleClick("close-menu-button", getUI().CloseMenu);
      HandleClick("character-button", CharacterClick);
      HandleClick("inventory-button", InventoryClick);
      HandleClick("battle-button", BattleClick);
      HandleClick("quit-button", QuitClick);
    }

    void CharacterClick()
    {
      SceneManager.LoadScene(SceneName.Character.name());
    }

    void InventoryClick()
    {
      SceneManager.LoadScene(SceneName.Inventory.name());
    }

    void BattleClick()
    {
      SceneManager.LoadScene(SceneName.Battle.name());
    }

    void QuitClick()
    {
      GetGameManager().currentGame = null;
      SceneManager.LoadScene(SceneName.Start.name());
    }
  }
}