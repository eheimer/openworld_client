using System;
using System.Collections;
using System.Collections.Generic;
using Openworld.Menus;
using Openworld.Models;
using Proyecto26;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Openworld.Scenes
{

  public abstract class SwipableScene : BaseScene
  {

    private Vector3 panelLocation;
    [SerializeField] float percentThreshold = 0.2f;
    [SerializeField] float easing = 0.5f;
    [SerializeField] int totalPages = 4;
    private int currentPage = 1;
    [SerializeField] GameObject panelHolder;

    public UIManagerBase menu;

    protected override void Start()
    {
      base.Start();
      if (panelHolder != null)
      {
        panelLocation = panelHolder.transform.position;
        //Add event handlers to the panel holder
        EventTrigger trigger = panelHolder.GetComponent<EventTrigger>();
        // drag event handler
        EventTrigger.Entry drag = new EventTrigger.Entry();
        drag.eventID = EventTriggerType.Drag;
        drag.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        trigger.triggers.Add(drag);
        // end drag event handler
        EventTrigger.Entry endDrag = new EventTrigger.Entry();
        endDrag.eventID = EventTriggerType.EndDrag;
        endDrag.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(endDrag);
      }
    }

    public void OnDrag(PointerEventData data)
    {
      if (menu != null)
      {
        menu.HideAllMenus();
      }
      float difference = data.pressPosition.x - data.position.x;
      panelHolder.transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
      float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
      if (Mathf.Abs(percentage) >= percentThreshold)
      {
        Vector3 newLocation = panelLocation;
        if (percentage > 0 && currentPage < totalPages)
        {
          currentPage++;
          newLocation += new Vector3(-Screen.width, 0, 0);
        }
        else if (percentage < 0 && currentPage > 1)
        {
          currentPage--;
          newLocation += new Vector3(Screen.width, 0, 0);
        }
        StartCoroutine(SmoothMove(panelHolder.transform.position, newLocation, easing));
        panelLocation = newLocation;
      }
      else
      {
        StartCoroutine(SmoothMove(panelHolder.transform.position, panelLocation, easing));
      }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
      float t = 0f;
      while (t <= 1.0)
      {
        t += Time.deltaTime / seconds;
        panelHolder.transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
        yield return null;
      }
      if (menu != null)
      {
        menu.CloseMenu();
      }
    }
  }
}