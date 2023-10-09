using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Openworld.Menus
{

  public abstract class UIBase : MonoBehaviour
  {

    [SerializeField]
    UIDocument mainMenu;

    /// <summary>
    /// <c>HideAllDocuments</c> hides all UIDocuments.
    /// </summary>
    public void HideAllDocuments()
    {
      foreach (var ui in FindObjectsOfType<UIDocument>())
      {
        try
        {
          ui.rootVisualElement.visible = false;
        }
        catch (Exception e)
        {
          Debug.Log("[" + this.GetType().Name + "] HideAllDocuments: " + e.Message);
        }
      }
    }

    public void ShowDocument(UIDocument document)
    {
      this.gameObject.SetActive(true);
      HideAllDocuments();
      document.rootVisualElement.visible = true;
    }


    /// <summary>
    /// <c>ShowMenu</c> calls <c>MenuValidate</c> to determine if the menu should be shown.
    /// If it should, it hides all other menus and shows the main menu.
    /// If it should not, it calls <c>InvalidMenu</c> to handle the case of a validation fail.
    /// </summary>
    public void ShowMenu()
    {
      ShowDocument(mainMenu);
    }

    /// <summary>
    /// <c>CloseMenu</c> closes any menu panels that are open, and shows the menu button
    /// </summary>
    public void CloseMenu()
    {
      HideAllDocuments();
      ShowMenuButton();
    }

    protected void ShowMenuButton()
    {
      FindObjectOfType<MenuButton>().GetComponent<UIDocument>().rootVisualElement.visible = true;
    }

    protected void RaiseEvent(Action action)
    {
      Debug.Log("[" + this.GetType().Name + "] RaiseEvent: " + action?.Method.Name ?? "null");
      UnsubscribeAllFormEvents();
      action?.Invoke();
    }

    protected void RaiseEvent(Action<Exception> action, Exception ex)
    {
      // get the name of the deriving class
      Debug.Log("[" + this.GetType().Name + "] RaiseEvent: " + action?.Method.Name ?? "null, " + ex.Message ?? "null");
      UnsubscribeAllFormEvents();
      action?.Invoke(ex);
    }

    /**
     * <summary>
     * <c>UnsubscribeAllFormEvents</c> implement this method to clean up event subscriptions
     * </summary>
     */
    protected virtual void UnsubscribeAllFormEvents() { }
  }
}