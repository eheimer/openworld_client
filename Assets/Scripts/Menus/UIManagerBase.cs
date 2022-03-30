using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public abstract class UIManagerBase : MonoBehaviour
  {
    [SerializeField]
    UIDocument mainMenu;
    private GameManager gameManager;

    protected virtual void Start()
    {
      if(GetGameManager() == null){
        SceneManager.LoadScene(SceneName.Start.name());
      } else {
        GetGameManager().SetMenuManager(this);
        CloseMenu();
      }
    }

    protected GameManager GetGameManager(){
      if(gameManager == null){
        gameManager = FindObjectOfType<GameManager>();
      }
      return gameManager;
    }

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
        mainMenu.GetComponent<MenuBase>().Show();
      } else {
        InvalidMenu();
      }
    }

    public void CloseMenu(){
      HideAllMenus();
      GetGameManager().ShowMenuButton();
    }

  }
}