// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class Skill : ObservableObject
  {
    private string _description;
    public string description
    {
      get => _description;
      set => Set(ref _description, value);
    }

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
  }
}