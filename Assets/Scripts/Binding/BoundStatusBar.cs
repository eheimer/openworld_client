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
  public class BoundStatusBar : BoundComponent
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
      BindingTargetComponentType = typeof(Image);
      BindingTargetProperty = "fillAmount";
    }

    protected override UnityEngine.Component GetTargetComponent()
    {
      // by default we will get the target component from the children of this game object
      // override to implent a different way of getting the target component
      return gameObject.GetComponent(BindingTargetComponentType);
    }

    protected override void Start()
    {
      base.Start();
      var text = gameObject.GetComponentInChildren<TMP_Text>();
      text.text = style.ToString();
      text.gameObject.SetActive(showLabel);

      string filename = statusMap[style];

      var sprite = Resources.Load<Sprite>("StatusBarSprites/" + filename);
      Image component = TargetComponent as Image;
      component.sprite = sprite;
      component.type = Image.Type.Filled;
      component.fillMethod = Image.FillMethod.Horizontal;
      component.fillOrigin = 0;
    }
  }
}