using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float width;
    private const float screenHeight = 20;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        width = spriteRenderer.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.size = new Vector2(width, GameManager.i.FillPercent * screenHeight);
    }
}
