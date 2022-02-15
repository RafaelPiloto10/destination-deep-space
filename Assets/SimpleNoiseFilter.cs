using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNoiseFilter
{
    NoiseSettings settings;
    SimplexNoise noise = new SimplexNoise();

    public SimpleNoiseFilter(NoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float freq = settings.baseRoughness;
        float amp = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = noise.Evaluate(point * freq + settings.center);
            noiseValue += (v + 1) * .5f * amp;
            freq *= settings.roughness;
            amp *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
