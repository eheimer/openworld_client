// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class ActiveCondition
  {
    public Character character;
    public Condition condition;
    public int cooldownRemaining;
    public CreatureInstance creature;
    public int damage;
    public int roundsRemaining;
    public CreatureInstance target;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}