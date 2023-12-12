using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Openworld.Binding
{

  /**
  ** A binding source provider must observe the binding source defined in
  ** the GetBindingSource() method, and raise a PropertyChangedEvent when
  ** it changes, so that observers know to re-attach the binding source.
 */
  public interface IBindingProvider : IObservable
  {
    public ObservableObject GetBindingSource(string sourcetype);
    public Type[] provides();
  }
}