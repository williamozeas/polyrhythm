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
    //Type: SETTINGS, Params: PPH (% per hit), Decay, Window time (ms), Miss penalty
    //Type: STOP
    public static void Parse(string marker) {
        string[] symbols = marker.Split(',');
        Debug.Log(symbols[0]);
        switch (symbols[0]) {
            case "L":
                L(int.Parse(symbols[1]));
                break;
            case "R":
                //R(int.Parse(symbols[1]));
                break;
            case "STYLE":
                break;
            case "SETTINGS":
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
}
