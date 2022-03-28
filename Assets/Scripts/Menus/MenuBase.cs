using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.UIElements;

namespace Openworld.Menus
{
  public class MenuBase : MonoBehaviour
  {
    protected UIManagerBase ui;
    protected VisualElement me;
    protected Communicator communicator;
    protected GameManager gameManager;

    protected virtual void Start()
    {
      ui = FindObjectOfType<UIManagerBase>();
      gameManager = FindObjectOfType<GameManager>();
      me = this.GetComponent<UIDocument>().rootVisualElement;
      communicator = FindObjectOfType<Communicator>();
      RegisterButtonHandlers();
    }

    public virtual void Show(){
      ui.HideAllMenus();
      Prep();
      GetData();
      me.visible = true;
    }

    protected virtual void Prep() { }

    protected virtual void RegisterButtonHandlers() { }
    protected virtual void GetData() { }

    protected virtual void HandleClick(string selector, Action method){
      me.Q<Button>(selector).clickable.clicked += method;
    }

    public virtual void RequestException(RequestException err)
    {
      Error(UnityEngine.JsonUtility.FromJson<FailResponse>(err.Response).error.message);
    }

    protected virtual void Error(string message)
    {
      gameManager.LogMessage("Scene load error:", message);
    }
  }
}
