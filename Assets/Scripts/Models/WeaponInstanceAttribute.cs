// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class WeaponInstanceAttribute
  {
    public WeaponAttribute attribute;
    public SlayerType slayer;
    public int value;
    public WeaponInstance weapon;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}