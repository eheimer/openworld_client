// Autogenerated 5/11/2022 9:44:27 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class ItemDamageResponse : ObservableObject
  {
    private ItemDamageType _result;
    public ItemDamageType result {
      get => _result;
      set => Set(ref _result, value);
    }
  }
}