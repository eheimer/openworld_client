// ****************************************************************************
// <copyright file="ObservableObject.cs" company="GalaSoft Laurent Bugnion">
// Copyright © GalaSoft Laurent Bugnion 2011-2016
// </copyright>
// ****************************************************************************
// <author>Laurent Bugnion</author>
// <email>laurent@galasoft.ch</email>
// <date>10.4.2011</date>
// <project>GalaSoft.MvvmLight.Messaging</project>
// <web>http://www.mvvmlight.net</web>
// <license>
// See license.txt in this project or http://www.galasoft.ch/license_MIT.txt
// </license>
// ****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Openworld.Binding
{
  /// <summary>
  /// An ObservableMonoBehaviour is a Unity MonoBehaviour that is observable
  /// </summary>
  public class ObservableMonoBehaviour : MonoBehaviour, IObservable
  {
    public event PropertyChangedEventHandler PropertyChanged;
    protected PropertyChangedEventHandler PropertyChangedHandler()
    {
      return PropertyChanged;
    }
    public event PropertyChangingEventHandler PropertyChanging;
    protected PropertyChangingEventHandler PropertyChangingHandler()
    {
      return PropertyChanging;
    }

    /// <summary>
    /// Raises the PropertyChanging event if needed.
    /// </summary>
    /// <remarks>If the propertyName parameter
    /// does not correspond to an existing property on the current class, an
    /// exception is thrown in DEBUG configuration only.</remarks>
    /// <param name="propertyName">(optional) The name of the property that
    /// changed.</param>
    public virtual void RaisePropertyChanging(
        [CallerMemberName] string propertyName = null)
    {
      //VerifyPropertyName(propertyName);

      var handler = PropertyChanging;
      if (handler != null)
      {
        handler(this, new PropertyChangingEventArgs(propertyName));
      }
    }

    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <remarks>If the propertyName parameter
    /// does not correspond to an existing property on the current class, an
    /// exception is thrown in DEBUG configuration only.</remarks>
    /// <param name="propertyName">(optional) The name of the property that
    /// changed.</param>
    public virtual void RaisePropertyChanged(
        [CallerMemberName] string propertyName = null)
    {
      //VerifyPropertyName(propertyName);

      var handler = PropertyChanged;
      if (handler != null)
      {
        handler(this, new PropertyChangedEventArgs(propertyName));
      }
    }

    /// <summary>
    /// Raises the PropertyChanging event if needed.
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changes.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changes.</param>
    public virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
    {
      var handler = PropertyChanging;
      if (handler != null)
      {
        var propertyName = GetPropertyName(propertyExpression);
        handler(this, new PropertyChangingEventArgs(propertyName));
      }
    }

    /// <summary>
    /// Raises the PropertyChanged event if needed.
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changed.</param>
    public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
    {
      var handler = PropertyChanged;

      if (handler != null)
      {
        var propertyName = GetPropertyName(propertyExpression);

        if (!string.IsNullOrEmpty(propertyName))
        {
          // ReSharper disable once ExplicitCallerInfoArgument
          RaisePropertyChanged(propertyName);
        }
      }
    }

    /// <summary>
    /// Extracts the name of a property from an expression.
    /// </summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="propertyExpression">An expression returning the property's name.</param>
    /// <returns>The name of the property returned by the expression.</returns>
    /// <exception cref="ArgumentNullException">If the expression is null.</exception>
    /// <exception cref="ArgumentException">If the expression does not represent a property.</exception>
    protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
    {
      if (propertyExpression == null)
      {
        throw new ArgumentNullException("propertyExpression");
      }

      var body = propertyExpression.Body as MemberExpression;

      if (body == null)
      {
        throw new ArgumentException("Invalid argument", "propertyExpression");
      }

      var property = body.Member as PropertyInfo;

      if (property == null)
      {
        throw new ArgumentException("Argument is not a property", "propertyExpression");
      }

      return property.Name;
    }

    /// <summary>
    /// Assigns a new value to the property. Then, raises the
    /// PropertyChanged event if needed. 
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyExpression">An expression identifying the property
    /// that changed.</param>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    /// <returns>True if the PropertyChanged event has been raised,
    /// false otherwise. The event is not raised if the old
    /// value is equal to the new value.</returns>
    protected bool Set<T>(
        Expression<Func<T>> propertyExpression,
        ref T field,
        T newValue)
    {
      if (EqualityComparer<T>.Default.Equals(field, newValue))
      {
        return false;
      }

      RaisePropertyChanging(propertyExpression);
      field = newValue;
      RaisePropertyChanged(propertyExpression);
      return true;
    }

    /// <summary>
    /// Assigns a new value to the property. Then, raises the
    /// PropertyChanged event if needed. 
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="propertyName">The name of the property that
    /// changed.</param>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    /// <returns>True if the PropertyChanged event has been raised,
    /// false otherwise. The event is not raised if the old
    /// value is equal to the new value.</returns>
    protected bool Set<T>(
        string propertyName,
        ref T field,
        T newValue)
    {
      if (EqualityComparer<T>.Default.Equals(field, newValue))
      {
        return false;
      }

      RaisePropertyChanging(propertyName);
      field = newValue;

      RaisePropertyChanged(propertyName);

      return true;
    }

    /// <summary>
    /// Assigns a new value to the property. Then, raises the
    /// PropertyChanged event if needed. 
    /// </summary>
    /// <typeparam name="T">The type of the property that
    /// changed.</typeparam>
    /// <param name="field">The field storing the property's value.</param>
    /// <param name="newValue">The property's value after the change
    /// occurred.</param>
    /// <param name="propertyName">(optional) The name of the property that
    /// changed.</param>
    /// <returns>True if the PropertyChanged event has been raised,
    /// false otherwise. The event is not raised if the old
    /// value is equal to the new value.</returns>
    protected bool Set<T>(
        ref T field,
        T newValue,
        [CallerMemberName] string propertyName = null)
    {
      return Set(propertyName, ref field, newValue);
    }

    public void RemoveAllHandlers(){
      foreach (var handler in PropertyChanged.GetInvocationList())
      {
        PropertyChanged -= (PropertyChangedEventHandler)handler;
      }
      foreach (var handler in PropertyChanging.GetInvocationList())
      {
          PropertyChanging -= (PropertyChangingEventHandler)handler;
      }
    }
  }
}