using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class GameMenuManager : UIManagerBase
  {

    [SerializeField]
    UIDocument createCharacterForm;

    protected override bool MenuValidate()
    {
      var gameManager = FindObjectOfType<GameManager>();
      return gameManager != null && !string.IsNullOrEmpty(gameManager.GetPlayer().character);
    }

    protected override void InvalidMenu()
    {
      createCharacterForm.GetComponent<MenuBase>().Show();
    }
  }
}