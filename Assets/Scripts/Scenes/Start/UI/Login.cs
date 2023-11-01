using System;
using Openworld.Models;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Login : FormBase
  {

    protected override void RegisterButtonHandlers()
    {
      HandleClick("login-submit", LoginSubmit);
      HandleClick("login-register", LoginRegister);
    }

    protected override void ClearForm()
    {
      try
      {
        var me = GetVisualElement();
        me.Q<TextField>("username").value = "";
        me.Q<TextField>("password").value = "";
      }
      catch (Exception ex)
      {
        Debug.LogError("[Login] ClearForm: " + ex.Message);
      }
    }

    void LoginSubmit()
    {
      var me = GetVisualElement();
      GetGameManager().Login(
        me.Q<TextField>("username").value.Trim(),
        me.Q<TextField>("password").value,
        RaiseSuccess, (ex) => RaiseFail(ex));
    }

    void LoginRegister()
    {
      RaiseFail(new LoginRegisterException());
    }
  }
}
