// Autogenerated 5/11/2022 9:44:27 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;

namespace Openworld.Models
{
  public class InvitePlayerRequest : ObservableObject
  {
    private string _email;
    public string email {
      get => _email;
      set => Set(ref _email, value);
    }
  }
}