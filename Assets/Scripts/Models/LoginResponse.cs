// Autogenerated 2/15/2022 10:10:59 AM
using System;


namespace Openworld.Models
{
  [Serializable]
  public class LoginResponse
  {
    public string player;
    public string token;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}