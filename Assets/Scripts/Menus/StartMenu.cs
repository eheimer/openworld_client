using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public class StartMenu : MonoBehaviour {
    
    VisualElement menu;

    // Start is called before the first frame update
    protected void Start()
    {
      var thismenu = this.GetComponent<UIDocument>();
      menu = thismenu.rootVisualElement;
      menu.Q<Button>("load-button").clickable.clicked += LoadGameClick;
      menu.Q<Button>("new-button").clickable.clicked += NewGameClick;
      menu.Q<Button>("logout-button").clickable.clicked += LogoutClick;
      menu.Q<Button>("quit-button").clickable.clicked += QuitClick;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadGameClick(){
        // display LoadGame form
    }

    void NewGameClick(){
        // display NewGame form
    }

    void LogoutClick(){
        // display Login form
    }

    void QuitClick(){
        // Quit game
    }
  }
}