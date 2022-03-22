using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class StartSceneMenu : MonoBehaviour
  {

    [SerializeField]
    UIDocument start;
    [SerializeField]
    UIDocument menuButton;
    [SerializeField]
    UIDocument loginForm;
    GameManager gameManager;

    // Start is called before the first frame update
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

    public void ShowMenu()
    {
      HideAllMenus();
      if(gameManager.GetAuthToken() != null && !gameManager.GetAuthToken().Equals("")){
        start.rootVisualElement.visible = true;
      }
      else {
        loginForm.rootVisualElement.visible = true;
      }
    }

    public void CloseMenu()
    {
      HideAllMenus();
      menuButton.rootVisualElement.visible = true;
    }

  }
}