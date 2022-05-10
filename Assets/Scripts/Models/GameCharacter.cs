// Autogenerated 5/10/2022 2:34:36 PM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class GameCharacter : ObservableObject
  {
    private PublicCharacter _character;
    public PublicCharacter character {
      get => _character;
      set => Set(ref _character, value);
    }

    private Game _game;
    public Game game {
      get => _game;
      set => Set(ref _game, value);
    }

    private bool _owner;
    public bool owner {
      get => _owner;
      set => Set(ref _owner, value);
    }


    public override string ToString(){
      return Newtonsoft.Json.JsonConvert.SerializeObject(this);
    } 
  }
}