using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAudioFunctions : MonoBehaviour
{
    public static SpecialAudioFunctions i;

    private const int CRCount = 1;

    public System.Func<IEnumerator>[] Coroutines = new System.Func<IEnumerator>[1];

    void Awake() {
        if(i == null) { i = this; }
    }

    void Start() {
        Coroutines[0] = IntroCR;
    }

    public void Special(string[] args) {
        switch(args[1]) {
            case "INTRO-MEASURE":
                IntroMeasure();
                break;
            default:
                Debug.LogError("Incorrect special callback!");
                break;
        }
    }

    //INTRO
    private float[] IntroCRBoundaries = {
        0,
        0.1f,
        0.2f,
        0.3f,
        0.4f,
        0.5f,
        0.6f,
        0.7f,
        0.8f,
        0.9f,
        1f
    };

    private GameSettings[] IntroSettings = {
        new GameSettings(0.01f, 0, 0.7f, 0.02f, 0.008f), //0
        new GameSettings(0.01f, 0, 0.6f, 0.02f, 0.008f), //0.1
        new GameSettings(0.01f, 0, 0.6f, 0.02f, 0.008f), //0.2
        new GameSettings(0.009f, 0, 0.5f, 0.019f, 0.008f), //0.3
        new GameSettings(0.008f, 0, 0.5f, 0.018f, 0.008f), //0.4
        new GameSettings(0.007f, 0, 0.45f, 0.017f, 0.008f), //0.5
        new GameSettings(0.006f, 0, 0.4f, 0.016f, 0.008f), //0.6
        new GameSettings(0.0055f, 0, 0.3f, 0.015f, 0.008f), //0.7
        new GameSettings(0.005f, 0, 0.3f, 0.015f, 0.008f), //0.8
        new GameSettings(0.0045f, 0, 0.3f, 0.013f, 0.008f), //0.9
        new GameSettings(0.004f, 0, 0.3f, 0.05f, 0.006f), //1

    };

    private int introCurrentIndex = -1;
    
    //Changes Game Settings Based on fill percent for intro sequence
    private IEnumerator IntroCR() {
        while(true) {
            float fill = GameManager.i.FillPercent;
            for(int i = IntroCRBoundaries.Length - 1; i >= 0; i--) {
                if(fill >= IntroCRBoundaries[i]) {
                    if(i == introCurrentIndex) {
                        break;
                    } else {
                        Debug.Log("changing to settings " + i);
                        CallbackParser.setGameSettings(IntroSettings[i]);
                        introCurrentIndex = i;
                        break;
                    }
                }
            }
            yield return null;
        }
    }

    private int introMeasurePart = 0;
    private bool introMeasureReset = false;
    private void IntroMeasure() {
        float fill = GameManager.i.FillPercent;
        Debug.Log("INTROMEASURE!!!");
        if (fill >= 1 && !introMeasureReset) {
            introMeasurePart++;
            AudioManager.i.music.setParameterByName("Part", introMeasurePart);
            CallbackParser.Parse("NEXT");
            introMeasureReset = true;
        } else if (fill >= 0.9) {
            AudioManager.i.music.setParameterByName("Chord", 0);
        } else if (fill >= 0.8) {
            AudioManager.i.music.setParameterByName("Chord", 1);
        } else if (fill >= 0.7) {
            AudioManager.i.music.setParameterByName("Chord", 0);
        } else if (fill >= 0.6) {
            AudioManager.i.music.setParameterByName("Chord", 1);
        } else {
            AudioManager.i.music.setParameterByName("Chord", 0);
            introMeasureReset = false;
        }
    }


}
