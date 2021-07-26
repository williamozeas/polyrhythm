using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    public StyleColor styleColor;
    public int sideNumber;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        StyleManager.OnStyleChange += UpdateColor;
        GetColoredComponent();
        UpdateColorOfComponent();
    }

    //Should be overwritten to get & set the correct component
    protected virtual void GetColoredComponent() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateColor(ref StyleTransition transition) {
        if(transition == StyleTransition.Fade) {
            //Lerp color
        } else {
            UpdateColorOfComponent();
        }
    }

    //should be overridden for non-sprite renderer classes
    protected virtual void UpdateColorOfComponent() {
        spriteRenderer.color = StyleManager.i.GetColor(sideNumber, styleColor);
    }

    

   
}
