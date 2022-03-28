using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class GameMenu : MenuBase {

    protected override void RegisterButtonHandlers()
    {
      HandleClick("character-button", CharacterClick);
      HandleClick("inventory-button", InventoryClick);
      HandleClick("battle-button", BattleClick);
      HandleClick("map-button", WorldMapClick);
      HandleClick("store-button", StoreClick);
      HandleClick("quit-button", QuitClick);
    }

    void CharacterClick(){
        //load the character scene
    }

    void InventoryClick(){
        //load the inventory scene
    }

    void BattleClick(){
        //load the battle scene
    }

    void WorldMapClick(){
        //load the map scene
    }

    void StoreClick(){
        //load the store scene
    }

    void QuitClick(){
      gameManager.currentGame = null;
      gameManager.LoadScene(SceneName.Start);
    }
  }
}