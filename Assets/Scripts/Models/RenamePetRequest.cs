// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class RenamePetRequest : ObservableObject
  {
    private string _name;
    public string name {
      get => _name;
      set => Set(ref _name, value);
    }
  }
}