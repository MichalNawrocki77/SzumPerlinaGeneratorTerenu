using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class PerlinTerrainGenerator
{
    public static TerrainData GenerateTerrain(TerrainData terrainData, int width, int height, int length, int seed, int octaves, float scale, float persistance, float lacunarity, float offsetX, float offsetY)
    {
        terrainData.heightmapResolution = width + 1;

        terrainData.size = new Vector3(width, height, length);
        terrainData.SetHeights(0, 0, PerlinNoise.Get2DPerlinMap(width, length, seed, scale, octaves, persistance, lacunarity, offsetX, offsetY));
        return terrainData;
    }

    //static float[,] GenerateHeights(int width, int length, int seed, int octaves, float perlinGrids, float persistance, float lacunarity, float offsetX, float offsetY)
    //{
    //    return PerlinNoise.Get2DPerlinMap(width, length,
    //        seed,
    //        perlinGrids, octaves, persistance, lacunarity,
    //        offsetX, offsetY);

    //}
}
