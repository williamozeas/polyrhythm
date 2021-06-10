using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StyleTransition {None, Fade, StraightWipeL, StraightWipeR, CircleOutL, CircleOutR, CircleInL, CircleInR}

public class StyleManager : MonoBehaviour
{
    public static StyleManager i; 
    public static Style[] style = new Style[2];
    public static int activeSide = 0;


    public static event ActionRef<StyleTransition> OnStyleChange;

    void Awake() {
        if(i == null) {
            i = this;
        }
        style[0] = new Style();
        style[1] = new Style("#fcff61", "#7e70ff", "#404040", "#e8e8e8");
    }

    // Start is called before the first frame update
    void Start()
    {
        activeSide = 0;

    }

    public void ChangeStyle(Style newStyle, StyleTransition transition) {
        if(transition == StyleTransition.None || transition == StyleTransition.Fade) {
            style[activeSide] = newStyle;
            OnStyleChange?.Invoke(ref transition); //in these cases the SetColor fn will take care of the transition
        } else {
            style[(activeSide + 1) % 2] = newStyle;
            ChangeStyle(transition);
        }
            
    }

    public void ChangeStyle(StyleTransition transition) {
        //REQ: transition != None or Fade
        OnStyleChange?.Invoke(ref transition); //have all sprite sets change their colors THEN play transition anim
        //Play anims
        switch(transition) {
            case StyleTransition.StraightWipeL:
                break;
        }
    }

    public Color GetColor(int sideNumber, StyleColor styleColor) {
        switch(styleColor) {
            case StyleColor.Left:
                return style[sideNumber].leftColor;
            case StyleColor.Right:
                return style[sideNumber].rightColor;
            case StyleColor.Bg:
                return style[sideNumber].bgColor;
            case StyleColor.Fill:
                return style[sideNumber].fillColor;
            case StyleColor.Extra:
                return style[sideNumber].extraColor;
            default:
                Debug.Log("GetColor defaulted!");
                return style[sideNumber].fillColor;
        }
    }

    

    
}
