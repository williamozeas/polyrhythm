using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    List<Transform> children = new List<Transform>();

    public Transform leftButtonCanvas;
    public Transform rightButtonCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentsInChildren<Transform>(children);
        children.Add(leftButtonCanvas);
        children.Add(rightButtonCanvas);
        GameManager.StartGame += Disable;
        GameManager.StartMainMenu += Enable;
    }

    void Disable() {
        foreach (Transform child in children)
        {
            child.gameObject.SetActive(false);
        }
    }

    void Enable() {
        foreach (Transform child in children)
        {
            child.gameObject.SetActive(true);
        }
    }
    
}
