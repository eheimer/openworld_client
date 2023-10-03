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

  public enum PanelOrientation { Horizontal, Vertical }

  public abstract class SwipableScene : BaseScene
  {

    private Vector3 panelLocation;
    [SerializeField] float percentThreshold = 0.2f;
    [SerializeField] float easing = 0.5f;
    private int currentPage = 1;
    [SerializeField]
    protected GameObject[] panels;
    [SerializeField] PanelOrientation orientation;
    GameObject panelHolder;
    private float spacing;
    private Rect canvas;

    protected override void Start()
    {
      base.Start();
      // if (menu != null)
      // {
      //   // these event handlers show and hide the panelHolder when the menu is closed and opened
      //   menu.OnMenuOpen += HandleMenuOpen;
      //   menu.OnMenuClose += HandleMenuClose;
      // }

      panelHolder = orientation == PanelOrientation.Horizontal ? GameObject.Find("PanelHolder.Horizontal") : GameObject.Find("PanelHolder.Vertical");
      canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>().rect;

      if (panelHolder != null)
      {
        //panelHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(canvas.width, canvas.height);
        spacing = panelHolder.GetComponent<HorizontalOrVerticalLayoutGroup>().spacing;
        //arrange the panels within the panelHolder
        foreach (GameObject panel in panels)
        {
          var g = Instantiate(panel, panelHolder.transform);
          var r = g.GetComponent<RectTransform>();
          r.sizeDelta = new Vector2(canvas.width, canvas.height);
        }

        // store the positions of each of the panels
        var startPosition = panelHolder.transform.position;

        //position the panelHolder to display the first panel
        panelLocation = startPosition;

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
        menu.HideAllDocuments();
      }
      float difference;
      if (orientation == PanelOrientation.Horizontal)
      {
        difference = data.pressPosition.x - data.position.x;
        panelHolder.transform.position = panelLocation - new Vector3(difference, 0, 0);
      }
      else
      {
        difference = data.pressPosition.y - data.position.y;
        panelHolder.transform.position = panelLocation - new Vector3(0, difference, 0);
      }
    }

    public void OnEndDrag(PointerEventData data)
    {
      float percentage = orientation == PanelOrientation.Horizontal ? (data.pressPosition.x - data.position.x) / Screen.width : -(data.pressPosition.y - data.position.y) / Screen.height;
      if (Mathf.Abs(percentage) >= percentThreshold)
      {
        if (percentage > 0 && currentPage < panels.Length)
        {
          currentPage++;
        }
        else if (percentage < 0 && currentPage > 1)
        {
          currentPage--;
        }
        Vector3 newLocation = GetPanelLocation();
        StartCoroutine(SmoothMove(panelHolder.transform.position, newLocation, easing));
        panelLocation = newLocation;
      }
      else
      {
        StartCoroutine(SmoothMove(panelHolder.transform.position, panelLocation, easing));
      }
    }

    Vector3 GetPanelLocation()
    {
      //consider spacing as well
      if (orientation == PanelOrientation.Horizontal)
      {
        return new Vector3(-(Screen.width * (currentPage - 1) - Screen.width / 2), Screen.height / 2, 0);
      }
      else
      {
        return new Vector3(Screen.width / 2, (Screen.height * (currentPage - 1) + Screen.height / 2), 0);
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


    protected virtual void HandleMenuOpen(object sender, EventArgs a)
    {
      //hide the panelHolder
      panelHolder.SetActive(false);
    }

    protected virtual void HandleMenuClose(object sender, EventArgs a)
    {
      //show the panelHolder
      panelHolder.SetActive(true);
    }

    private void OnDestroy()
    {
      // if (menu != null)
      // {
      //   menu.OnMenuOpen -= HandleMenuOpen;
      //   menu.OnMenuClose -= HandleMenuClose;
      // }
    }
  }
}