using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Openworld.Scenes
{

  public class CharacterOverviewDataMap : MonoBehaviour
  {
    private CharacterDetail character;
    private Dictionary<string, ArrayList> characterMap = new Dictionary<string, ArrayList>();
    
    public void SetCharacter(CharacterDetail detail){
      this.character = detail;
      this.UpdateBoundControls();
    }

    protected void Start()
    {
      //find datapoints
      foreach (var go in FindObjectsOfType<GameObject>())
      {
        if (!string.IsNullOrEmpty(go.tag))
        {
          if (go.tag.StartsWith("character."))
          {
            object bindObject = go.GetComponent<TMP_Text>();
            if (bindObject == null)
            {
              bindObject = go.GetComponent<Image>();
            }
            var prop = go.tag.Substring("character.".Length);
            object item;
            try
            {
              item = characterMap[prop];
            }
            catch (Exception)
            {
              characterMap.Add(prop, new ArrayList());
            }
            var mapValues = characterMap[prop];
            mapValues.Add(bindObject);
          }
        }
      }
    }

    protected void UpdateBoundControls()
    {
      {
        foreach (var bind in characterMap.Keys)
        {
          var bindObjects = characterMap[bind];
          string value = null;
          try
          {
            value = character.GetType().GetField(bind).GetValue(character).ToString();
          } catch (Exception){
            Debug.Log("Field " + bind + " is not found");
          }
          if(String.IsNullOrEmpty(value)) {
            continue;
          }
          foreach (var bindObject in bindObjects)
          {
            if (bindObject.GetType() == typeof(TextMeshProUGUI))
            {
              ((TMP_Text)bindObject).text = value;
            }
            else if (bindObject.GetType() == typeof(Image))
            {
              try
              {
                ((Image)bindObject).fillAmount = int.Parse(value);
              } catch (Exception e){
                Debug.Log("IMAGE EX: " + e.Message);
              }
            }
          }
        }
      }
    }
  }
}