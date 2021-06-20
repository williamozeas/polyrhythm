using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightWipeRCM : ColorMask
{
    private float AddTime = 1.5f;
    private float removeTime = 1.5f;
    [SerializeField]
    Vector3 start = new Vector3(-22.7f, 1, 0);
    [SerializeField]
    Vector3 end = new Vector3(-1.6f, 1, 0);
    
    public override void Add() {
        if(canTransition) {
            canTransition = false;
            mask.enabled = true;
            StartCoroutine("AddAnimation");
        }
        
    }

    private IEnumerator AddAnimation() {
        
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            transform.position = new Vector3(EasingFunction.EaseOutCubic(start.x, end.x, timeElapsed/AddTime), end.y, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mask.enabled = false;
        StyleManager.i.SwitchStyles();
        canTransition = true;
    }

    public override void Remove() {
        if(canTransition) {
            mask.enabled = true;
            canTransition = false;
            StyleManager.i.SwitchStyles();
            StartCoroutine("RemoveAnimation");
        }
    }

    private IEnumerator RemoveAnimation() {
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            transform.position = new Vector3(EasingFunction.EaseOutCubic(end.x, start.x, timeElapsed/AddTime), end.y, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mask.enabled = false;
        canTransition = true;
    }
}
