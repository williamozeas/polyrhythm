using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ColorMask : MonoBehaviour
{
    protected bool canTransition = true;
    protected SpriteMask mask;
    
    // Start is called before the first frame update
    protected void Start()
    {
        mask = GetComponent<SpriteMask>();
    }

    public abstract void Add();

    public abstract void Remove();
}
