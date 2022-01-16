using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;
using Openworld.Models;

namespace Openworld.Forms
{
    public class LoginForm : BaseForm
    {
        [SerializeField] InputField usernameComponent;
        [SerializeField] InputField passwordComponent;

        protected override bool ShouldValidate { get { return false; } }

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
    }
}

