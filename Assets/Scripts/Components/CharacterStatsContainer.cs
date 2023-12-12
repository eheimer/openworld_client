using System.Collections;
using System.Collections.Generic;
using Openworld;
using Openworld.Binding;
using Openworld.Models;
using Openworld.Scenes;
using UnityEngine;

namespace Openworld
{
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
      // new StatDef() { name = "Mana", statField = "maxMana", showSubStat = true, subStatField = "manaReplenish" },
      // new StatDef() { name = "HP", statField = "maxHp",  showSubStat = true,subStatField = "hpReplenish" },
      // new StatDef() { name = "Stamina", statField = "maxStamina",  showSubStat = true,subStatField = "staminaReplenish" },
      new StatDef() { name = "Hit Ch", statField = "CharacterDetail.hitChance"},
      new StatDef() { name = "Def Ch", statField = "CharacterDetail.defChance"},
      new StatDef() { name = "Parry", statField = "CharacterDetail.parry"},
      new StatDef() { name = "Swing", statField = "CharacterDetail.swingSpeed"},
      new StatDef() { name = "Cast", statField = "CharacterDetail.castSpeed"},
      new StatDef() { name = "Heal", statField = "CharacterDetail.healSpeed"},
    };

    void Start()
    {
      CharacterDetail character = FindObjectOfType<GameManager>().character;
      var container = this.gameObject;
      for (int i = 0; i < stats.Length; i++)
      {
        var stat = Instantiate(statPrefab, container.transform);
        stat.GetComponent<CharacterStatDisplay>().Initialize(stats[i].showName, stats[i].name, stats[i].statField, stats[i].showSubStat, stats[i].subStatField);
      }
    }
  }
}