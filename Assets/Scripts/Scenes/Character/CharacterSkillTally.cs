using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Openworld.Binding;
using UnityEngine;

public class CharacterSkillTally : ObservableMonoBehaviour, IBindingProvider
{
    private static int SKILLS_MAX = 24;
    private static int SKILLS_BONUS = 0;
    private static int EFFECTIVE_SKILLS_MAX = SKILLS_MAX + SKILLS_BONUS;
    public class SkillTally : ObservableObject
    {
        private int skillsChosen = 0;
        private int skillsRemaining = EFFECTIVE_SKILLS_MAX;
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

    }

    public void SetSkillsMax(int max)
    {
        SKILLS_MAX = max;
        skillTally.SkillsRemaining = max + skillTally.SkillsBonus - skillTally.SkillsChosen;
    }

    public void SetSkillBonus(int bonus)
    {
        skillTally.SkillsBonus = bonus;
        skillTally.SkillsRemaining = SKILLS_MAX + bonus - skillTally.SkillsChosen;
    }

    public void SetSkillsChosen(int chosen)
    {
        skillTally.SkillsChosen = chosen;
        skillTally.SkillsRemaining = SKILLS_MAX + skillTally.SkillsBonus - chosen;
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
}
