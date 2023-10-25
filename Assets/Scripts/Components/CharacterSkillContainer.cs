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

        // these skills record the minimum levels for skills associated with the character's race
        private CharacterSkill[] _raceSkills = new CharacterSkill[0];
        public CharacterSkill[] RaceSkills
        {
            set
            {
                // remove current _raceSkills from creator.SelectedSkills
                foreach (CharacterSkill raceSkill in _raceSkills)
                {
                    foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
                    {
                        if (selectedSkill.id == raceSkill.id)
                        {
                            selectedSkill.level -= raceSkill.level;
                            if (selectedSkill.level <= 0)
                            {
                                creator.SelectedSkills.Remove(selectedSkill);
                            }
                            break;
                        }
                    }
                }
                _raceSkills = value;
                // add new _raceSkills to creator.SelectedSkills
                foreach (CharacterSkill raceSkill in _raceSkills)
                {
                    bool found = false;
                    foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
                    {
                        if (selectedSkill.id == raceSkill.id)
                        {
                            selectedSkill.level += raceSkill.level;
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        creator.SelectedSkills.Add(raceSkill);
                    }
                }
                RefreshSkillDisplays();
            }
        }

        public void RefreshSkillDisplays()
        {
            // remove any skill displays that are not in creator.SelectedSkills
            List<GameObject> skillDisplays = new List<GameObject>();
            foreach (Transform child in transform)
            {
                // don't add the skillSelector
                if (child.gameObject == skillSelector)
                {
                    continue;
                }
                skillDisplays.Add(child.gameObject);
            }
            foreach (GameObject skillDisplay in skillDisplays)
            {
                SkillDisplay sd = skillDisplay.GetComponent<SkillDisplay>();
                bool found = false;
                foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
                {
                    if (selectedSkill.name == sd.SkillName)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    Destroy(skillDisplay);
                }
            }
            // add a skill display for any skills that are in creator.SelectedSkills but not in the list of skill displays
            foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
            {
                bool found = false;
                foreach (Transform child in transform)
                {
                    SkillDisplay sd = child.gameObject.GetComponent<SkillDisplay>();
                    if (sd != null && sd.SkillName == selectedSkill.name)
                    {
                        // make sure the skill level is the same
                        if (sd.SkillLevel != selectedSkill.level)
                        {
                            sd.SkillLevel = selectedSkill.level;
                        }
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    GameObject skillDisplay = Instantiate(skillDisplayPrefab, transform);
                    var sd = skillDisplay.GetComponent<SkillDisplay>();
                    sd.Initialize(selectedSkill);
                    sd.OnRemove += SkillRemoved;
                }
            }
            if (skillSelector != null)
            {
                Destroy(skillSelector);
            }
            MakeSkillSelector(AvailableSkills(creator.Skills));
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
            RefreshSkillDisplays();
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

            // if the skill is in _raceSkills, we cannot remove it
            foreach (CharacterSkill raceSkill in _raceSkills)
            {
                if (raceSkill.name == sd.SkillName)
                {
                    return;
                }
            }

            // find the skill in creator.SelectedSkills that has the same name as sd
            foreach (CharacterSkill selectedSkill in creator.SelectedSkills)
            {
                if (selectedSkill.name == sd.SkillName)
                {
                    creator.SelectedSkills.Remove(selectedSkill);
                    break;
                }
            }
            RefreshSkillDisplays();
        }
    }
}