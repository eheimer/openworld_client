using Openworld.Binding;
using Openworld.Models;
using UnityEngine;
using System;
using Proyecto26;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Openworld
{

    public class CharacterCreator : ObservableMonoBehaviour, IBindingProvider
    {
        [SerializeField] TMP_InputField characterName;
        [SerializeField] Button createButton;
        [SerializeField] TMP_Dropdown raceDropdown;
        [SerializeField] TMP_Dropdown strDropdown;
        [SerializeField] TMP_Dropdown dexDropdown;
        [SerializeField] TMP_Dropdown intDropdown;
        [SerializeField] TMP_Text formErrors;
        [SerializeField] CharacterSkillContainer skillContainer;
        [SerializeField] int maxSkills = 24;

        public int MaxSkillAllocation
        {
            get
            {
                return maxSkills;
            }
        }
        private RacesResponse[] _races;
        private ObservableArray<Skill> _skills = new ObservableArray<Skill>();
        private List<CharacterSkill> _selectedSkills;
        private int _selectedRace;

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
                return _skills.Items;
            }
            set
            {
                _skills.Items = value;
            }
        }

        public ObservableArray<Skill> ObservableSkills
        {
            get
            {
                return _skills;
            }
        }

        public List<CharacterSkill> SelectedSkills
        {
            get
            {
                if (_selectedSkills == null)
                {
                    _selectedSkills = new List<CharacterSkill>();
                }
                return _selectedSkills;
            }
            set
            {
                Set(ref _selectedSkills, value);
            }
        }

        protected void RaceChangeListener(int value)
        {
            if (value != _selectedRace)
            {
                CharacterSkill[] newRaceSkills = value > 0 ? Races[value - 1].skills : new CharacterSkill[0];
                skillContainer.RaceSkills = newRaceSkills;
                _selectedRace = value;
            }
        }

        protected void Start()
        {
            //populate the skills and races from the server
            var communicator = FindObjectOfType<GameManager>().GetCommunicator();

            //subscribe to the raceDropdown change event
            raceDropdown.onValueChanged.AddListener(RaceChangeListener);

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
            if (characterName == null || characterName.text.Trim().Equals(""))
            {
                formErrors.text = "Character name is required";
                return false;
            }

            if (raceDropdown == null || raceDropdown.value == 0)
            {
                formErrors.text = "Race selection is required";
                return false;
            }

            CharacterSkillTally.SkillTally tally = skillContainer.GetTally();
            if (tally.SkillsRemaining > 0)
            {
                formErrors.text = "You have " + tally.SkillsRemaining + " skill points remaining to be selected";
                return false;
            }

            if (tally.SkillsRemaining < 0)
            {
                formErrors.text = "You have selected " + -tally.SkillsRemaining + " too many skill points";
                return false;
            }

            if (strDropdown == null)
            {
                formErrors.text = "Strength is required";
                return false;
            }

            if (dexDropdown == null)
            {
                formErrors.text = "Dexterity is required";
                return false;
            }

            if (intDropdown == null)
            {
                formErrors.text = "Intelligence is required";
                return false;
            }

            if (Races[raceDropdown.value - 1].name.Equals("Fairy"))
            {
                if (strDropdown.value + dexDropdown.value + intDropdown.value != 4)
                {
                    formErrors.text = "Total stat points must equal 7";
                    return false;
                }
            }
            else if (strDropdown.value + dexDropdown.value + intDropdown.value != 5)
            {
                formErrors.text = "Total stat points must equal 8";
                return false;
            }

            formErrors.text = "";
            return true;
        }

        public void CreateButtonClick()
        {
            // find the GameManager component
            var gameManager = FindObjectOfType<GameManager>();
            var communicator = gameManager.GetCommunicator();

            // send the request to the server
            communicator.CreateCharacter(
                gameManager.currentGame, characterName.text,
                Races[raceDropdown.value - 1].id,
                strDropdown.value + 1, dexDropdown.value + 1, intDropdown.value + 1,
                SelectedSkills.ToArray(),
                (resp) =>
                {
                    // save the character to the gameManager
                    gameManager.GetPlayer().character = resp.id;
                    gameManager.character = resp;
                    CreateCharacterSuccess?.Invoke();
                },
                (RequestException ex) =>
                {
                    // fire the fail event
                    CreateCharacterFail?.Invoke(ex);
                }
            );
        }

        public ObservableObject GetBindingSource()
        {
            return ObservableSkills;
        }
    }
}