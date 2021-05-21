using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DrumType {Left, Right}
public class Drum : MonoBehaviour
{
    public DrumType drumType = DrumType.Left;
    public DrumVisuals visuals;
    private bool active = false;
    private bool wasHit = true;

    
    // Start is called before the first frame update
    void Start()
    {
        visuals = GetComponent<DrumVisuals>();
    }

    public void Activate() {
        StartCoroutine("HoldWindowOpen");
        wasHit = false;
    }

    public virtual void OnPress() {
        visuals.OnPress();
        if(active) {
            OnHit();
        } else {
            OnMiss();
        }
    }

    public virtual void OnMiss() {
        GameManager.i.subtractFillMiss();
        visuals.OnMiss();
    }

    public virtual void OnHit() {
        GameManager.i.addFill();
        wasHit = true;
        visuals.OnHit();
    }

    public virtual void OnPass() {
        GameManager.i.subtractFillPass();
        visuals.OnPass();
    }

    IEnumerator HoldWindowOpen() {
        active = true;
        yield return new WaitForSeconds(GameManager.i.settings.window);
        active = false;
        if(!wasHit) {
            OnPass();
        }
    }
}
