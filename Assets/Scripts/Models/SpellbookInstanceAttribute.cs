// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class SpellbookInstanceAttribute
  {
    public SpellbookAttribute attribute;
    public Skill skill;
    public SlayerType slayer;
    public SpellbookInstance spellbook;
    public int value;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}