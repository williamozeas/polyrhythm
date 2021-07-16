using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fill : MonoBehaviour
{
    SpriteRenderer[] spriteRenderer;
    private float width;
    private const float screenHeight = 20;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();
        width = spriteRenderer[0].size.x;
        GameManager.ResetFill += ResetFill;
    }

    // Update is called once per frame
    void Update()
    {
        SetFill();
    }

    public void ResetFill() {
        SpriteRenderer prev = spriteRenderer[index];
        index = (index + 1) % 2;
        SetFill();
        StartCoroutine(VisFx.FadeOut(prev, 1f));
    }

    private void SetFill() {
        float height = GameManager.i.FillPercent * screenHeight;
        if (float.IsNaN (height)) height = 0;
        spriteRenderer[index].size = new Vector2(width, height);
    }

    
}
