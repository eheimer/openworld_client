// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class UpdateCharacterSkillRequest : ObservableObject
  {
    private int _level;
    public int level {
      get => _level;
      set => Set(ref _level, value);
    }
  }
}