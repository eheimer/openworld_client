using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class GameMenuManager : UIManagerBase
  {

    [SerializeField]
    MenuBase createCharacterForm;

    protected override bool MenuValidate()
    {
      var gameManager = FindObjectOfType<GameManager>();
      return gameManager != null && gameManager.character != null;
    }

    protected override void InvalidMenu()
    {
      createCharacterForm.Show();
    }
  }
}