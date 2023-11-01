using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Openworld.Models;

public class SkillDisplay : MonoBehaviour
{
    // Reference to the TextMeshProUGUI to display the skill name
    [SerializeField] private TMP_Text skillNameText;

    [SerializeField] private TMP_Dropdown skillLevelDropdown;

    private int minLevel;
    private int maxLevel;

    // Event to notify when the skill level changes
    public delegate void SkillLevelChangedHandler(GameObject skillDisplay);
    public event SkillLevelChangedHandler OnSkillLevelChanged;

    public void Initialize(CharacterSkill skill, int minLevel = 0, int maxLevel = 4)
    {
        SkillName = skill.name;
        this.minLevel = minLevel;
        this.maxLevel = maxLevel;
        SkillLevel = skill.level;
    }

    public string SkillName
    {
        get
        {
            return skillNameText.text;
        }
        set
        {
            skillNameText.text = value;
        }
    }

    public int SkillLevel
    {
        get
        {
            return skillLevelDropdown.value;
        }
        set
        {
            skillLevelDropdown.value = value;
        }
    }

    // Method to handle the skill level dropdown value changed event
    public void HandleSkillLevelChanged()
    {
        // make sure the skill level is within the min and max
        skillLevelDropdown.SetValueWithoutNotify(
            Mathf.Clamp(SkillLevel, this.minLevel, this.maxLevel)
        );
        OnSkillLevelChanged?.Invoke(gameObject);
    }
}
