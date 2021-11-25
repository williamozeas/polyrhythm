using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsSlider : MonoBehaviour
{
    Slider slider;
    
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = 1f;
    }

    public void UpdateVolume() {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName("Volume", 1.4f - slider.value);
    }

    public void UpdateGlow() {
        PostProcessing.i.UpdateGlow(slider.value);
    }

    public void UpdateShake() {
        CameraShake.i.UpdateShake(0.1f + 0.9f * slider.value);
    }

}
