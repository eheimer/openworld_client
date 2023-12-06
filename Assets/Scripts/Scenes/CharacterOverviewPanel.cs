using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Openworld.Binding;
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

    public ObservableObject GetBindingSource()
    {
      return FindObjectOfType<GameManager>().character;
    }
  }
}