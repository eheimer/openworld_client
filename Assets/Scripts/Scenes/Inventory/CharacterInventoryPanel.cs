using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Openworld.Binding;
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
            if (args.PropertyName == "inventory")
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