using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrumType {Left, Right}
public class Drum : MonoBehaviour
{
    public DrumType drumType = DrumType.Left;
    public DrumVisuals visuals;
    private bool active = false;
    private int intensity;
    [SerializeField]
    protected bool addsToScore;
    private int activeNotes = 0;
    private int hits = 0;


    
    // Start is called before the first frame update
    void Start()
    {
        visuals = GetComponent<DrumVisuals>();
    }

    public void Activate(ref int intensityIn) {
        StartCoroutine("HoldWindowOpen");
        visuals.OnActivate(intensityIn);
        intensity = intensityIn;
    }

    public virtual void OnPress() {
        visuals.OnPress();
        if(active) {
            OnHit(intensity);
        } else {
            //StartCoroutine("HoldHit");
            OnMiss();
        }
    }

    public virtual void OnMiss() {
        if (addsToScore) {
            GameManager.i.subtractFillMiss();
            Debug.Log("Miss");
        }
        visuals.OnMiss();
    }

    public virtual void OnHit(int intensity) {
        if (addsToScore) {
            GameManager.i.addFill();
            hits++;
            Debug.Log("Hit");
        }
        visuals.OnHit(intensity);
        active = false;
    }

    public virtual void OnPass() {
        if (addsToScore) {
            GameManager.i.subtractFillPass();
            Debug.Log("Pass");
        }
        visuals.OnPass();
    }

    IEnumerator HoldWindowOpen() {
        active = true;
        activeNotes++;
        yield return new WaitForSeconds(GameManager.i.settings.window);
        activeNotes--;
        if(activeNotes == 0) active = false;
        if(hits <= 0) {
            OnPass();
        } else {
            hits--;
        }
    }

    IEnumerator HoldHit() {
        float timeElapsed = 0;
        while(timeElapsed < GameManager.i.settings.earlyDelay) {
            if(active) {
                OnHit(intensity);
                yield break;
            }
            timeElapsed += Time.deltaTime;
        }
        OnMiss();
    }
}
