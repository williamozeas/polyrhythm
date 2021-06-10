using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorBackground : SetColor
{
    private Camera cam;
    protected override void GetColoredComponent()
    {
        cam = GetComponent<Camera>();
    }

    protected override void UpdateColorOfComponent()
    {
        cam.backgroundColor = StyleManager.i.GetColor(sideNumber, styleColor);
    }
}
