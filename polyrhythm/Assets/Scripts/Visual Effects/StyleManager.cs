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
            //16 - Caution Sign
            new Style ("#E3B23C", "#E3B23C", "#423E37", "#AB9D9C", "#A39594"),
            //17-22 - Faded Glass Transition
            new Style ("#E3B23C", "#E3B23C", "#423E37", "#8C8C7D", "#A39594"),
            new Style ("#CFB2B2", "#E3B23C", "#423E37", "#6D6466", "#A39594"),
            new Style ("#CFB2B2", "#E3B23C", "#4E3D42", "#6D6466", "#A39594"),
            new Style ("#CFB2B2", "#6D6466", "#4E3D42", "#8C8C7D", "#A39594"),
            new Style ("#CFB2B2", "#6D6466", "#8C8C7D", "#4E3D42", "#A39594"),
            new Style ("#8C8C7D", "#6D6466", "#CFB2B2", "#4E3D42", "#A39594"),
            //23 - Faded Glass
            new Style("#A9BD89", "#CFB2B2", "#4E3D42", "#8C8C7D", "#6D6466"),
            //24 - Pan Flag
            new Style("#f15bb5", "#00bbf9", "#42147B", "#FEE020", "#00F5D4"),
            //25 - Pan Flag Reversed bg
            new Style("#f15bb5", "#00bbf9", "#FEE020", "#FEE020", "#00F5D4"),
            //26 - Pan Flag BW
            new Style("#f15bb5", "#00bbf9", "#000000", "#dddddd", "#00F5D4"),
            //27 - green fill
            new Style("#bbbbbbbb", "#4f92ff", "#0C0C08", "#28CC46", "#63A088"),
            //28 - all warm yellow
            new Style("#bbbbbbbb", "#4f92ff", "#F2C253", "#F2C253", "#63A088"),
            //29 - fall floaty
            new Style("#388697", "#5E277C", "#DCAE43", "#D99125", "#63A088"),
            //30 - green-yellow bg
            new Style("#388697", "#5E277C", "#C4DB42", "#8BD925", "#63A088"),
            //31 - green bg
            new Style("#388697", "#5E277C", "#42DB5B", "#25D96A", "#63A088"),
            //32 - turqoise bg
            new Style("#388697", "#5E277C", "#42D9DB", "#25D9D9", "#63A088"),
            //33 - blue bg
            new Style("#388697", "#5E277C", "#6342DB", "#4C25D9", "#63A088"),
            //34 - pink bg
            new Style("#388697", "#5E277C", "#C442DB", "#D92594", "#63A088"),
            //35 - red bg
            new Style("#388697", "#5E277C", "#DB5142", "#D92525", "#63A088"),
            //36 - climax
            new Style("#B1D2D1", "#D6E2E9", "#EF6C9C", "#FFAE70", "#F0EFEB"),
            //37 - pretty black grapes
            new Style ("#DD99BB", "#7B506F", "#000000", "#000000", "EAD7D1"),
            //38 - pretty grapes
            new Style ("#DD99BB", "#7B506F", "#0b0916", "#A19291", "EAD7D1"),
            //38 - pretty grapes all white
            new Style ("#DD99BB", "#7B506F", "#A19291", "#A19291", "EAD7D1"),
            //40 - pretty grapes reversed
            new Style ("#DD99BB", "#7B506F", "#9d8e8d", "#0b0916", "EAD7D1"),
            
            
            
            //## - cool sweater
            new Style("#ACC196", "#E9EB9E", "#49475B", "#799496", "#14080E"),
            
            //new Style("#", "#", "#", "#", "#"),
        };
    }

    public static Style[] Styles {
        get {return styles;} private set{}
    }

    
}
