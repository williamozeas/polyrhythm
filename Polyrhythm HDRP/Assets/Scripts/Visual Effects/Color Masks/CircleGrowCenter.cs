using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleGrowCenter : ColorMask
{
    private float AddTime = 1f;
    private float removeTime = 1f;
    float screenSize;
    Camera camera;
    float height;
    float width;
    
    new void Start() {
        camera = Camera.main;
        base.Start();
        height = camera.ScreenToWorldPoint(new Vector3(0, Screen.height, camera. nearClipPlane)).y - camera.ScreenToWorldPoint(new Vector3(0, 0, camera. nearClipPlane)).y;
        width = camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, camera. nearClipPlane)).x - camera.ScreenToWorldPoint(new Vector3(0, 0, camera. nearClipPlane)).x;
        screenSize = Mathf.Sqrt((width * width) + (height * height)) + 0.5f;
    }

    public override void Add() {
        if(canTransition) {
            canTransition = false;
            mask.enabled = true;
            StartCoroutine("AddAnimation");
        }
        
    }

    private IEnumerator AddAnimation() {
        transform.position = new Vector3(0, 0, 0);
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            float scale = EasingFunction.EaseOutCubic(0, screenSize, timeElapsed/AddTime);
            transform.localScale = new Vector3(scale, scale, 0);
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
        transform.position = new Vector3(0, 0, 0);
        float timeElapsed = 0;
        while(timeElapsed < AddTime) {
            float scale = EasingFunction.EaseOutCubic(screenSize, 0, timeElapsed/AddTime);
            transform.localScale = new Vector3(scale, scale, 0);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        mask.enabled = false;
        canTransition = true;
    }
}
