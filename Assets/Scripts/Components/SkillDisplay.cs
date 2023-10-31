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

    // Method to handle the remove button click event
    public void HandleRemoveButtonClick()
    {
        // Raise the OnRemove event
        OnRemove?.Invoke(gameObject);
    }
}
