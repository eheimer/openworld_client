using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using System;
using TMPro;

namespace Openworld.Forms {
    public class CreateCharacterForm : BaseForm
    {
        [SerializeField] TMP_InputField nameComponent;
        [SerializeField] TMP_InputField maxhpComponent;

        protected override void DoSubmit()
        {
            communicator.CreateCharacter(nameComponent.text, Int32.Parse(maxhpComponent.text), CreateSuccess, RequestException);
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
                maxhpComponent.text = value;
            }
        }
    }
}