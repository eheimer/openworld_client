using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace Openworld.Binding
{
  public class BoundTextComponent : BoundComponent
  {
    public BoundTextComponent(){
      BindingTargetComponentType = typeof(TMP_Text);
      BindingTargetProperty = "text";
    }

    protected override void UpdateBindingTarget(){
      if (TargetProperty != null && SourcePropertyValue != null)
      {
        TargetProperty.SetValue(TargetComponent, SourcePropertyValue.ToString());
      }
    }
  }
}
