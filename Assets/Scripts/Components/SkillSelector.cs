using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Openworld.Binding;
using Openworld.Models;
using System.Collections.Generic;
using System;

namespace Openworld
{
    public class SkillSelector : MonoBehaviour
    {
        // Reference to the TMP_Dropdown in the SkillSelector prefab
        [SerializeField] private TMP_Dropdown skillDropdown;

        // Event to notify when a skill is selected
        public delegate void SkillSelectedHandler(string skillName);
        public event SkillSelectedHandler OnSkillSelected;

        public void Initialize(Skill[] skills)
        {
            List<string> options = new List<string>();
            options.Add("Select a skill...");
            foreach (Skill skill in skills)
            {
                options.Add(skill.name);
            }
            // clear the dropdown options
            skillDropdown.ClearOptions();
            // get the skills from the CharacterSkillContainer
            skillDropdown.AddOptions(options);
            // Subscribe to the OnValueChanged event of the TMP_Dropdown
            skillDropdown.onValueChanged.AddListener(HandleDropdownValueChanged);
        }

        // Method to handle the dropdown value change event
        private void HandleDropdownValueChanged(int index)
        {
            // Get the selected skill name
            string selectedSkill = skillDropdown.options[index].text;

            // Raise the OnSkillSelected event
            OnSkillSelected?.Invoke(selectedSkill);
        }

        // Other methods and logic can be added as needed
    }
}