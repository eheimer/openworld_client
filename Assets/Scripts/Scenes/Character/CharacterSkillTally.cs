using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Openworld;
using Openworld.Binding;
using Openworld.Models;
using UnityEngine;

public class CharacterSkillTally : ObservableMonoBehaviour, IBindingProvider
{
    public class SkillTally : ObservableObject
    {
        private int skillsChosen = 0;
        private int skillsRemaining = 0;
        private int skillsBonus = 0;

        public int SkillsChosen
        {
            get
            {
                return skillsChosen;
            }
            set
            {
                Set(ref skillsChosen, value);
            }
        }

        public int SkillsRemaining
        {
            get
            {
                return skillsRemaining;
            }
            set
            {
                Set(ref skillsRemaining, value);
            }
        }

        public int SkillsBonus
        {
            get
            {
                return skillsBonus;
            }
            set
            {
                Set(ref skillsBonus, value);
            }
        }

        public SkillTally MakeDeepCopy()
        {
            SkillTally copy = new SkillTally();
            copy.SkillsChosen = SkillsChosen;
            copy.SkillsRemaining = SkillsRemaining;
            copy.SkillsBonus = SkillsBonus;
            return copy;
        }

    }

    private CharacterCreator characterCreator;

    public int GetSkillsMax()
    {
        if (characterCreator == null)
        {
            characterCreator = FindObjectOfType<CharacterCreator>();
        }
        return characterCreator.MaxSkillAllocation;
    }

    public void SetSkillBonus(int bonus)
    {
        skillTally.SkillsBonus = bonus;
        skillTally.SkillsRemaining = GetSkillsMax() + bonus - skillTally.SkillsChosen;
    }

    public void SetSkillsChosen(int chosen)
    {
        skillTally.SkillsChosen = chosen;
        skillTally.SkillsRemaining = GetSkillsMax() + skillTally.SkillsBonus - chosen;
    }

    public int GetSkillsRemaining()
    {
        return skillTally.SkillsRemaining;
    }

    private SkillTally _skillTally = new SkillTally();

    public SkillTally skillTally
    {
        get { return _skillTally; }
        set { Set(ref _skillTally, value); }
    }

    public ObservableObject GetBindingSource()
    {
        return skillTally;
    }

    // returns a copy of the tally so that the original can't be modified
    public SkillTally GetTallyCopy()
    {
        return skillTally.MakeDeepCopy();
    }
}
