using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Login : MonoBehaviour
  {
    [SerializeField]
    UIDocument registerForm;
    StartSceneMenu ui;

    // Start is called before the first frame update
    void Start()
    {
      ui = FindObjectOfType<StartSceneMenu>();
      var root = this.GetComponent<UIDocument>().rootVisualElement;
      root.Add(new InspectorElement(this));
      root.Q<Button>("login-submit").clickable.clicked += LoginSubmit;
      root.Q<Button>("login-register").clickable.clicked += LoginRegister;
      root.Q<Button>("login-cancel").clickable.clicked += LoginCancel;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void LoginSubmit()
    {
      //submit
      var username = this.GetComponent<UIDocument>().rootVisualElement.Q<TextField>("username").value;
      var password = this.GetComponent<UIDocument>().rootVisualElement.Q<TextField>("password").value;
      var communicator = FindObjectOfType<Communicator>();
      communicator.Login(username, password,
        (response) =>
        {
          var gameManager = FindObjectOfType<GameManager>();
          gameManager.SetToken(response.token);
          communicator.GetPlayerDetail(response.player,
          (response) =>
          {
            gameManager.SetPlayer(response);
            ui.ShowMenu();
          }, (err) =>
          {
            Debug.Log(err.Message);
          });
        }, (err) =>
        {
          Debug.Log(err.Message);
        });
    }

    void LoginRegister(){
      //show register form
      ui.HideAllMenus();
      registerForm.rootVisualElement.visible = true;
    }

    void LoginCancel(){
      //close the form
      ui.CloseMenu();
    }
  }
}
