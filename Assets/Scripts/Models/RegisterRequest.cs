// Autogenerated 5/12/2022 7:01:03 AM
//   by GenerateClientEntities. DO NOT MODIFY
using System;
using Openworld.Binding;

namespace Openworld.Models
{
  public class RegisterRequest : ObservableObject
  {
    private string _email;
    public string email
    {
      get => _email;
      set => Set(ref _email, value);
    }

    private string _username;
    public string username
    {
      get => _username;
      set => Set(ref _username, value);
    }

    private string _password;
    public string password
    {
      get => _password;
      set => Set(ref _password, value);
    }
  }
}