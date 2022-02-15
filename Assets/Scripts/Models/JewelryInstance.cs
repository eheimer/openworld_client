// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class JewelryInstance
  {
    public JewelryInstanceAttribute[] attributes;
    public bool damaged;
    public bool equipped;
    public Gem gem;
    public Inventory inventory;
    public JewelryLocation location;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}