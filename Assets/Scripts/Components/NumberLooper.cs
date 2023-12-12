using System;
using System.Reflection;
using Openworld.Binding;
using TMPro;
using UnityEngine;

namespace Openworld
{
  [Obsolete("This class and any components that reference it are not currently in use.  It will require some refactoring to get it to work with the new binding system")]
  public class NumberLooper : MonoBehaviour
  {
    [SerializeField] BoundTextComponent boundComponent;
    [SerializeField] string boundField;
    [SerializeField] bool showName;
    [SerializeField] string statName;
    [SerializeField] TMP_Text statNameComponent;
    [SerializeField] int minValue = 0;
    [SerializeField] int maxValue;
    [SerializeField] bool loopUp = false;
    [SerializeField] bool loopDown = false;
    [SerializeField] bool showIncreaseButton = true;
    [SerializeField] GameObject increaseButton;
    [SerializeField] bool showDecreaseButton = true;
    [SerializeField] GameObject decreaseButton;

    PropertyInfo bindingSourceProperty;

    void Start()
    {
      // if (boundComponent != null)
      // {
      //   boundComponent.bindingSource = boundField;
      // }
      // if (statNameComponent != null)
      // {
      //   statNameComponent.text = statName;
      //   statNameComponent.gameObject.SetActive(showName);
      // }
      // SetButtonDisplay(minValue);
    }

    public void Initialize(bool showStatName, string statName, string boundField, int minValue, int maxValue, bool loopUp, bool loopDown, bool showIncrease, bool showDecrease)
    {
      // if (statNameComponent != null)
      // {
      //   statNameComponent.text = statName;
      //   statNameComponent.gameObject.SetActive(showStatName);
      // }
      // if (boundComponent != null)
      // {
      //   boundComponent.bindingSource = boundField;
      // }
      // this.minValue = minValue;
      // this.maxValue = maxValue;
      // this.loopUp = loopUp;
      // this.loopDown = loopDown;
      // this.showIncreaseButton = showIncrease;
      // this.showDecreaseButton = showDecrease;
      // SetButtonDisplay(minValue);
    }

    private int GetCurrentValue()
    {
      return 0;
      //return int.Parse(boundComponent.GetBindingSourceProperty().GetValue(boundComponent.GetBindingSource()).ToString());
    }

    private void SetButtonDisplay(int value)
    {
      decreaseButton?.SetActive(showDecreaseButton && (loopDown || value > minValue));
      increaseButton?.SetActive(showIncreaseButton && (loopUp || value < maxValue));
    }

    public void Increase()
    {
      var value = GetCurrentValue() + 1;
      if (value > maxValue)
      {
        if (loopUp)
        {
          value = minValue;
        }
        else
        {
          value = maxValue;
        }
      }
      SetButtonDisplay(value);
      // boundComponent.GetBindingSourceProperty().SetValue(boundComponent.GetBindingSource(), value);
    }

    public void Decrease()
    {
      var value = GetCurrentValue() - 1;
      if (value < minValue)
      {
        if (loopDown)
        {
          value = maxValue;
        }
        else
        {
          value = minValue;
        }
      }
      SetButtonDisplay(value);
      // boundComponent.GetBindingSourceProperty().SetValue(boundComponent.GetBindingSource(), value);
    }
  }
}