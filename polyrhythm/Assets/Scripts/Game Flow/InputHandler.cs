using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    private KeyCode leftDrumKey = KeyCode.Z;
    private KeyCode rightDrumKey = KeyCode.M;

    public static event Action LeftDrumPress;
    public static event Action RightDrumPress;

    private GameState prevState;

    private void Start() {
        instance = this;
    }
    int current = 0;
    private void Update() {

        if(Input.GetKeyDown(leftDrumKey)) {
            if(GameManager.i.State == GameState.MAIN_MENU) {
                GameManager.i.State = GameState.GAME;
            }
            if(GameManager.i.State == GameState.END) {
                GameManager.i.State = GameState.MAIN_MENU;
            }
            LeftDrumPress?.Invoke();
        }
        if(Input.GetKeyDown(rightDrumKey)) {
            if(GameManager.i.State == GameState.END) {
                GameManager.i.State = GameState.MAIN_MENU;
            }
            if(GameManager.i.State == GameState.MAIN_MENU) {
                GameManager.i.State = GameState.PAUSE;
            }
            RightDrumPress?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Escape)) {
            if(GameManager.i.State == GameState.PAUSE) {
                GameManager.i.State = prevState;
            } else {
                prevState = GameManager.i.State;
                GameManager.i.State = GameState.PAUSE;
            }
        }

        //DEBUG
        #if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.D)) {
                GameManager.i.debug = !GameManager.i.debug;
                if(GameManager.i.debug) {
                    Debug.Log("Debug mode Activated!");
                } else {
                    Debug.Log("Debug mode deactivated!");
                }
            }

            if(Input.GetKeyDown(KeyCode.F)) {
                GameManager.i.FillPercent = 2f;
            }

            if(Input.GetKeyDown(KeyCode.S)) {
                current = (current + 1)%2;
                StyleManager.i.ChangeStyle(StyleManager.Styles[current], StyleTransition.CircleGrowBotR, StyleDirection.Add, null);
            }
        #endif
    }
}
