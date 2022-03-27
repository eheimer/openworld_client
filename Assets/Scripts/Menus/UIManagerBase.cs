using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public abstract class UIManagerBase : MonoBehaviour
  {

    [SerializeField]
    UIDocument mainMenu;
    [SerializeField]
    UIDocument menuButton;
    protected GameManager gameManager;

    protected void Start()
    {
      gameManager = FindObjectOfType<GameManager>();
      CloseMenu();
      menuButton.rootVisualElement.Q<Button>().clickable.clicked += ShowMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HideAllMenus()
    {
      foreach (var menu in FindObjectsOfType<UIDocument>())
      {
        menu.rootVisualElement.visible = false;
      }
    }

    protected virtual bool Validate(){
      return true;
    }

    /**
    ** This should handle display in the case of
    ** validation fail check
    */
    protected virtual void InvalidMenu(){ }

    public void ShowMenu()
    {
      HideAllMenus();
      if (Validate())
      {
        mainMenu.GetComponent<MenuBase>().Show();
      } else {
        InvalidMenu();
      }
    }

    public void CloseMenu()
    {
      HideAllMenus();
      menuButton.rootVisualElement.visible = true;
    }

  }
}