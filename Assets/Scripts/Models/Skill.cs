// Autogenerated 4/8/2022 6:02:31 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  [Serializable]
  public class Skill
  {
    public string description;
    public string id;
    public string name;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}