// Autogenerated 2/25/2022 10:41:46 PM
//   by GenerateClientEntities. DO NOT MODIFY
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