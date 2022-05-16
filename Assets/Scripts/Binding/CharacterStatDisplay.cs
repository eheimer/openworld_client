using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using TMPro;
using UnityEngine;

namespace Openworld.Binding
{
  public class CharacterStatDisplay : MonoBehaviour
  {
    [SerializeField] GameObject stat;
    [SerializeField] TMP_Text statNameTarget;
    [SerializeField] GameObject subStat;

    public void Initialize(bool showStatName, string statName, string statBindField, bool showSubStat, string subStatBindField)
    {
      stat.GetComponentInChildren<BoundComponent>().bindingSourceProperty = statBindField;
      if(showSubStat){
          subStat.GetComponentInChildren<BoundComponent>().bindingSourceProperty = subStatBindField;
      }

      statNameTarget.gameObject.SetActive(showStatName);
      statNameTarget.text = statName;
      subStat.SetActive(showSubStat);
    }
  }
}