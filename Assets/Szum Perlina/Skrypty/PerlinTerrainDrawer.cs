using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrainDrawer : MonoBehaviour
{
    Terrain terrain;
    [SerializeField]
    int width, height, length, octaves, seed;
    [SerializeField]
    float perlinGrids, offsetX, offsetY, lacunarity, persistance;

    void Start()
    {
        terrain = GetComponent<Terrain>();
    }
    private void Update()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    public void OnGenerateClick()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width+1;

        terrainData.size = new Vector3 (width,height, length);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        return PerlinNoise.Get2DPerlinMap(width, length,
            seed,
            perlinGrids, octaves, persistance, lacunarity,
            offsetX, offsetY);

    }
}
