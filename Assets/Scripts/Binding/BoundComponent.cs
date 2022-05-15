using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Openworld;
using TMPro;
using UnityEngine;

namespace Openworld.Binding
{
  public abstract class BoundComponent : MonoBehaviour
  {
    [SerializeField] string bindingSourceProperty; // the property on the bindingSource that we want to observe
    private IBindingProvider bindingProvider;

    void Start()
    {
      bindingProvider = gameObject.GetComponentsInParent<IBindingProvider>()?[0];
      if(bindingProvider != null){
        bindingProvider.PropertyChanged += SourceChanged;
      }
      RefreshBinding();
    }

    protected void AttachBindingSource()
    {
      bindingSource = bindingProvider.GetBindingSource();
      bindingSource.PropertyChanged += SourcePropertyChanged;
    }

    private ObservableObject bindingSource;
    protected ObservableObject GetBindingSource()
    {
      if (bindingSource == null)
      {
        AttachBindingSource();
        if (bindingSource != null)
        {
          UpdateTargetValue();
        }
      }
      return bindingSource;
    }

    public void RefreshBinding()
    {
      if (bindingSource != null)
      {
        bindingSource.PropertyChanged -= SourcePropertyChanged;
        bindingSource = null;
      }
      AttachBindingSource();
    }

    protected abstract System.Type GetBindingTargetComponentType();
    protected abstract string GetBindingTargetProperty();

    protected UnityEngine.Component GetTargetComponent()
    {
      return gameObject.GetComponent(GetBindingTargetComponentType());
    }
    protected PropertyInfo GetTargetProperty()
    {
      return GetTargetComponent().GetType().GetProperty(GetBindingTargetProperty());
    }
    protected System.Type GetTargetPropertyType()
    {
      return GetTargetProperty().GetGetMethod().ReturnType;
    }
    protected object GetSourcePropertyValue()
    {
      return GetBindingSource().GetType().GetProperty(bindingSourceProperty, BindingFlags.Public | BindingFlags.Instance).GetValue(GetBindingSource());
    }
    protected void UpdateTargetValue()
    {
      Debug.Log("TargetComponent: " + GetTargetComponent());
      Debug.Log("BindingTargetProperty: " + GetBindingTargetProperty());
      Debug.Log("TargetProperty: " + GetTargetProperty());
      Debug.Log("SourcePropertyValue: " + GetSourcePropertyValue());
      GetTargetProperty().SetValue(GetTargetComponent(), GetSourcePropertyValue());
    }

    public virtual void SourcePropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == bindingSourceProperty)
      {
        Debug.Log("SourcePropertyChanged: " + e.PropertyName);
        var target = gameObject.GetComponent(GetBindingTargetComponentType());
        if (target != null)
        {
          target.GetType().GetProperty(GetBindingTargetProperty())
            .SetValue(target,
              sender.GetType().GetProperty(e.PropertyName, BindingFlags.Public | BindingFlags.Instance).GetValue(sender)
            );
        }
      }
    }
    public virtual void SourceChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "bindingSource")
      {
        Debug.Log("SourceChanged: " + e.PropertyName);
        RefreshBinding();
        UpdateTargetValue();
      }
    }
  }
}