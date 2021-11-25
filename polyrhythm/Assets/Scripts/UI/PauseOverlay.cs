using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseOverlay : MonoBehaviour
{
    
    public float fadeInTime = 0.8f;
    public float fadeOutTime = 0.7f;
    private float finalOpacity = 0.886f;
    private Image image;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        GameManager.Pause += OnPause;
        GameManager.Unpause += OnUnpause;
        image.enabled = false;
    }   

    public void OnPause() {
        image.enabled = true;
        StartCoroutine(OnPauseAnimation());
    }

    public void OnUnpause() {
        StartCoroutine(OnUnpauseAnimation());

    }

    IEnumerator OnPauseAnimation() {
        float timeElapsed = 1.2f;
        while(timeElapsed < fadeInTime) {
            image.color = new Color(0.1037f, 0.1037f, 0.1037f, timeElapsed/fadeInTime * finalOpacity);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0.1037f, 0.1037f, 0.1037f, finalOpacity);
    }

    IEnumerator OnUnpauseAnimation() {
        float timeElapsed = 0;
        while(timeElapsed < fadeOutTime) {
            image.color = new Color(0.1037f, 0.1037f, 0.1037f, (1 - timeElapsed/fadeOutTime) * finalOpacity);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        image.color = new Color(0.1037f, 0.1037f, 0.1037f, 0);
        image.enabled = false;
    }
    
}
