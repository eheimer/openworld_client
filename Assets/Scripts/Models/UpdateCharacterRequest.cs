// Autogenerated 2/19/2022 5:42:23 PM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class UpdateCharacterRequest
  {
    public int baseResist;
    public int inventorySize;
    public int maxHp;
    public string name;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}