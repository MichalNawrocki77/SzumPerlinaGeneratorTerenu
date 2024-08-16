
using UnityEngine;

public static class PerlinTerrainGenerator
{
    public static float[,] ApplyCurveToHeightMap(float[,] heightMap, AnimationCurve heightCurve)
    {
        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int z = 0; z < heightMap.GetLength(1); z++)
            {
                heightMap[x, z] = heightCurve.Evaluate(heightMap[x, z]);
            }
        }
        return heightMap;
    }
}
