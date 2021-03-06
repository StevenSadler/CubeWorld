﻿using UnityEngine;

public class NoiseUtils
{
    static int minHeight = 4;
    static int maxHeight = 12;
    static float smooth = 0.01f;
    static int octaves = 4;
    static float persistence = 0.5f;
    static int offset = 20000;

    public static int GenerateStoneHeight(float x, float z) {
        //float height = Map(minHeight, maxHeight - 5, 0, 1, FractalBrownianMotion(x * smooth * 2, z * smooth * 2, octaves + 1, persistence));
        float height = FractalBrownianMotion(x * smooth * 2, z * smooth * 2, octaves + 1, persistence);
        height = Mathf.Pow(height, 4);
        height = height * 100 + 5;
        return (int)height;
    }

    public static int GenerateHeight(float x, float z) {
        //float height = Map(minHeight, maxHeight, 0, 1, FractalBrownianMotion(x * smooth, z * smooth, octaves, persistence));
        float height = FractalBrownianMotion(x * smooth, z * smooth, octaves, persistence);
        height = Mathf.Pow(height, 3);
        height = height * 100 + 20;
        return (int)height;
    }

    public static float FractalBrownianMotion3D(float x, float y, float z, float smooth, int octaves) {
        float persistence = 0.5f;
        x *= smooth;
        y *= smooth;
        z *= smooth;

        float xy = FractalBrownianMotion(x, y, octaves, persistence);
        float xz = FractalBrownianMotion(x, z, octaves, persistence);

        float yx = FractalBrownianMotion(y, x, octaves, persistence);
        float yz = FractalBrownianMotion(y, z, octaves, persistence);

        float zx = FractalBrownianMotion(z, x, octaves, persistence);
        float zy = FractalBrownianMotion(z, y, octaves, persistence);

        return (xy + xz + yx + yz + zx + zy) / 6.0f;
    }

    static float Map(float newmin, float newmax, float origmin, float origmax, float value) {
        return Mathf.Lerp(newmin, newmax, Mathf.InverseLerp(origmin, origmax, value));
    }

    static float FractalBrownianMotion(float x, float z, int oct, float pers) {
        x += offset;
        z += offset;

        float total = 0;
        float frequency = 1;
        float amplitude = 1;
        float maxValue = 0;
        for (int i = 0; i < oct; i++) {
            total += Mathf.PerlinNoise(x * frequency, z * frequency) * amplitude;

            maxValue += amplitude;

            amplitude *= pers;
            frequency *= 2;
        }

        return total / maxValue;
    }
}
