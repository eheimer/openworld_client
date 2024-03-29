using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Openworld.Binding
{
  public enum StatusBarStyle
  {
    Health, Mana, Stamina, Sleep, Hunger
  }
  public class BoundStatusBar : BoundComponent<Image>
  {
    private static Dictionary<StatusBarStyle, string> statusMap = new Dictionary<StatusBarStyle, string> {
        {StatusBarStyle.Health, "Bar_Red_Front"},
        {StatusBarStyle.Mana, "Bar_Blue_Front"},
        {StatusBarStyle.Stamina, "Bar_Yellow_Front"},
        {StatusBarStyle.Sleep, "Bar_Green_Front"},
        {StatusBarStyle.Hunger, "Bar_Purple_Front"}
    };

    [SerializeField] StatusBarStyle style;
    [SerializeField] bool showLabel;

    public BoundStatusBar()
    {
      //BindingTargetComponentType = typeof(Image);
      BindingTargetProperty = "fillAmount";
    }

    protected override Image GetTargetComponent()
    {
      return gameObject.GetComponent<Image>();
    }

    protected override void Start()
    {
      base.Start();
      var text = gameObject.GetComponentInChildren<TMP_Text>();
      text.text = style.ToString();
      text.gameObject.SetActive(showLabel);

      string filename = statusMap[style];

      var sprite = Resources.Load<Sprite>("StatusBarSprites/" + filename);
      Image component = GetTargetComponent();
      component.sprite = sprite;
      component.type = Image.Type.Filled;
      component.fillMethod = Image.FillMethod.Horizontal;
      component.fillOrigin = 0;
    }
  }
}