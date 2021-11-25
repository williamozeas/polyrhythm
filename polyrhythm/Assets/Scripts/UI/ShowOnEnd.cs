using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnEnd : MonoBehaviour
{
    public Canvas child;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.EndGame += Enable;
        GameManager.MainMenu += Disable;
    }

    void Disable() {
        child.enabled = false;;
    }

    void Enable() {
        child.enabled = true;
    }
}
