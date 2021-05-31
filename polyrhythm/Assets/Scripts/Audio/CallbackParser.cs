using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackParser : MonoBehaviour
{
    public static IEnumerator specialCoroutine;

    //Marker name formatting:
    //TYPE  PARAMS
    //Type: L, Params: Intensity (1-5)
    //Type: R, Params: Intensity (1-5)
    //Type: STYLE, Params: Style Name
    //Type: SETTINGS, Params: PPH (% per hit), Decay, Window time (ms), Miss penalty, Pass penalty
    //Type: NEXT
    //Type: COROUTINE, Params: "START" or "STOP", Coroutine #
    //Type: SPECIAL, Params: Anything
    //Type: STOP
    public static void Parse(string marker) {
        Debug.Log(marker);
        string[] symbols = marker.Split(' ');
        switch (symbols[0]) {
            case "L":
                L(int.Parse(symbols[1]));
                break;
            case "R":
                R(int.Parse(symbols[1]));
                break;
            case "NONE":
                break;
            case "STYLE":
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
                Debug.LogError("Incorrect marker!");
                break;
        }
    }

    private static void L(int intensity) {
        GameManager.i.leftDrum.Activate();
    }

    private static void R(int intensity) {
        GameManager.i.rightDrum.Activate();
    }
    
    private static void setGameSettings(string[] args) {
        GameSettings newSettings;
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
}
