using System.Collections;
using System.Collections.Generic;
using Openworld;
using Openworld.Models;
using Openworld.Scenes;
using UnityEngine;

public class CharacterStatsContainer : MonoBehaviour
{
  [SerializeField] GameObject statPrefab;

  class StatDef
  {
    public string name;
    public string statField;
    public bool showName = true;
    public bool showSubStat = false;
    public string subStatField;
  }

  StatDef[] stats = {
      new StatDef() { name = "Mana", statField = "mana", showSubStat = true, subStatField = "manaReplenish" },
      new StatDef() { name = "HP", statField = "hp",  showSubStat = true,subStatField = "hpReplenish" },
      new StatDef() { name = "Stamina", statField = "stamina",  showSubStat = true,subStatField = "staminaReplenish" },
      new StatDef() { name = "Hit Ch", statField = "hitChance"},
      new StatDef() { name = "Def Ch", statField = "defChance"},
      new StatDef() { name = "Parry", statField = "parry"},
      new StatDef() { name = "Swing", statField = "swingSpeed"},
      new StatDef() { name = "Cast", statField = "castSpeed"},
      new StatDef() { name = "Heal", statField = "healSpeed"},
 };



  void Start()
  {
    /*
    Mana, HP, Stamina, Hit Ch, Def Ch, Parry, Swing, Cast, Heal
    */
    CharacterDetail character = FindObjectOfType<GameManager>().character;
    var container = this.gameObject;
    for (int i = 0; i < stats.Length; i++)
    {
      var stat = Instantiate(statPrefab, container.transform);
      stat.GetComponent<CharacterStatDisplay>().Initialize(stats[i].showName, stats[i].name, character, stats[i].statField, stats[i].showSubStat, stats[i].subStatField);
    }
  }

  void Update()
  {

  }
}
