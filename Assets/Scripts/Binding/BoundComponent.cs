using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Openworld;
using TMPro;
using UnityEngine;

namespace Openworld.Binding
{
  /**
  ** A BoundComponent is an observer that binds a property on an ObservableObject
  ** to a target, usually a property of this component
  **/
  public abstract class BoundComponent : MonoBehaviour
  {
    [SerializeField] public string bindingSourceProperty; // the property on the bindingSource that we want to observe

    #region UNITY LIFECYCLE METHODS
    protected virtual void Start()
    {
      try
      {
        BindingProvider = GetBindingProvider();
        BindingSource = GetBindingSource();
        TargetComponent = GetTargetComponent();
        TargetProperty = TargetComponent?.GetType().GetProperty(BindingTargetProperty);
      }
      catch (Exception ex)
      {
        Debug.LogError("Error configuring binding in " + this.GetType() + ": " + ex.Message);
      }
    }
    #endregion

    #region IMPLEMENTABLE METHODS
    protected virtual UnityEngine.Component GetTargetComponent()
    {
      // by default we will get the target component from the children of this game object
      // override to implent a different way of getting the target component
      return gameObject.GetComponentInChildren(BindingTargetComponentType);
    }
    /**
    ** By default we will get the bindingProvider from a parent up the tree.  This
    ** can be overridden to specify a bindingProvider to use, or to use none. A
    ** bindingProvider is not required.
    **/
    protected virtual IBindingProvider GetBindingProvider()
    {
      return gameObject.GetComponentsInParent<IBindingProvider>(true)?[0];
    }

    /**
    ** By default we will get the bindingSource from the bindingProvider.  This
    ** can be overridden to specify a different bindingSource, if a bindingProvider
    ** is not used.
    **/
    public virtual ObservableObject GetBindingSource()
    {
      return BindingProvider.GetBindingSource();
    }

    public virtual PropertyInfo GetBindingSourceProperty()
    {
      return BindingSource.GetType().GetProperty(bindingSourceProperty, BindingFlags.Public | BindingFlags.Instance);
    }

    /**
    ** This is called when the observed property value changes, and the binding
    ** needs to be updated.  Override this to do something more complex
    **/
    protected virtual void UpdateBindingTarget()
    {
      if (TargetProperty != null)
      {
        TargetProperty.SetValue(TargetComponent, SourcePropertyValue);
      }
    }
    #endregion

    #region PROPERTIES
    protected object SourcePropertyValue { get; set; } // the value from the bindingSourceProperty
    protected UnityEngine.Component TargetComponent { get; set; } // the component that contains the property that we want to bind
    protected PropertyInfo TargetProperty { get; set; } // the property that we want to bind
    public System.Type BindingTargetComponentType { get; set; }
    public string BindingTargetProperty { get; set; }
    private IBindingProvider _bindingProvider;
    protected IBindingProvider BindingProvider
    {
      get => _bindingProvider;
      set
      {
        //detach event handler
        if (_bindingProvider != null)
        {
          _bindingProvider.PropertyChanged -= ProviderSourceChanged;
        }
        _bindingProvider = value;
        //attach event handler
        if (_bindingProvider != null)
        {
          _bindingProvider.PropertyChanged += ProviderSourceChanged;
        }
      }
    }

    private ObservableObject _bindingSource;
    protected ObservableObject BindingSource
    {
      get => _bindingSource;
      set
      {
        //detach event handler
        if (_bindingSource != null)
        {
          _bindingSource.PropertyChanged -= SourcePropertyChanged;
        }
        _bindingSource = value;
        //attach event handler
        if (_bindingSource != null)
        {
          _bindingSource.PropertyChanged += SourcePropertyChanged;
        }
      }
    }
    #endregion

    #region EVENT HANDLERS
    /**
    ** This is called when the observed property value changes
    **/
    private void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == bindingSourceProperty)
      {
        //Debug.Log("BindingSource Property Changed: " + e.PropertyName);
        UpdateBindingSourcePropertyValue();
      }
    }

    /**
    ** This is called when the bindingProvider source changes, and the binding needs to be updated
    **/
    private void ProviderSourceChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "bindingSource")
      {
        BindingSource = GetBindingSource();
        // set the value immediately
        UpdateBindingSourcePropertyValue();
      }
    }

    private void UpdateBindingSourcePropertyValue()
    {
      var sourceProperty = GetBindingSourceProperty();
      if (sourceProperty != null)
      {
        SourcePropertyValue = sourceProperty.GetValue(BindingSource);
      }
      UpdateBindingTarget();
    }
  }
  #endregion
}