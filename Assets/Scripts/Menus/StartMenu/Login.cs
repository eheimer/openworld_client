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
        Debug.Log("[Login] ClearForm: " + ex.Message);
      }
    }

    void LoginSubmit()
    {
      var me = GetVisualElement();
      GetGameManager().GetCommunicator().Login(
        me.Q<TextField>("username").value.Trim(),
        me.Q<TextField>("password").value,
        (resp) => LoginSuccess(resp),
        RequestException);
    }

    void LoginRegister()
    {
      RaiseFail(new LoginRegisterException());
    }

    void LoginSuccess(LoginResponse resp)
    {
      Debug.Log("Login Success: " + resp.player + ", " + resp.token);
      GetGameManager().SetToken(resp.token);
      GetGameManager().GetCommunicator().GetPlayerDetail(resp.player, (resp) =>
      {
        Debug.Log("GetPlayerDetail Success: " + resp.username + ", " + resp.id);
        GetGameManager().SetPlayer(resp);
        RaiseSuccess();
      }, (ex) => RaiseFail(ex));
    }
  }
}
