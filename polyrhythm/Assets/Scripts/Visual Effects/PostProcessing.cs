using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessing : MonoBehaviour
{
    public static PostProcessing i;
    private VolumeProfile volume;
    private ChromaticAberration chromatic;
    private Bloom bloom;
    void Awake() {
        i = this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<Volume>().profile;
        volume.TryGet<ChromaticAberration>(out chromatic);
        volume.TryGet<Bloom>(out bloom);
    }

    

    private Coroutine chromaticChange;
    public void OnHit(int intensity) {
        if(chromaticChange != null) {
            StopCoroutine(chromaticChange);
        }
        chromaticChange = StartCoroutine(ChromaticAberrationHit(intensity));
    }

    private IEnumerator ChromaticAberrationHit(int intensity) {
        float newValue = 0;
        if(intensity != 1) {
            newValue = Mathf.Clamp01(intensity/4f);
        }
        if(newValue == 0) {
            yield return null;
        } else {
            chromatic.intensity.Override(newValue);
            float holdTime = 0.1f + newValue * 0.2f;
            float decayTime = 0.2f + newValue * 0.4f;
            float timeElapsed = 0;
            yield return new WaitForSecondsRealtime(holdTime);
            while(timeElapsed < decayTime) {
                chromatic.intensity.Override(EasingFunction.EaseInSine(newValue, 0, timeElapsed/decayTime));
                timeElapsed += Time.deltaTime;
                yield return null;
            }
            chromatic.intensity.Override(0f);
        }
        
    }
}
