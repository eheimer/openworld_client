using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using Openworld.Binding;
using Openworld.Models;
using UnityEngine;
namespace Openworld.Scenes
{
    public class CharacterInventoryPanel : ObservableMonoBehaviour, IBindingProvider
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

        public ObservableObject GetBindingSource(string sourcetype)
        {
            if (sourcetype == "CharacterDetail") return FindObjectOfType<GameManager>().character;
            if (sourcetype == "Inventory") return FindObjectOfType<GameManager>().character?.inventory;
            return null;
        }

        public Type[] provides()
        {
            return new Type[] { typeof(CharacterDetail), typeof(Inventory) };
        }
    }

}