// Autogenerated 2/19/2022 5:42:23 PM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class FailResponse
  {
    public Error error;
    public bool success;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}