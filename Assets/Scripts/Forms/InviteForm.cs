using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Proyecto26;
using TMPro;
using Openworld.Scenes;

namespace Openworld.Forms
{
    public class InviteForm : BaseForm
    {
        [SerializeField] TMP_InputField emailComponent;

        protected override void DoSubmit()
        {
            communicator.InvitePlayer(gameManager.currentGame, emailComponent.text, InviteSuccess, RequestException);
        }

        public void InviteSuccess(ResponseHelper res) {
            gameManager.LoadScene(SceneName.YourGames);
        }

        public void Cancel(){
            gameManager.LoadScene(SceneName.YourGames);
        }
    }
}