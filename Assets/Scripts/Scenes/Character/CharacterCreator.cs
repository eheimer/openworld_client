using System.Globalization;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using Openworld.Binding;
using Openworld.Models;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using System;
using Proyecto26;

namespace Openworld
{

    public class CharacterCreator : ObservableMonoBehaviour
    {
        private RacesResponse[] _races;
        private Skill[] _skills;
        private CreateCharacterRequest _character;

        public event Action CreateCharacterSuccess;
        public event Action<Exception> CreateCharacterFail;

        public RacesResponse[] Races
        {
            get
            {
                return _races;
            }
            set
            {
                Set(ref _races, value);
            }
        }

        public Skill[] Skills
        {
            get
            {
                return _skills;
            }
            set
            {
                Set(ref _skills, value);
            }
        }

        private void Start()
        {
            //populate the skills and races from the server
            var communicator = FindObjectOfType<GameManager>().GetCommunicator();
            communicator.GetRaces((resp) =>
            {
                Races = resp;
            }, (RequestException ex) => { throw ex; });

            communicator.GetSkills((resp) =>
            {
                Skills = resp;
            }, (RequestException ex) => { throw ex; });
        }

        // Update is called once per frame
        void Update()
        {
            // each update, validate the character.  If input is complete, enable the "Save" button
            // otherwise, disable it.
            // if it is not valid, update the button's hover text to explain why.
        }
    }
}