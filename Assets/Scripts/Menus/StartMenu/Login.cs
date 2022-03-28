using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Login : FormBase
  {
    [SerializeField]
    UIDocument registerForm;
    [SerializeField]

    protected override void RegisterButtonHandlers(){
      HandleClick("login-submit", LoginSubmit);
      HandleClick("login-register", LoginRegister);
      HandleClick("login-cancel", LoginCancel);
    }

    protected override void ClearForm(){
      me.Q<TextField>("username").value = "";
      me.Q<TextField>("password").value = "";
    }

    void LoginSubmit()
    {
      gameManager.Login(
        me.Q<TextField>("username").value,
        me.Q<TextField>("password").value, LoginSuccess, RequestException);
    }

    void LoginRegister(){
      registerForm.GetComponent<MenuBase>().Show();
    }

    void LoginCancel(){
      ui.CloseMenu();
    }

    void LoginSuccess(){
      ui.ShowMenu();
    }
  }
}
