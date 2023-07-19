using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatingDisplay : MonoBehaviour
{
  [SerializeField]
  private Image spinner;

  private float minPaddingPercent = .05f;
  private float panelPaddingPercent = .05f;
  private int maxSpinnerSizePixels = 234; // spinner should be square
  private Vector2 lastPanelSize = new Vector2(0, 0);
  private Vector2 lastParentSize = new Vector2(0, 0);
  private Vector2 lastParentPosition = new Vector2(0, 0);

  void Update()
  {
    RectTransform parentTransform = transform.parent.GetComponent<RectTransform>();
    Vector2 parentSize = parentTransform.sizeDelta;
    Vector2 parentPosition = parentTransform.localPosition;
    Vector2 panelSize = transform.GetComponent<RectTransform>().sizeDelta;
    if (parentSize != lastParentSize || parentPosition != lastParentPosition)
    {
      lastParentSize = parentSize;
      lastParentPosition = parentPosition;
      //resize the panel to fit the parent, padding size should be uniform for x and y
      float pad = Mathf.Min(parentSize.x, parentSize.y) * panelPaddingPercent;
      panelSize = new Vector2(parentSize.x - pad, parentSize.y - pad);
      RectTransform rectTransform = GetComponent<RectTransform>();
      rectTransform.sizeDelta = panelSize;
      //move the panel to the center of the parent
      rectTransform.localPosition = new Vector3(0, 0, 0);
    }
    else
    {
      if (panelSize == lastPanelSize)
      {
        //nothing has changed
        return;
      }
      lastPanelSize = panelSize;
      // resize the spinner as the panel size changes
      float minSize = Mathf.Min(panelSize.x, panelSize.y);
      float panelPadding = minSize * minPaddingPercent;
      float spinnerSize = minSize - 2 * panelPadding;
      spinnerSize = Mathf.Min(spinnerSize, maxSpinnerSizePixels);
      spinner.rectTransform.sizeDelta = new Vector2(spinnerSize, spinnerSize);
      Debug.Log("UpdatingDisplay: " + panelSize + ", " + spinnerSize);
    }
  }
}
