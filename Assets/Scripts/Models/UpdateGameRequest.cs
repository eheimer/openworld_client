// Autogenerated 5/11/2022 9:44:27 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class UpdateGameRequest : ObservableObject
  {
    private int _maxPlayers;
    public int maxPlayers {
      get => _maxPlayers;
      set => Set(ref _maxPlayers, value);
    }

    private string _name;
    public string name {
      get => _name;
      set => Set(ref _name, value);
    }
  }
}