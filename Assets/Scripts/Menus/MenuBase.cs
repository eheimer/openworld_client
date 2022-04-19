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
    private UIManagerBase ui;
    private VisualElement me;
    private GameManager gameManager;

    protected virtual void Start()
    {
      RegisterButtonHandlers();
    }

    protected GameManager GetGameManager(){
      if(gameManager == null){
        gameManager = FindObjectOfType<GameManager>();
      }
      return gameManager;
    }

    protected UIManagerBase getUI(){
      if(ui == null){
        ui = FindObjectOfType<UIManagerBase>(true);
        ui.gameObject.SetActive(true);
      }
      return ui;
    }

    protected VisualElement GetVisualElement(){
      if(me == null){
        me = this.GetComponent<UIDocument>().rootVisualElement;
      }
      return me;
    }

    public virtual void Show(){
      getUI().HideAllMenus();
      Prep();
      GetData();
      GetVisualElement().visible = true;
    }

    protected virtual void Prep() { }

    protected virtual void RegisterButtonHandlers() { }
    protected virtual void GetData() { }

    protected virtual void HandleClick(string selector, Action method){
      GetVisualElement().Q<Button>(selector).clickable.clicked += method;
    }

    public virtual void RequestException(RequestException err)
    {
      Error(UnityEngine.JsonUtility.FromJson<FailResponse>(err.Response).error.message);
    }

    protected virtual void Error(string message)
    {
      GetGameManager().LogMessage("Scene load error:", message);
    }
  }
}
