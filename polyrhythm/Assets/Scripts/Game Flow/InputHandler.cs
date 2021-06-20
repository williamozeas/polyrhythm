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

    private void Start() {
        instance = this;
    }

    private void Update() {

        if(Input.GetKeyDown(leftDrumKey)) {
            if(GameManager.i.State == GameState.MAIN_MENU) {
                GameManager.i.State = GameState.GAME;
            }
            LeftDrumPress?.Invoke();
        }
        if(Input.GetKeyDown(rightDrumKey)) {
            RightDrumPress?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            GameManager.i.debug = !GameManager.i.debug;
            if(GameManager.i.debug) {
                Debug.Log("Debug mode Activated!");
            } else {
                Debug.Log("Debug mode deactivated!");
            }
        }

        if(Input.GetKeyDown(KeyCode.S)) {
            StyleManager.i.ChangeStyle(StyleManager.Styles[1], StyleTransition.CircleGrowBotR, StyleDirection.Add, null);
        }
    }
}
