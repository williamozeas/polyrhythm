using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumVisuals : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private int originalScale = 1;
    public float inflateScale = 1.5f;
    public float inflateDuration = 0.4f;
    [SerializeField] private EasingFunction.Ease ease = EasingFunction.Ease.EaseOutCubic;
    private EasingFunction.Function easingFunction;
    public EasingFunction.Ease Ease {
        get {
            return ease;
        }
        set {
            ease = value;
            easingFunction = EasingFunction.GetEasingFunction(ease);
        }
    }

    void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        easingFunction = EasingFunction.GetEasingFunction(ease);
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
        transform.localScale = new Vector3(inflateScale, inflateScale, 1f);
        float timeElapsed = 0;
        while(timeElapsed < inflateDuration) {
            float current = easingFunction(inflateScale, originalScale, timeElapsed / inflateDuration);
            transform.localScale = new Vector3(current, current, 1f);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
    }

    public void OnMiss() {

    }

    public void OnHit() {

    }

    public void OnPass() {
        
    }

}
