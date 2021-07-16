using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoorsCM : ColorMask
{
    public float AddTime = 0.6f;
    public float removeTime = 0.6f;
    [SerializeField]
    public Vector3 startL = new Vector3(-15.5f, 1, 0);
    [SerializeField]
    public Vector3 endL = new Vector3(1.6f, 1, 0);
    [SerializeField]
    public Vector3 startR = new Vector3(14.8f, 1, 0);
    [SerializeField]
    public Vector3 endR = new Vector3(1.6f, 1, 0);
    public SpriteMask left;
    public SpriteMask right;

    
    
    public override void Add() {
        if(canTransition) {
            canTransition = false;
            left.enabled = true;
            right.enabled = true;
            StartCoroutine("AddAnimation");
        }
        
    }

    private IEnumerator AddAnimation() {
        
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            left.transform.position = new Vector3(EasingFunction.EaseOutQuad(startL.x, endL.x, timeElapsed/AddTime), EasingFunction.EaseOutQuad(startL.y, endL.y, timeElapsed/AddTime), 0);
            right.transform.position = new Vector3(EasingFunction.EaseOutQuad(startR.x, endR.x, timeElapsed/AddTime), EasingFunction.EaseOutQuad(startR.y, endR.y, timeElapsed/AddTime), 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        left.enabled = false;
        right.enabled = false;
        StyleManager.i.SwitchStyles();
        canTransition = true;
    }

    public override void Remove() {
        if(canTransition) {
            left.enabled = true;
            right.enabled = true;
            canTransition = false;
            StyleManager.i.SwitchStyles();
            StartCoroutine("RemoveAnimation");
        }
    }

    private IEnumerator RemoveAnimation() {
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            left.transform.position = new Vector3(EasingFunction.EaseInQuad(endL.x, startL.x, timeElapsed/AddTime), EasingFunction.EaseInQuad(endL.y, startL.y, timeElapsed/AddTime), 0);
            right.transform.position = new Vector3(EasingFunction.EaseInQuad(endR.x, startR.x, timeElapsed/AddTime), EasingFunction.EaseInQuad(endR.y, startR.y, timeElapsed/AddTime), 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        left.enabled = false;
        right.enabled = false;
        canTransition = true;
    }
}
