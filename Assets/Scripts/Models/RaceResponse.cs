using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class RacesResponse : ObservableObject
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
  }
}