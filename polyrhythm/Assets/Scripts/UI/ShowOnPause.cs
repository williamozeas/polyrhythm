using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnPause : MonoBehaviour
{
    public Canvas child;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Pause += Enable;
        GameManager.Unpause += Disable;
    }

    void Disable() {
        child.enabled = false;;
    }

    void Enable() {
        child.enabled = true;
    }
}
