using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MAIN_MENU, GAME, PAUSE}

public class GameManager : MonoBehaviour
{
    public int score = 0;
    private float fillPercent = 0;
    private GameState state;
    public static GameManager i; 
    public GameSettings settings;
    public static event Action ResetFill;

    public LeftDrum leftDrum;
    public RightDrum rightDrum;

    public float FillPercent {
        get { return fillPercent; }
        set { 
            fillPercent = value;
            if(fillPercent <= 0) {
                fillPercent = 0;
            }
        }
    }

    public GameState State {
        get {
            return state;
        }
        set {
            state = value;
            switch(state) {
                case GameState.MAIN_MENU:
                    score = 0;
                    break;

                case GameState.GAME:
                    score = 0;
                    break;

                case GameState.PAUSE:
                    break;    
            }
        }
    }

    private void Start() {
        i = this;
        State = GameState.MAIN_MENU;
        settings = new GameSettings();
        leftDrum = GameObject.Find("Left Drum").GetComponent<LeftDrum>();
        rightDrum = GameObject.Find("Right Drum").GetComponent<RightDrum>();
    }

    void Update() {
        Decay();
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Fill Percent", fillPercent);
    }

    void Decay() {
        FillPercent -= Time.deltaTime * settings.decay;
    }

    public void subtractFillMiss() {
        StartCoroutine(smoothAdd(settings.penaltyMiss * -1, 0.3f));
    }

    public void subtractFillPass() {
        StartCoroutine(smoothAdd(settings.penaltyPass * -1, 0.3f));
    }

    public void addFill() {
        FillPercent += settings.pph;
    }

    public void ResetFillPercent() {
        ResetFill?.Invoke();
        FillPercent = 0;
    }

    IEnumerator smoothAdd(float percent, float time) {
        float perSec = percent/time;
        float timeElapsed = 0;
        while(timeElapsed < time) {
            FillPercent += perSec * Time.deltaTime;
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

}
