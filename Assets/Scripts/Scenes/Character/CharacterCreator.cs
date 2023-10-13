using Openworld.Binding;
using Openworld.Models;
using UnityEngine;
using System;
using Proyecto26;
using TMPro;
using UnityEngine.UI;

namespace Openworld
{

    public class CharacterCreator : ObservableMonoBehaviour
    {
        [SerializeField] TMP_InputField characterName;
        [SerializeField] Button createButton;
        [SerializeField] TMP_Dropdown raceDropdown;
        private RacesResponse[] _races;
        private Skill[] _skills;
        private int _selectedRace;
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

        protected void Start()
        {
            //populate the skills and races from the server
            var communicator = FindObjectOfType<GameManager>().GetCommunicator();
            communicator.GetRaces((resp) =>
            {
                Races = resp;
                // races dropdown should be converted to a bound control, but for now we'll just
                // manipulate the TMP one that we have
                raceDropdown.ClearOptions();
                raceDropdown.options.Add(new TMP_Dropdown.OptionData("Select a race..."));
                foreach (var item in Races)
                {
                    raceDropdown.options.Add(new TMP_Dropdown.OptionData(item.name));
                }
                raceDropdown.SetValueWithoutNotify(0);
                raceDropdown.RefreshShownValue();
            }, (RequestException ex) => { throw ex; });

            communicator.GetSkills((resp) =>
            {
                Skills = resp;
            }, (RequestException ex) => { throw ex; });
        }

        void Update()
        {
            createButton.interactable = Validate();
        }

        bool Validate()
        {
            //for initial phase, just make sure there is a character name
            if (characterName == null || characterName.text.Trim().Equals(""))
            {
                return false;
            }
            if (raceDropdown == null || raceDropdown.value == 0)
            {
                return false;
            }
            return true;
        }

        public void CreateButtonClick()
        {
            // find the GameManager component
            var gameManager = FindObjectOfType<GameManager>();
            var communicator = gameManager.GetCommunicator();

            // send the request to the server
            communicator.CreateCharacter(gameManager.currentGame, characterName.text, Races[raceDropdown.value - 1].id, (resp) =>
            {
                // save the character to the gameManager
                gameManager.GetPlayer().character = resp.id;
                gameManager.character = resp;
                CreateCharacterSuccess?.Invoke();
            }, (RequestException ex) =>
            {
                // fire the fail event
                CreateCharacterFail?.Invoke(ex);
            });


            Debug.Log("Clicked the create button");
        }
    }
}