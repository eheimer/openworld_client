using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Scenes;
using Proyecto26;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Openworld.Forms {
    public class CreateCharacterForm : BaseForm
    {
        [SerializeField] TMP_InputField nameComponent;
        [SerializeField] TMP_InputField maxhpComponent;
    [SerializeField] TMP_InputField inventory;
    [SerializeField] TMP_InputField baseResist;

    protected override void DoSubmit()
        {
            communicator.CreateCharacter(nameComponent.text, Int32.Parse(maxhpComponent.text), Int32.Parse(inventory.text), Int32.Parse(baseResist.text), CreateSuccess, RequestException);
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