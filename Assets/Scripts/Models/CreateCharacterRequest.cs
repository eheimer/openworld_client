// Autogenerated 5/10/2022 2:34:36 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class CreateCharacterRequest : ObservableObject
  {
    private int _dexterity;
    public int dexterity {
      get => _dexterity;
      set => Set(ref _dexterity, value);
    }

    private int _intelligence;
    public int intelligence {
      get => _intelligence;
      set => Set(ref _intelligence, value);
    }

    private int _movement;
    public int movement {
      get => _movement;
      set => Set(ref _movement, value);
    }

    private string _name;
    public string name {
      get => _name;
      set => Set(ref _name, value);
    }

    private int _strength;
    public int strength {
      get => _strength;
      set => Set(ref _strength, value);
    }


    public override string ToString(){
      return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    } 
  }
}