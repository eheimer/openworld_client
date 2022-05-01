using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using TMPro;
using UnityEngine;

public class CharacterStatDisplay : MonoBehaviour
{
  [SerializeField] GameObject stat;
  [SerializeField] public bool showName = true;
  [SerializeField] public string statName = "";
  [SerializeField] TMP_Text statNameTarget;
  [SerializeField] public bool showSubStat = true;
  [SerializeField] GameObject subStat;

  [SerializeField] string statBindField;
  [SerializeField] string subStatBindField;

  private CharacterDetail character;

  public void Initialize(bool showStatName, string statName, CharacterDetail source, string statBindField, bool showSubStat, string subStatBindField){
    this.showName = showStatName;
    this.statName = statName;
    this.character = source;
    this.statBindField = statBindField;
    this.showSubStat = showSubStat;
    this.subStatBindField = subStatBindField;

    // stat.GetComponent<DataBoundComponent<CharacterDetail, int>>().SetBindSource(character,statBindField);
    // if(showSubStat){
    //     subStat.GetComponent<DataBoundComponent<CharacterDetail, int>>().SetBindSource(character,subStatBindField);
    // }

    statNameTarget.gameObject.SetActive(showName);
    statNameTarget.text = statName;
    subStat.SetActive(showSubStat);
  }
}