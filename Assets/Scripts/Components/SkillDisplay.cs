using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Openworld.Models;

public class SkillDisplay : MonoBehaviour
{
    // Reference to the TextMeshProUGUI to display the skill name
    [SerializeField] private TMP_Text skillNameText;

    // Reference to the Button to remove the skill
    [SerializeField] private Button removeButton;

    // Event to notify when the skill is removed
    public delegate void SkillRemovedHandler(GameObject skillDisplay);
    public event SkillRemovedHandler OnRemove;

    public void Initialize(CharacterSkill skill)
    {
        // Set the skill name
        SetSkillName(skill.name);
        // TODO: set skill level, min level, etc.
    }

    public string SkillName
    {
        get
        {
            return skillNameText.text;
        }
    }

    // Method to set the skill name in the SkillDisplay
    public void SetSkillName(string skillName)
    {
        skillNameText.text = skillName;
    }

    // Method to handle the remove button click event
    public void HandleRemoveButtonClick()
    {
        // Raise the OnRemove event
        OnRemove?.Invoke(gameObject);
    }
}
