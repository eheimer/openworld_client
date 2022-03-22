using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Register : MonoBehaviour
  {
    StartSceneMenu ui;
    // Start is called before the first frame update
    void Start()
    {
      ui = FindObjectOfType<StartSceneMenu>();
      var root = this.GetComponent<UIDocument>().rootVisualElement;
      root.Q<Button>("register-submit").clickable.clicked += RegisterSubmit;
      root.Q<Button>("register-cancel").clickable.clicked += RegisterCancel;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RegisterSubmit(){
      //submit
    }

    void RegisterCancel(){
      //close the form
      ui.CloseMenu();
    }
  }
}
