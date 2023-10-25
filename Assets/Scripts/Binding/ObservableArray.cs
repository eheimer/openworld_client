using System.Collections;
using System.Collections.Generic;
using Openworld.Binding;
using UnityEngine;

namespace Openworld.Binding
{
    public class ObservableArray<T> : ObservableObject where T : ObservableObject
    {
        private T[] items;
        public T[] Items
        {
            get => items;
            set
            {
                Set<T[]>(ref items, value);
            }
        }
    }
}