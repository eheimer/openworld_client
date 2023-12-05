using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
    public class InventoryUI : UIBase
    {
        void Start()
        {
            mainMenu.rootVisualElement.Q<Button>("inventory-button").RemoveFromHierarchy();
        }
    }
}
