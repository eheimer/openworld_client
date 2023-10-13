using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class RacesResponse : ObservableObject
  {
    private int _id;
    public int id
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

    private string _description;
    public string description
    {
      get => _description;
      set => Set(ref _description, value);
    }

    private Skill[] _skills;
    public Skill[] skills
    {
      get => _skills;
      set => Set(ref _skills, value);
    }
  }
}