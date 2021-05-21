using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDrum : Drum
{
    // Start is called before the first frame update

    void OnEnable() {
        InputHandler.LeftDrumPress += OnPress;
    }

    void OnDisable() {
        InputHandler.LeftDrumPress -= OnPress;
    }

    public override void OnPress()
    {
        base.OnPress();

    }
}
