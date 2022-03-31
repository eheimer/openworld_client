using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using UnityEditor;
using UnityEngine;
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
      HandleClick("map-button", WorldMapClick);
      HandleClick("store-button", StoreClick);
      HandleClick("quit-button", QuitClick);
    }

    void CharacterClick()
    {
      //load the character scene
      GetGameManager().LoadScene(SceneName.Character);
    }

    void InventoryClick()
    {
      //load the inventory scene
      GetGameManager().LoadScene(SceneName.Character);
      //switch to the inventory panel
    }

    void BattleClick()
    {
      //load the battle scene
      GetGameManager().LoadScene(SceneName.Battle);
    }

    void WorldMapClick()
    {
      //load the map scene
      GetGameManager().LoadScene(SceneName.WorldMap);
    }

    void StoreClick()
    {
      //load the store scene
      GetGameManager().LoadScene(SceneName.Store);
    }

    void QuitClick()
    {
      var gameManager = GetGameManager();
      gameManager.currentGame = null;
      gameManager.LoadScene(SceneName.Start);
    }
  }
}