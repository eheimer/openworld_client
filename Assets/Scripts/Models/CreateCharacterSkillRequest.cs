// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class CreateCharacterSkillRequest : ObservableObject
  {
    private string _skillId;
    public string skillId {
      get => _skillId;
      set => Set(ref _skillId, value);
    }
  }
}