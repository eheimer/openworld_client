using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public abstract class FormBase : MenuBase
  {

    protected abstract void ClearForm();

    protected override void Prep()
    {
      ClearForm();
    }

    protected override void HandleClick(string selector, Action method){
      me.Q<Button>(selector).clickable.clicked += method;
    }
  }
}
