using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Openworld.Binding
{
  public interface IObservable : INotifyPropertyChanged, INotifyPropertyChanging
  {
    void RaisePropertyChanging(string propertyName);
    void RaisePropertyChanged(string propertyName);
    void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression);
    void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression);
    void RemoveAllHandlers();
  }
}