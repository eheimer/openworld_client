// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class Monster
  {
    public MonsterAction[] actions;
    public int aggroPriority;
    public string alignment;
    public string anatomy;
    public bool animate;
    public string bard;
    public string baseDmg;
    public DamageType breathDmgType;
    public MonsterClue[] clues;
    public int controlSlots;
    public DamageType damageType;
    public string dexterity;
    public string evalInt;
    public string gold;
    public string hoverStats;
    public string hp;
    public string intelligence;
    public string karma;
    public string magery;
    public string name;
    public string packInstinct;
    public string preferredFood;
    public string resistC;
    public string resistE;
    public string resistF;
    public string resistP;
    public string resistPh;
    public string resistSpell;
    public SlayerType[] slayers;
    public string specials;
    public string strength;
    public string tactics;
    public string taming;
    public string tracking;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}