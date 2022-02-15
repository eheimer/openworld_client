// Autogenerated 2/15/2022 10:30:15 AM
using System;

namespace Openworld.Models
{
  [Serializable]
  public class User
  {
    public Character[] characters;
    public Game currentGame;
    public string email;
    public Game[] games;
    public bool isAdmin;
    public DateTime lastSeenAt;
    public string name;
    public string password;

    public override string ToString(){
      return UnityEngine.JsonUtility.ToJson (this, true);
    } 
  }
}