// Autogenerated 5/10/2022 2:34:36 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class MonsterRequest : ObservableObject
  {
    private int _monsterId;
    public int monsterId {
      get => _monsterId;
      set => Set(ref _monsterId, value);
    }


    public override string ToString(){
      return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    } 
  }
}