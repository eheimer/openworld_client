using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Openworld;
using TMPro;
using Unity.Properties;
using UnityEngine;

namespace Openworld.Binding
{
  /**
  ** A BoundComponent is an observer that binds a property on an ObservableObject
  ** to a target, usually a property of this component
  **/
  public abstract class BoundComponent<T> : MonoBehaviour where T : UnityEngine.Component
  {
    // the bindingSourceProperty should be a string formatted like "<ObservableObject>.<property>"
    // in the future, we may implement nested properties several levels deep, but for now it is just one level
    [SerializeField()] string bindingSource; // the property on the bindingSource that we want to observe

    #region UNITY LIFECYCLE METHODS
    protected virtual void Start()
    {
      try
      {
        BindingProvider = FindBindingProvider();
        BindingSource = GetBindingSource();
        TargetComponent = GetTargetComponent();
        TargetProperty = TargetComponent?.GetType().GetProperty(BindingTargetProperty);
        // set the value immediately if possible
        if (BindingSource != null)
        {
          UpdateBindingSourcePropertyValue();
        }
      }
      catch (Exception ex)
      {
        Debug.LogError("Error configuring binding in " + this.GetType() + ": " + ex.Message);
      }
    }
    #endregion

    #region IMPLEMENTABLE METHODS
    protected virtual T GetTargetComponent()
    {
      // by default we will get the target component from the children of this game object
      // override to implent a different way of getting the target component
      return gameObject.GetComponentInChildren<T>();
    }
    /**
    ** By default we will get the bindingProvider from a parent up the tree.  This
    ** can be overridden to specify a bindingProvider to use, or to use none. A
    ** bindingProvider is not required.
    **/
    protected virtual IBindingProvider FindBindingProvider()
    {
      IBindingProvider[] providers = gameObject.GetComponentsInParent<IBindingProvider>(true);
      foreach (IBindingProvider provider in providers)
      {
        foreach (var t in provider.provides())
        {
          if (t.Name == BindingSourceContainer) return provider;
          if (t.IsGenericType && t.GetGenericTypeDefinition().Name.Split('`')[0] == BindingSourceContainer.Split('<', '>')[0])
          {
            var genericType = t.GetGenericArguments()[0];
            if (genericType.Name == BindingSourceContainer.Split('<', '>')[1])
            {
              return provider;
            }
          }
        }
      }
      return null;
    }

    /**
    ** By default we will get the bindingSource from the bindingProvider.  This
    ** can be overridden to specify a different bindingSource, if a bindingProvider
    ** is not used.
    **/
    private ObservableObject GetBindingSource()
    {
      return BindingProvider.GetBindingSource(BindingSourceContainer);
    }

    public virtual PropertyInfo GetBindingSourceProperty()
    {
      return BindingSource.GetType().GetProperty(BindingSourcePropertyName, BindingFlags.Public | BindingFlags.Instance);
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
    protected T TargetComponent { get; set; } // the component that contains the property that we want to bind
    protected PropertyInfo TargetProperty { get; set; } // the property that we want to bind
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
      if (e.PropertyName == BindingSourcePropertyName)
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
      if (sourceProperty != null && BindingSource != null)
      {
        SourcePropertyValue = sourceProperty.GetValue(BindingSource);
        UpdateBindingTarget();
      }
    }

    public void SetBindingSource(string bindingSource)
    {
      this.bindingSource = bindingSource;
      _bindingSourceContainer = null;
      _bindingSourcePropertyName = null;
    }

    string _bindingSourceContainer;
    protected string BindingSourceContainer
    {
      get
      {
        if (_bindingSourceContainer == null)
        {
          string[] bindingParts = bindingSource.Split('.');
          if (bindingParts.Length != 2)
          {
            Debug.LogWarning("Binding source property must be in the format <ObservableObject>.<property> for " + this.GetType());
            return null;
          }
          _bindingSourceContainer = bindingParts[0];
        }
        return _bindingSourceContainer;
      }
      set
      {
        _bindingSourceContainer = value;
      }
    }

    string _bindingSourcePropertyName;
    protected string BindingSourcePropertyName
    {
      get
      {
        if (_bindingSourcePropertyName == null)
        {
          string[] bindingParts = bindingSource.Split('.');
          if (bindingParts.Length != 2)
          {
            Debug.LogWarning("Binding source property must be in the format <ObservableObject>.<property> for " + this.GetType());
            return null;
          }
          _bindingSourcePropertyName = bindingParts[1];
        }
        return _bindingSourcePropertyName;
      }
      set
      {
        _bindingSourcePropertyName = value;
      }
    }
  }
  #endregion
}