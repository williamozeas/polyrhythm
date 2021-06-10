using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumVisuals : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameObject outlines;
    private SpriteRenderer[] outlineSpriteRenderers;
    private Color color;
    public Color Color {
        get {return color;}
        set {SetNewColor(value);}
    }
    private int originalScale = 1;
    [Header("Inflate")]
    public float inflateScale = 0.6f;
    public float inflateDuration = 0.4f;
    [SerializeField] private EasingFunction.Ease inflateEase = EasingFunction.Ease.EaseOutCubic;
    private EasingFunction.Function inflateEaseFunction;
    public EasingFunction.Ease InflateEase {
        get {
            return inflateEase;
        }
        set {
            inflateEase = value;
            inflateEaseFunction = EasingFunction.GetEasingFunction(inflateEase);
        }
    }

    [Header("Outline Extend")]
    public float outlineExtendScale = 3f;
    private bool[] outlineExtendSlowing = { false, false, false, false, false };
    public float outlineExtendDuration = 0.7f;
    [SerializeField] private EasingFunction.Ease outlineExtendEase = EasingFunction.Ease.EaseOutCubic;
    private EasingFunction.Function outlineExtendFunction;
    public EasingFunction.Ease OutlineExtendEase {
        get {
            return outlineExtendEase;
        }
        set {
            outlineExtendEase = value;
            outlineExtendFunction = EasingFunction.GetEasingFunction(outlineExtendEase);
        }
    }

    void Awake() {
        outlineSpriteRenderers = outlines.GetComponentsInChildren<SpriteRenderer>();
    }
    

    void Start()
    {
        inflateEaseFunction = EasingFunction.GetEasingFunction(inflateEase);
        outlineExtendFunction = EasingFunction.GetEasingFunction(outlineExtendEase);
        spriteRenderer.color = color;
        foreach (var outlineSpriteRenderer in outlineSpriteRenderers)
        {
            outlineSpriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }
        
    }

    // void Update() {
    //     easingFunction = EasingFunction.GetEasingFunction(ease);
    // }

    private IEnumerator inflateCoroutine;

    public void OnPress() {
        if(inflateCoroutine != null) 
            StopCoroutine(inflateCoroutine);
        inflateCoroutine = Inflate();
        StartCoroutine(inflateCoroutine);
    }

    IEnumerator Inflate() {
        spriteRenderer.size = new Vector2(inflateScale * 0.02f, inflateScale * 0.02f);
        float timeElapsed = 0;
        while(timeElapsed < inflateDuration) {
            float current = inflateEaseFunction(inflateScale, originalScale, timeElapsed / inflateDuration);
            spriteRenderer.size = new Vector3(current * 0.02f, current * 0.02f, 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }


    private int outlineCounter = 0;
    public void OnActivate(int intensity) {
        outlineCounter = (outlineCounter + 1) % outlineSpriteRenderers.Length;
        outlineExtendSlowing[outlineCounter] = true;
        StartCoroutine("OutlineExtend");
    }

    IEnumerator OutlineExtend() {
        int counter = outlineCounter;
        SpriteRenderer outlineSpriteRenderer = outlineSpriteRenderers[counter];
        float timeElapsed = 0;
        float speed = 0;
        while(timeElapsed < outlineExtendDuration) {
            if(outlineExtendSlowing[counter]) {
                float current = outlineExtendFunction(originalScale * 0.6f, outlineExtendScale, timeElapsed / outlineExtendDuration);
                speed = Mathf.Clamp((current * 0.16f - outlineSpriteRenderer.size.x) / Time.deltaTime, 1.6f, 3);
                outlineSpriteRenderer.size = new Vector2(current * 0.16f, current * 0.16f);
            } else {
                outlineSpriteRenderer.size = new Vector2(outlineSpriteRenderer.size.x + (speed * Time.deltaTime), outlineSpriteRenderer.size.y + (speed * Time.deltaTime));
            }
            float alpha = EasingFunction.EaseOutSine(1f, 0f, timeElapsed/outlineExtendDuration);
            outlineSpriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,spriteRenderer.color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        outlineSpriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g,spriteRenderer.color.b, 0);
        outlineSpriteRenderer.size = new Vector2(originalScale * 0.6f * 0.16f, originalScale * 0.8f * 0.16f);
    }

    public void OnMiss() {

    }

    public void OnHit() {
        outlineExtendSlowing[outlineCounter] = false;
    }

    public void OnPass() {

    }

    private void SetNewColor(Color newColor) {
        color = newColor;
        spriteRenderer.color = color;
        foreach (SpriteRenderer spr in outlineSpriteRenderers)
        {
            spr.color = color;   
        }
    }

    

}
