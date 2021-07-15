using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CallbackParser : MonoBehaviour
{
    public static IEnumerator specialCoroutine;

    private static int primeLock = -1;

    //Marker name formatting:
    //TYPE  PARAMS
    //Type: L, Params: Intensity (0-5)
    //Type: R, Params: Intensity (0-5)
    //Type: STYLE, Params: Style Name
    //Type: SETTINGS, Params: PPH (% per hit), Decay, Window time (ms), Miss penalty, Pass penalty
    //Type: NEXT
    //Type: COROUTINE, Params: "START" or "STOP", Coroutine #
    //Type: SPECIAL, Params: Anything
    //Type: STOP
    public static void Parse(string marker) {
        Debug.Log(marker);
        string[] symbols = marker.Split(' ');
        Parse(symbols);
    }

    public static void Parse(string[] symbols) {
        switch (symbols[0]) {
            case "PRIMED":
                if(int.Parse(symbols[1]) == primeLock) {
                    Parse(symbols.Skip(2).ToArray());
                }
                break;
            case "L":
                L(int.Parse(symbols[1]));
                break;
            case "R":
                R(int.Parse(symbols[1]));
                break;
            case "NONE":
                break;
            case "PRIME":
                primeLock = int.Parse(symbols[1]);
                break;
            case "STYLE":
                Style(symbols);
                break;
            case "SETTINGS":
                setGameSettings(symbols);
                break;
            case "NEXT":
                GameManager.i.ResetFillPercent();
                break;
            case "COROUTINE":
                SpecialCoroutine(symbols);
                break;
            case "SPECIAL":
                SpecialAudioFunctions.i.Special(symbols);
                break;
            case "STOP":
                AudioManager.i.StopMusic();
                break;
            default:
                Debug.LogWarning("Incorrect marker!");
                Debug.Log(symbols[0]);
                break;
        }
    }

    private static void L(int intensity) {
        GameManager.LeftDrumActivate(intensity);
    }

    private static void R(int intensity) {
        GameManager.RightDrumActivate(intensity);
    }
    
    private static void setGameSettings(string[] args) {
        if(args.Length != 6) {
            setGameSettings(new GameSettings());
            Debug.Log("Default Settings");
        } else {
            setGameSettings(new GameSettings(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5])));
        }
        
    }

    public static void setGameSettings(GameSettings newSettings) {
        if(GameManager.i.debug) {
            newSettings = new GameSettings(newSettings.pph * 2, 0, newSettings.window, 0, 0);
        }
        GameManager.i.settings = newSettings;
    }

    private static void SpecialCoroutine(string[] args) {
        switch (args[1]) {
            case "START":
                specialCoroutine = SpecialAudioFunctions.i.Coroutines[int.Parse(args[2])]();
                SpecialAudioFunctions.i.StartCoroutine(specialCoroutine);
                break;
            case "STOP":
                SpecialAudioFunctions.i.StopCoroutine(specialCoroutine);
                break;
            default:
                Debug.LogError("Incorrect Coroutine Marker!");
                break;
        }
    }

    private static void Style(string[] symbols) {
        StyleTransition transition;
        StyleDirection direction;
        //direction
        switch(symbols[3].ToLower()) {
            case "add": {
                direction = StyleDirection.Add;
                break;
            }
            case "remove": {
                direction = StyleDirection.Remove;
                break;
            }
            default: {
                direction = StyleDirection.None;
                break;
            }
        }
        //transition
        switch(symbols[2].ToLower()) {
            case "straightwipemaskr": {
                transition = StyleTransition.StraightWipeR;
                break;
            }
            case "straightwipemaskl": {
                transition = StyleTransition.StraightWipeL;
                break;
            }
            case "circlegrowtopr": {
                transition = StyleTransition.CircleGrowTopR;
                break;
            }
            case "circlegrowtopl": {
                transition = StyleTransition.CircleGrowTopL;
                break;
            }
            case "circlegrowbotr": {
                transition = StyleTransition.CircleGrowBotR;
                break;
            }
            case "circlegrowbotl": {
                transition = StyleTransition.CircleGrowBotL;
                break;
            }
            case "circlegrowcenter": {
                transition = StyleTransition.CircleGrowCenter;
                break;
            }
            case "drippywiper": {
                transition = StyleTransition.DrippyWipeR;
                break;
            }
            case "drippywipel": {
                transition = StyleTransition.DrippyWipeL;
                break;
            }
            case "wavywiper": {
                transition = StyleTransition.WavyWipeR;
                break;
            }
            case "wavywipel": {
                transition = StyleTransition.WavyWipeL;
                break;
            }
            case "horizontaldoors": {
                transition = StyleTransition.HorizontalDoors;
                break;
            }
            case "verticaldoors": {
                transition = StyleTransition.VerticalDoors;
                break;
            }
            case "fade": {
                transition = StyleTransition.Fade;
                break;
            }
            case "none": {
                transition = StyleTransition.None;
                break;
            }
            default: {
                transition = StyleTransition.None;
                Debug.Log("Unknown transition type!");
                break;
            }
        }
        StyleManager.i.ChangeStyle(StyleManager.Styles[int.Parse(symbols[1])], transition, direction, symbols);
    }
}
