using System.Collections;
using System.Collections.Generic;
using Openworld.Binding;
using UnityEngine;
using UnityEngine.UI;

public class BoundImage : BoundComponent<Image>
{
    [SerializeField] string resourcePath = ""; // the path to find the image resource

    public BoundImage()
    {
        BindingTargetProperty = "sprite";
    }

    protected override Image GetTargetComponent()
    {
        return gameObject.GetComponent<Image>();
    }

    protected override void UpdateBindingTarget()
    {
        if (TargetProperty != null && SourcePropertyValue != null)
        {
            var sprite = Resources.Load<Sprite>(resourcePath + "/" + SourcePropertyValue.ToString());
            TargetProperty.SetValue(TargetComponent, sprite);
        }
    }
}
