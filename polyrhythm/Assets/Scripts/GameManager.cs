using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { MAIN_MENU, GAME, PAUSE}


public class GameManager : MonoBehaviour
{
    public int score = 0;
    private GameState gameState;
    public static GameManager instance; 
    private void Start() {
        instance = this;
        SetGameState(GameState.MAIN_MENU);
    }

    public GameState GetGameState() {
        return gameState;
    }

    public void SetGameState(GameState state) {
        gameState = state;
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
