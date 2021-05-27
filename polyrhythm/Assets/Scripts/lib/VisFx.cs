using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisFx 
{
    public static IEnumerator FadeOut(SpriteRenderer spr, float time) {
        float timeElapsed = 0;
        Color color = spr.color;
        while(timeElapsed < time) {
            float alpha = EasingFunction.Linear(1f, 0f, timeElapsed / time);
            spr.color = new Color(color.r, color.g, color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
