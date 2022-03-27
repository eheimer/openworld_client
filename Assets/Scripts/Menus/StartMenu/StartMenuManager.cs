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

    protected override bool Validate(){
      return gameManager.GetAuthToken() != null && !gameManager.GetAuthToken().Equals("");
    }

    protected override void InvalidMenu()
    {
      loginForm.GetComponent<MenuBase>().Show();
    }

  }
}