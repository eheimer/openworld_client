using UnityEngine;
using UnityEngine.UI;
using Proyecto26;
using Openworld.Models;
using Openworld.Scenes;

namespace Openworld.Forms
{
  public abstract class BaseForm : BaseScene
  {
    [SerializeField] protected Text formError;

    protected override void Start()
    {
      base.Start();
      formError.enabled = false;
    }
    protected abstract void DoSubmit();

    public void Submit()
    {
      formError.enabled = false;
      DoSubmit();
    }

    protected override void Error(string message)
    {
      base.Error(message);
      formError.text = message;
      formError.enabled = true;
    }

    protected override void GetData() { }
  }
}