using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderWithShader : MonoBehaviour
{
    public Shader glitch;
    // Start is called before the first frame update
    void Start()
    {
        Camera.main.RenderWithShader(glitch, "");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
