﻿using System.Collections;
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

    public float FillPercent {
        get { return fillPercent; }
        set { 
            fillPercent = value;
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
    }

    void Update() {
        Decay();
    }

    void Decay() {
        if(fillPercent > 0) {
            fillPercent -= Time.deltaTime * settings.decay;
        }
    }

    public void subtractFillMiss() {
        FillPercent -= settings.penaltyMiss;
    }

    public void subtractFillPass() {
        FillPercent -= settings.penaltyPass;
    }

    public void addFill() {
        FillPercent += settings.pph;
    }

}
