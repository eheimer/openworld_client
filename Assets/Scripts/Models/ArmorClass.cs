// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class ArmorClass
  {
    public int durability;
    public string name;
    public ClassDamageReduction[] reductions;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}