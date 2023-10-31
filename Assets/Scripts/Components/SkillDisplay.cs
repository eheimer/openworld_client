using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Openworld.Models;

public class SkillDisplay : MonoBehaviour
{
    // Reference to the TextMeshProUGUI to display the skill name
    [SerializeField] private TMP_Text skillNameText;

    [SerializeField] private TMP_Dropdown skillLevelDropdown;

    // Reference to the Button to remove the skill
    [SerializeField] private Button removeButton;

    // Event to notify when the skill is removed
    public delegate void SkillRemovedHandler(GameObject skillDisplay);
    public event SkillRemovedHandler OnRemove;
    // Event to notify when the skill level changes
    public delegate void SkillLevelChangedHandler(GameObject skillDisplay);
    public event SkillLevelChangedHandler OnSkillLevelChanged;

    public void Initialize(CharacterSkill skill)
    {
        // Set the skill name
        SkillName = skill.name;
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
        // Raise the OnSkillLevelChanged event
        OnSkillLevelChanged?.Invoke(gameObject);
    }

    // Method to handle the remove button click event
    public void HandleRemoveButtonClick()
    {
        // Raise the OnRemove event
        OnRemove?.Invoke(gameObject);
    }
}
