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

    protected override void Start()
    {
      base.Start();
      if ( string.IsNullOrEmpty(gameManager.GetAuthToken()) || string.IsNullOrEmpty(gameManager.currentGame))
      {
        gameManager.LoadScene(SceneName.Start);
      }
    }

    protected override bool MenuValidate()
    {
      Debug.Log(gameManager.GetPlayer());
      Debug.Log(gameManager.GetPlayer().character);
      return gameManager.GetPlayer() != null && !string.IsNullOrEmpty(gameManager.GetPlayer().character);
    }

    protected override void InvalidMenu()
    {
        createCharacterForm.GetComponent<MenuBase>().Show();
    }
  }
}