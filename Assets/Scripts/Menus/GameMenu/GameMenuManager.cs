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
      var gameManager = GetGameManager();
      return gameManager != null && !string.IsNullOrEmpty(gameManager.GetPlayer().character);
    }

    protected override void InvalidMenu()
    {
      createCharacterForm.GetComponent<MenuBase>().Show();
    }

    void DestroyAll()
    {
      for (int i = 0; i < transform.childCount; i++)
      {
        var child = transform.GetChild(i);
        Destroy(child.gameObject);
      }
      Destroy(gameObject);
    }
  }
}