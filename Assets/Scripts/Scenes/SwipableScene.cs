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
    private int currentPage = 1;
    [SerializeField] GameObject panelHolder;
    [SerializeField] GameObject[] panels;
    private List<Rect> panelSizes = new List<Rect>();
    private List<Vector3> panelPositions = new List<Vector3>();
    private float spacing;

    protected override void Start()
    {
      base.Start();
      if (panelHolder != null)
      {
        spacing = panelHolder.GetComponent<HorizontalLayoutGroup>().spacing;
        var canvas = GetComponent<Canvas>();
        //arrange the panels within the panelHolder
        foreach (GameObject panel in panels)
        {
          var g = Instantiate(panel, panelHolder.transform);
          var r = g.GetComponent<RectTransform>();
          panelSizes.Add(RectTransformUtility.PixelAdjustRect(r, canvas));
        }

        // store the positions of each of the panels
        var tPos = new Vector3(0 - Math.Abs(Screen.width - panelSizes[0].width) / 2, Math.Abs(Screen.height - panelSizes[0].height) / 2,0);
        var startPosition = panelHolder.transform.position + tPos;
        Debug.Log("0 : " + startPosition);
        panelPositions.Add(startPosition);

        for (var i = 1; i < panels.Length; i++){
          // calculate transform = previous panel's width plus spacing, plus adjustment for current panel's width vs screen width
          tPos = new Vector3(0 - Math.Abs(Screen.width - panelSizes[i].width) / 2 - panelSizes[i-1].width + spacing/2,0,0);
          var pos = panelPositions[i - 1] + tPos;
          Debug.Log(i + " : " + pos);
          panelPositions.Add(panelPositions[i - 1] + tPos);
        }

        //position the panelHolder to display the first panel
        panelHolder.transform.position = panelPositions[0];
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
        if (percentage > 0 && currentPage < panels.Length)
        {
          currentPage++;
        }
        else if (percentage < 0 && currentPage > 1)
        {
          currentPage--;
        }
        Vector3 newLocation = panelPositions[currentPage - 1];
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