using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColorDrum : SetColor
{
    private DrumVisuals drum;
    protected override void GetColoredComponent()
    {
        drum = GetComponent<DrumVisuals>();
    }

    protected override void UpdateColorOfComponent()
    {
        drum.Color = StyleManager.i.GetColor(sideNumber, styleColor);
    }
}
