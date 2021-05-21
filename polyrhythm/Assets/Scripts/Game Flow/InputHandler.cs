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
            LeftDrumPress?.Invoke();
        }
        if(Input.GetKeyDown(rightDrumKey)) {
            RightDrumPress?.Invoke();
        }
    }
}
