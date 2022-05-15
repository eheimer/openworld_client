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
    // protected override string GetBindingTargetProperty()
    protected override string GetBindingTargetProperty()
    {
      return "text";
    }
    // protected override System.Type GetBindingTargetComponentType(){
    protected override System.Type GetBindingTargetComponentType()
    {
      return typeof(TMP_Text);
    }
  }
}
