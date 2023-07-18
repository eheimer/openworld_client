using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public abstract class UIManagerBase : MonoBehaviour
  {
    public event EventHandler OnMenuOpen;
    public event EventHandler OnMenuClose;

    [SerializeField]
    UIDocument mainMenu;

    /// <summary>
    /// <c>HideAllDocuments</c> hides all UIDocuments.
    /// </summary>
    public void HideAllDocuments(bool raiseEvent = true)
    {
      foreach (var ui in FindObjectsOfType<UIDocument>())
      {
        try
        {
          ui.rootVisualElement.visible = false;
        }
        catch (Exception e)
        {
        }
      }
      if (raiseEvent)
      {
        OnRaiseDocumentCloseEvent();
      }
    }

    public void ShowDocument(UIDocument document)
    {
      this.gameObject.SetActive(true);
      HideAllDocuments(false);
      document.rootVisualElement.visible = true;
      OnRaiseDocumentOpenEvent();
    }

    /// <summary>
    /// <c>MenuValidate</c> validates game state for the menu.
    /// </summary>
    protected virtual bool MenuValidate()
    {
      return true;
    }

    /// <summary>
    /// <c>InvalidMenu</c> handles menu display in case of validation fail.
    /// </summary>
    protected virtual void InvalidMenu() { }

    /// <summary>
    /// <c>ShowMenu</c> calls <c>MenuValidate</c> to determine if the menu should be shown.
    /// If it should, it hides all other menus and shows the main menu.
    /// If it should not, it calls <c>InvalidMenu</c> to handle the case of a validation fail.
    /// </summary>
    public void ShowMenu()
    {
      if (MenuValidate())
      {
        ShowDocument(mainMenu);
      }
      else
      {
        InvalidMenu();
      }
    }

    /// <summary>
    /// <c>CloseMenu</c> closes any menu panels that are open, and shows the menu button
    /// </summary>
    public void CloseMenu()
    {
      HideAllDocuments();
      FindObjectOfType<BaseScene>().ShowMenuButton();
    }

    protected virtual void OnRaiseDocumentOpenEvent()
    {
      EventHandler raiseEvent = OnMenuOpen;
      if (raiseEvent != null)
      {
        raiseEvent(this, EventArgs.Empty);
      }
    }

    protected virtual void OnRaiseDocumentCloseEvent()
    {
      EventHandler raiseEvent = OnMenuClose;
      if (raiseEvent != null)
      {
        raiseEvent(this, EventArgs.Empty);
      }
    }
  }
}