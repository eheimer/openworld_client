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
    public event Action FormSuccess;
    public event Action<Exception> FormFail;

    protected abstract void ClearForm();

    protected override void Prep()
    {
      ClearForm();
    }

    protected override void HandleClick(string selector, Action method)
    {
      GetVisualElement().Q<Button>(selector).clickable.clicked += method;
    }

    protected void RaiseSuccess()
    {
      FormSuccess?.Invoke();
    }

    protected void RaiseFail(Exception ex)
    {
      FormFail?.Invoke(ex);
    }

  }
}
