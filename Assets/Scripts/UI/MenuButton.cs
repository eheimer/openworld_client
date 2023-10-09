using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Openworld.Menus

{
  public sealed class MenuButton : MonoBehaviour
  {
    void Start()
    {
      this.GetComponent<UIDocument>().rootVisualElement.Q<Button>().clickable.clicked += ShowMenu;
    }

    public void ShowMenu()
    {
      FindObjectOfType<UIBase>(true)?.ShowMenu();
    }

  }

}