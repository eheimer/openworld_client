using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Openworld.Models;
using Openworld.Scenes;
using Proyecto26;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Forms
{
  public class LoginForm : BaseForm
  {
    [SerializeField] TMP_InputField usernameComponent;
    [SerializeField] TMP_InputField passwordComponent;
    [SerializeField] Toggle devToggle;

    protected override bool ShouldValidate { get { return false; } }
    override protected void Start() {
      //TODO: this can/should be deleted for production code, though it shouldn't affect it
      if (Application.isEditor)
      {
        usernameComponent.text = "eric@heimerman.org";
        passwordComponent.text = "eric";
      } else {
        devToggle.enabled = false;
      }
      base.Start();
      ToggleDev();
    }

    protected override void DoSubmit()
    {
      communicator.Login(usernameComponent.text, passwordComponent.text, LoginSuccess, RequestException);
    }

    public void LoginSuccess(LoginResponse res)
    {
      gameManager.SetToken(res.token);
      communicator.GetPlayer(res.player, gameManager.SetPlayer, RequestException);
    }

    public void Register()
    {
      gameManager.LoadScene(SceneName.Register);
    }

    public void ToggleDev(){
      communicator.SetIsDevUrl(devToggle.isOn);
    }
  }
}

