using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopIcons : MonoBehaviour
{
    public Image LoopSymbol;
    public Image ForwardSymbol;
    private int isLoop;
    private RectTransform rectTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        DisableIcons();
        GameManager.OnLoopChange += SetSymbol;
        GameManager.MainMenu += DisableIcons;
    }

    public void SetSymbol(ref int i) {
        if(i != isLoop) {
            isLoop = i;
            StartCoroutine("SetSymbolCR");
        }
    }

    public void DisableIcons() {
        LoopSymbol.enabled = false;
        ForwardSymbol.enabled = false;
    }

    IEnumerator SetSymbolCR() {
        Image inImage;
        Image outImage;
        Vector3 inStart = new Vector3(-1200, -68, 0);
        Vector3 midPos = new Vector3(-100, -68, 0);
        Vector3 outEnd = new Vector3(300, -68, 0); 
        if(isLoop == 1) {
            inImage = LoopSymbol;
            outImage = ForwardSymbol;
        } else {
            inImage = ForwardSymbol;
            outImage = LoopSymbol;
        }
        inImage.enabled = true;
        float timeElapsed = 0;
        float enterTime = 2f;
        float exitTime = 0.8f;
        //invariant: exitTime <= enterTime
        while(timeElapsed <= enterTime) {
            inImage.rectTransform.anchoredPosition = new Vector3(EasingFunction.EaseOutCubic(inStart.x, midPos.x, timeElapsed/enterTime), midPos.y, 0);
            if(timeElapsed <= exitTime) {
                outImage.rectTransform.anchoredPosition = new Vector3(EasingFunction.EaseOutCubic(midPos.x, outEnd.x, timeElapsed/enterTime), midPos.y, 0);
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        inImage.rectTransform.anchoredPosition = midPos;
        outImage.rectTransform.anchoredPosition = outEnd;
    }

}
