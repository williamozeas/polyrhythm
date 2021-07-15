using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StyleTransition {None, Fade, StraightWipeL, StraightWipeR, CircleGrowTopL, CircleGrowTopR, 
        CircleGrowBotL, CircleGrowBotR, CircleGrowCenter, DrippyWipeR, DrippyWipeL, WavyWipeR, WavyWipeL,
        HorizontalDoors, VerticalDoors}
public enum StyleDirection {Add, Remove, None}

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
        SetStylesArray();
        style[0] = new Style();
        style[1] = Styles[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        activeSide = 0;
    }

    public void ChangeStyle(Style newStyle, StyleTransition transition, StyleDirection direction, string[] args) {
        if(transition == StyleTransition.None || transition == StyleTransition.Fade) {
            style[activeSide] = newStyle;
            OnStyleChange?.Invoke(ref transition); //in these cases the SetColor fn will take care of the transition
        } else {
            style[(activeSide + 1) % 2] = newStyle;
            ChangeStyle(transition, direction, args);
        }
            
    }

    public void ChangeStyle(StyleTransition transition, StyleDirection direction, string[] args) {
        //REQ: transition != None or Fade
        OnStyleChange?.Invoke(ref transition); //have all sprite sets change their colors THEN play transition anim
        //Play anims 
        MaskManager.i.Transition(transition, direction, args);
        
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

    public void SwitchStyles() {
        Style temp = style[activeSide];
        style[activeSide] = style[(activeSide + 1) % 2];
        style[(activeSide + 1) % 2] = temp;
        StyleTransition trans = StyleTransition.None;
        OnStyleChange?.Invoke(ref trans);
    }

    private static Style[] styles;
    //Left, Right, Bg, Fill, Extra
    private void SetStylesArray() {
        styles = new Style[] {
            //0 - default black/white/blue/redpink
            new Style(),
            //1 - pink/green
            new Style ("#38ff49", "#ff38e8", "#000000", "#eeeeee"),
            //2 - yellow/purple with gray bgs
            new Style("#fcff61", "#7e70ff", "#202020", "#e8e8e8"),
            //3 - red/green with white/black reversed
            new Style ("#38ff49", "#ff38e8", "#eeeeee", "#000000"),
            //4 - black and white and grey
            new Style ("#f0f0fd", "#fdf0f0", "#050505", "#707070"),
        };
    }

    public static Style[] Styles {
        get {return styles;} private set{}
    }

    
}
