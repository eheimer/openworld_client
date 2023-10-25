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
        private RacesResponse[] _races;
        private ObservableArray<Skill> _skills = new ObservableArray<Skill>();
        private List<CharacterSkill> _selectedSkills;
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
            // compare value to _selectedRace
            // if it's different, update _selectedRace and update the skill selectors
            if (value != _selectedRace)
            {
                CharacterSkill[] oldRaceSkills = Races[_selectedRace - 1].skills;
                CharacterSkill[] newRaceSkills = Races[value - 1].skills;

                // remove old skills/levels from _selectedSkills
                foreach (var raceSkill in oldRaceSkills)
                {
                    foreach (var selectedSkill in _selectedSkills)
                    {
                        if (selectedSkill.id == raceSkill.id)
                        {
                            selectedSkill.level -= raceSkill.level;
                            if (selectedSkill.level <= 0)
                            {
                                _selectedSkills.Remove(selectedSkill);
                            }
                        }
                    }
                }

                // add new skills/levels to _selectedSkills
                foreach (var raceSkill in newRaceSkills)
                {
                    bool found = false;
                    foreach (var selectedSkill in _selectedSkills)
                    {
                        if (selectedSkill.id == raceSkill.id)
                        {
                            selectedSkill.level += raceSkill.level;
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        _selectedSkills.Add((CharacterSkill)raceSkill);
                    }
                }
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
            // character name is required
            if (characterName == null || characterName.text.Trim().Equals(""))
            {
                formErrors.text = "Character name is required";
                return false;
            }
            // race is required
            if (raceDropdown == null || raceDropdown.value == 0)
            {
                formErrors.text = "Race selection is required";
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
            communicator.CreateCharacter(gameManager.currentGame, characterName.text, Races[raceDropdown.value - 1].id, strDropdown.value + 1, dexDropdown.value + 1, intDropdown.value + 1, (resp) =>
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

        public ObservableObject GetBindingSource()
        {
            return ObservableSkills;
        }
    }
}