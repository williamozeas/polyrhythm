using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSettings {
    //percent fill per hit, 0-1
    public float pph = 0.05f;
    //decay % per second, 0-1
    public float decay = 0.02f;
    //window of opportunity to hit (s)
    public float window = 0.15f;
    //penalty % per miss (wrong hit)
    public float penaltyMiss = 0.03f;
    //penalty % per pass (wrong hit)
    public float penaltyPass = 0.01f; 
    //window before marker misses, must be smaller than window
    public float earlyDelay = 0.015f;

    public GameSettings() { }

    public GameSettings(float pphIn, float decayIn, float windowIn, float penaltyMissIn, float penaltyPassIn) {
        pph = pphIn;
        decay = decayIn;
        window = windowIn;
        penaltyMiss = penaltyMissIn;
        penaltyPass = penaltyPassIn;
    }

    public GameSettings(float pphIn, float decayIn, float windowIn, float penaltyMissIn, float penaltyPassIn, float earlyWindowIn) {
        pph = pphIn;
        decay = decayIn;
        window = windowIn;
        penaltyMiss = penaltyMissIn;
        penaltyPass = penaltyPassIn;
        earlyDelay = earlyWindowIn;
    }
    
}
