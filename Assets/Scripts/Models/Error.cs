// Autogenerated 2/19/2022 5:42:23 PM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class Error
  {
    public string message;
    public string type;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}