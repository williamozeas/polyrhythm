using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallbackParser : MonoBehaviour
{
    //Marker name formatting:
    //TYPE, PARAMS
    //Type: L, Params: Intensity (1-5)
    //Type: R, Params: Intensity (1-5)
    //Type: STYLE, Params: Style Name
    //Type: SETTINGS, Params: PPH (% per hit), Decay, Window time (ms), Miss penalty, Pass penalty
    //Type: RESET
    //Type: STOP
    public static void Parse(string marker) {
        string[] symbols = marker.Split(',');
        Debug.Log(symbols[0]);
        switch (symbols[0]) {
            case "L":
                L(int.Parse(symbols[1]));
                break;
            case "R":
                R(int.Parse(symbols[1]));
                break;
            case "STYLE":
                break;
            case "SETTINGS":
                setGameSettings(symbols);
                break;
            case "RESET":
                GameManager.i.ResetFillPercent();
                break;
            case "STOP":
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
            newSettings = new GameSettings();
            Debug.Log("Default Settings");
        } else {
            newSettings = new GameSettings(float.Parse(args[1]), float.Parse(args[2]), float.Parse(args[3]), float.Parse(args[4]), float.Parse(args[5]));
        }
        GameManager.i.settings = newSettings;
    }
}
