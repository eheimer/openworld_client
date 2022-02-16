// Autogenerated 2/16/2022 10:48:30 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class Gem
  {
    public ItemCategory category;
    public string image;
    public int level;
    public string name;
    public GemRarity rarity;
    public int weight;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}