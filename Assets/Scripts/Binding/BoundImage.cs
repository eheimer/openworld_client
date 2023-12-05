using System.Collections;
using System.Collections.Generic;
using Openworld.Binding;
using UnityEngine;
using UnityEngine.UI;

public class BoundImage : BoundComponent
{
    [SerializeField] string resourcePath = ""; // the path to find the image resource

    public BoundImage()
    {
        BindingTargetComponentType = typeof(Image);
        BindingTargetProperty = "sprite";
    }

    protected override UnityEngine.Component GetTargetComponent()
    {
        return gameObject.GetComponent(BindingTargetComponentType);
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
