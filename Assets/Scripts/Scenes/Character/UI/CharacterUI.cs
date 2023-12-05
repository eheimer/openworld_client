using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class CharacterUI : UIBase
  {
    protected void Start()
    {
      mainMenu.rootVisualElement.Q<Button>("character-button").RemoveFromHierarchy();
    }
  }
}