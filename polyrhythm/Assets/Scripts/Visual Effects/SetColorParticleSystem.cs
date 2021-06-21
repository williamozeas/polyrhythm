using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetColorParticleSystem : SetColor
{
    private ParticleSystem.MainModule particles;

    protected override void GetColoredComponent()
    {
        particles = GetComponent<ParticleSystem>().main;
    }

    protected override void UpdateColorOfComponent()
    {
        particles.startColor = StyleManager.i.GetColor(sideNumber, styleColor);
    }
   
}
