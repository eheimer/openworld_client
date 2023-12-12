using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Openworld.Binding;
using Openworld.Models;
using UnityEngine;

namespace Openworld.Scenes
{
  public class CharacterOverviewPanel : ObservableMonoBehaviour, IBindingProvider
  {
    void Awake()
    {
      GameManager gm = FindObjectOfType<GameManager>();
      if (gm != null)
      {
        gm.PropertyChanged += GameManagerPropertyChanged;
      }
    }

    private void GameManagerPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      if (args.PropertyName == "character")
      {
        RaisePropertyChanged("bindingSource");
      }
    }

    public ObservableObject GetBindingSource(string source = null)
    {
      return FindObjectOfType<GameManager>().character;
    }

    public Type[] provides()
    {
      return new Type[] { typeof(CharacterDetail) };
    }
  }
}