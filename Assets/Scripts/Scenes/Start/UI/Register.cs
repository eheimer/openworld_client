using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Register : FormBase
  {
    protected override void RegisterButtonHandlers()
    {
      HandleClick("register-submit", RegisterSubmit);
      HandleClick("register-cancel", RegisterCancel);
    }

    protected override void ClearForm()
    {
      var me = GetVisualElement();
      me.Q<TextField>("email").value = "";
      me.Q<TextField>("password").value = "";
      me.Q<TextField>("password-confirm").value = "";
      me.Q<TextField>("name").value = "";
    }

    void RegisterSubmit()
    {
      var me = GetVisualElement();
      GetGameManager().LoginOrRegister(
        me.Q<TextField>("email").value,
        me.Q<TextField>("password").value,
        me.Q<TextField>("name").value, RaiseSuccess, (ex) => RaiseFail(ex));
    }

    void RegisterCancel()
    {
      RaiseFail(new FormCancelException());
    }

    void RegisterSuccess(LoginResponse resp)
    {
      RaiseSuccess();
    }
  }
}
