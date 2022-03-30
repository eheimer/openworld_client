using System;
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
      try
      {
        var me = GetVisualElement();
        me.Q<TextField>("username").value = "";
        me.Q<TextField>("password").value = "";
      } catch(Exception ex){
        Debug.Log("[Login] ClearForm: " + ex.Message);
      }
    }

    void LoginSubmit()
    {
      var me = GetVisualElement();
      GetGameManager().Login(
        me.Q<TextField>("username").value,
        me.Q<TextField>("password").value, LoginSuccess, RequestException);
    }

    void LoginRegister(){
      registerForm.GetComponent<MenuBase>().Show();
    }

    void LoginCancel(){
      getUI().CloseMenu();
    }

    void LoginSuccess(){
      getUI().ShowMenu();
    }
  }
}
