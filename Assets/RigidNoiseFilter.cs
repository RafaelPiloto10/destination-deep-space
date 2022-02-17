using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidNoiseFilter : INoiseFilter
{
    NoiseSettings.RigidNoiseSettings settings;
    SimplexNoise noise = new SimplexNoise();

    public RigidNoiseFilter(NoiseSettings.RigidNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float freq = settings.baseRoughness;
        float amp = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * freq + settings.center));
            v *= v;
            v *= weight;
            weight = Mathf.Clamp(v * settings.weightMultiplier, 0, 1);
            noiseValue += v * amp;
            freq *= settings.roughness;
            amp *= settings.persistence;
        }

        noiseValue = Mathf.Max(0, noiseValue - settings.minValue);
        return noiseValue * settings.strength;
    }
}
