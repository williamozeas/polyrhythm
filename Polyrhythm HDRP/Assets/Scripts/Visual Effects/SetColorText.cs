using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetColorText : SetColor
{
    private TextMeshProUGUI text;

    protected override void GetColoredComponent()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    protected override void UpdateColorOfComponent()
    {
        text.color = StyleManager.i.GetColor(sideNumber, styleColor);
    }
   
}
