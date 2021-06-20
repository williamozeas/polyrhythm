using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorMaskTesting : MonoBehaviour
{
    private ColorMask mask;
    
    // Start is called before the first frame update
    void Start()
    {
        mask = GetComponent<ColorMask>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A)) {
            mask.Add();
        }
        if(Input.GetKeyDown(KeyCode.R)) {
            mask.Remove();
        }
    }
}
