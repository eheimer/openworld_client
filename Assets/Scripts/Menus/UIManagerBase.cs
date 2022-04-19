using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public abstract class UIManagerBase : MonoBehaviour
  {
    [SerializeField]
    UIDocument mainMenu;
   
   public void HideAllMenus()
    {
      foreach (var menu in FindObjectsOfType<UIDocument>())
      {
        try{
          menu.rootVisualElement.visible = false;
        } catch(Exception e){
        }
      }
    }

    protected virtual bool MenuValidate(){
      return true;
    }

    /**
    ** This should handle menu display in the case of
    ** validation fail check
    */
    protected virtual void InvalidMenu(){ }

    public void ShowMenu()
    {
      HideAllMenus();
      if (MenuValidate())
      {
        this.gameObject.SetActive(true);
        //mainMenu.enabled = true;
        mainMenu.GetComponent<MenuBase>().Show();
      } else {
        InvalidMenu();
      }
    }

    public void CloseMenu(){
      HideAllMenus();
      FindObjectOfType<BaseScene>().ShowMenuButton();
    }

  }
}