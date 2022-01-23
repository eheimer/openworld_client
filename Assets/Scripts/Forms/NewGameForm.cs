using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Proyecto26;
using System;
using Openworld.Models;
using TMPro;
using Openworld.Scenes;

namespace Openworld.Forms
{
    public class NewGameForm : BaseForm
    {
        [SerializeField] TMP_InputField nameComponent;
        [SerializeField] TMP_InputField maxPlayersComponent;

        protected override bool ShouldValidate { get { return false; } }

        protected override void DoSubmit()
        {
            communicator.CreateGame(nameComponent.text, Int32.Parse(maxPlayersComponent.text), CreateSuccess, RequestException);
        }

        public void CreateSuccess(ResponseHelper res)
        {
            gameManager.LoadScene(SceneName.YourGames);
        }

        public void Cancel()
        {
            gameManager.LoadScene(SceneName.YourGames);
        }

        public void ValidateInput(string value)
        {
            int parsed;
            if (Int32.TryParse(value,out parsed))
            {
                maxPlayersComponent.text = value;
            }
        }
    }
}

