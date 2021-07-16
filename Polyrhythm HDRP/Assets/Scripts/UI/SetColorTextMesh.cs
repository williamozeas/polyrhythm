using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorTextMesh : SetColor
{
    TextMesh text;
    protected override void GetColoredComponent()
    {
        text = GetComponent<TextMesh>();
    }

    protected override void UpdateColorOfComponent()
    {
        text.color = StyleManager.i.GetColor(sideNumber, styleColor);
    }
    
}
