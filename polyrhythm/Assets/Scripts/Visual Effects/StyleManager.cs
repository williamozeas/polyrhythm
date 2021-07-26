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
        if(transition == StyleTransition.None) {
            style[activeSide] = newStyle;
            OnStyleChange?.Invoke(ref transition); //in these cases the SetColor fn will take care of the transition
        } else if(transition == StyleTransition.Fade) {
            StartCoroutine(LerpColor(style[activeSide], newStyle, float.Parse(args[4])));
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

    public IEnumerator LerpColor(Style start, Style end, float duration) {
        float timeElapsed = 0;
        while(timeElapsed < duration) {
            Color left = Color.Lerp(start.leftColor, end.leftColor, timeElapsed/duration);
            Color right = Color.Lerp(start.rightColor, end.rightColor, timeElapsed/duration);
            Color bg = Color.Lerp(start.bgColor, end.bgColor, timeElapsed/duration);
            Color fill = Color.Lerp(start.fillColor, end.fillColor, timeElapsed/duration);
            Color extra = Color.Lerp(start.extraColor, end.extraColor, timeElapsed/duration);
            ChangeStyle(new Style(left, right, bg, fill, extra), StyleTransition.None, StyleDirection.None, null);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        ChangeStyle(end, StyleTransition.None, StyleDirection.None, null);
        
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
    private void SetStylesArray() {
        //Left, Right, bg, fill, other
        //ALWAYS NEXT BEFORE STYLE IN MAPS
        styles = new Style[] {
            //0 - default black/white/blue/redpink
            new Style(),
            //1 - pink/green
            new Style ("#38ff49", "#ff38e8", "#000000", "#eeeeee"),
            //2 - yellow/purple with gray bgs
            new Style("#fcff61", "#7e70ff", "#202020", "#e8e8e8"),
            //3 - red/green with white/black reversed
            new Style ("#38ff49", "#ff38e8", "#eeeeee", "#000000"),
            //4 - white/white with black/grey bg
            new Style ("#fdfdfd", "#fdfdfd", "#101010", "#404040"),
            //5 - blue/redpink reversed
            new Style ("#32FBFF", "#FF008A", "#000000", "#eeeeee"),
            //6 - yellow/green with black/white bg
            new Style ("#FCFF36", "#8FFF0F", "#000000", "#eeeeee"),
            //7 - ALL BLACK
            new Style ("#000000", "#000000", "#000000", "#000000"),
            //8 - Black and White
            new Style ("#ffffff", "#ffffff", "#000000", "#000000"),
            //9 - INVERSE black and white
            new Style ("#000000", "#000000", "#ffffff", "#ffffff"),
            //10 - Orange/Orange black white
            new Style ("#FFBF3F", "#FFBF3F", "#000000", "#ffffff"),
            //11 - Strawberry Lemonade
            new Style ("#FF3F8E", "#FF3F8E", "#FFBF3F", "#000000"),
            //12 - Cool blues
            new Style ("#74D5DD", "#74D5DD", "#398AD7", "#2F66A9"),
            //13 - Circus Candy Cane
            new Style ("#FF2839", "#eeeeee", "#FF2839", "#eeeeee"),
            //14 - inverse Candy Cane
            new Style ("#eeeeee", "#FF2839", "#FF2839", "#eeeeee"),
            //15 - Cotton Candy Clouds
            new Style ("#Ea5dE2", "#60F8FF", "#FAFF60", "#949687"),
        };
    }

    public static Style[] Styles {
        get {return styles;} private set{}
    }

    
}
