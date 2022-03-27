using System.Collections;
using System.Collections.Generic;
using Proyecto26;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class Register : FormBase
  {
    private string email;
    private string pass;

    protected override void RegisterButtonHandlers(){
      HandleClick("register-submit", RegisterSubmit);
      HandleClick("register-cancel", RegisterCancel);
    }

    protected override void ClearForm()
    {
      me.Q<TextField>("email").value = "";
      me.Q<TextField>("password").value = "";
      me.Q<TextField>("password-confirm").value = "";
      me.Q<TextField>("name").value = "";
    }

    void RegisterSubmit(){
      // need to save these, because we need them later, and the FormBase
      // wipes them out after this method is called
      email = me.Q<TextField>("email").value;
      pass = me.Q<TextField>("password").value;
      communicator.Register( email, pass,
        me.Q<TextField>("name").value, RegisterSuccess, RequestException);
    }

    void RegisterCancel(){
      ui.CloseMenu();
    }

    void RegisterSuccess(ResponseHelper resp){
      Debug.Log("Register success: " + resp.Data);
      gameManager.Login(email, pass, LoginSuccess, RequestException);
    }

    void LoginSuccess(){
      ui.ShowMenu();
    }

  }
}
