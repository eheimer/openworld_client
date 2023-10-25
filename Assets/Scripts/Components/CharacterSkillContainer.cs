using System;
using System.Collections.Generic;
using Openworld.Binding;
using Openworld.Models;
using UnityEngine;

namespace Openworld
{
    public class CharacterSkillContainer : BoundComponent
    {
        [SerializeField] private GameObject skillSelectorPrefab;
        [SerializeField] private GameObject skillDisplayPrefab;
        [SerializeField] private CharacterCreator creator;

        private GameObject skillSelector;

        public void AddRaceSkills(Skill[] raceSkills)
        {

        }

        // we're simply waiting for this to get called by the BoundComponent base class
        // before we instantiate the first SkillSelector, since we need the Skills array
        // to be populated first
        protected override void UpdateBindingTarget()
        {
            Skill[] skills = SourcePropertyValue as Skill[];
            if (skills != null && skills.Length > 0)
            {
                MakeSkillSelector(AvailableSkills(skills));
            }
        }

        public Skill[] AvailableSkills(Skill[] skills)
        {
            // get the skills from the CharacterCreator and filter out the skills already selected
            List<Skill> availableSkills = new List<Skill>();
            foreach (Skill skill in skills)
            {
                bool skillSelected = false;
                foreach (Skill selectedSkill in creator.SelectedSkills)
                {
                    if (selectedSkill.name == skill.name)
                    {
                        skillSelected = true;
                        break;
                    }
                }
                if (!skillSelected)
                {
                    availableSkills.Add(skill);
                }
            }
            return availableSkills.ToArray();
        }

        // Method to be called when a new skill is selected in the SkillSelector dropdown
        public void SkillSelected(string skillName)
        {
            // find the skill in Skills that has the same name as the selected skill
            Skill[] skills = SourcePropertyValue as Skill[];
            CharacterSkill selectedSkill = null;
            foreach (Skill skill in skills)
            {
                if (skill.name == skillName)
                {
                    selectedSkill = new CharacterSkill()
                    {
                        description = skill.description,
                        id = skill.id,
                        level = 1,
                        name = skill.name
                    };
                    break;
                }
            }
            // add the selected skill to the CharacterCreator
            if (selectedSkill != null)
            {
                creator.SelectedSkills.Add(selectedSkill);
            }
            // Instantiate a new SkillDisplay
            GameObject skillDisplay = Instantiate(skillDisplayPrefab, transform);
            var sd = skillDisplay.GetComponent<SkillDisplay>();
            sd.Initialize(selectedSkill);
            sd.OnRemove += SkillRemoved;

            // Destroy the current SkillSelector
            Destroy(skillSelector);

            // Instantiate a new SkillSelector for the next selection at the end of the list
            MakeSkillSelector(AvailableSkills(skills));
        }

        protected void MakeSkillSelector(Skill[] skills)
        {
            if (skillSelector != null)
            {
                Destroy(skillSelector);
            }
            // Instantiate the initial SkillSelector
            skillSelector = Instantiate(skillSelectorPrefab, transform);
            var ss = skillSelector.GetComponent<SkillSelector>();
            ss.Initialize(skills);
            ss.OnSkillSelected += SkillSelected;
        }

        // Method to be called when a skill is removed from the SkillDisplay
        public void SkillRemoved(GameObject skillDisplay)
        {
            // Remove the skill from the CharacterCreator
            SkillDisplay sd = skillDisplay.GetComponent<SkillDisplay>();
            // find the skill in creator.SelectedSkills that has the same name as sd
            foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
            {
                if (selectedSkill.name == sd.SkillName)
                {
                    creator.SelectedSkills.Remove(selectedSkill);
                    break;
                }
            }

            // Destroy the SkillDisplay game object
            Destroy(skillDisplay);

            // Destroy the current SkillSelector
            Destroy(skillSelector);
            MakeSkillSelector(AvailableSkills(creator.Skills));
        }

    }
}