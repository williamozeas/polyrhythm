using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDrum : Drum
{
   // Start is called before the first frame update

    void OnEnable() {
        InputHandler.RightDrumPress += OnPress;
    }

    void OnDisable() {
        InputHandler.RightDrumPress -= OnPress;
    }

    public override void OnPress()
    {
        base.OnPress();

    }
}
