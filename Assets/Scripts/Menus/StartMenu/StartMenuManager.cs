using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class StartMenuManager : UIManagerBase
  {

    [SerializeField]
    UIDocument loginForm;

    protected override bool MenuValidate()
    {
      var gameManager = FindObjectOfType<GameManager>();
      return gameManager != null && !string.IsNullOrEmpty(gameManager.GetAuthToken());
    }

    protected override void InvalidMenu()
    {
      loginForm.GetComponent<MenuBase>().Show();
    }
  }
}