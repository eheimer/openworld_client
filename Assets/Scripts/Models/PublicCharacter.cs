// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class PublicCharacter : ObservableObject
  {
    private string _id;
    public string id
    {
      get => _id;
      set => Set(ref _id, value);
    }

    private string _name;
    public string name
    {
      get => _name;
      set => Set(ref _name, value);
    }

    private string _race;
    public string race
    {
      get => _race;
      set => Set(ref _race, value);
    }
  }
}