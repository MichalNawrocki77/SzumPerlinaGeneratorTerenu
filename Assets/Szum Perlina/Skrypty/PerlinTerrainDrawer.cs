using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinTerrainDrawer : MonoBehaviour
{
    Terrain terrain;

    int width = 256, depth = 20, height = 256;
    [SerializeField]
    float perlinGrids, offsetX, offsetY;

    void Start()
    {
        terrain = GetComponent<Terrain>();
    }
    void Update()
    {
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width+1;

        terrainData.size = new Vector3 (width,depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width,height];
        for(int x = 0; x < width; x++)
        {
            for( int y = 0; y < height;y++)
            {
                heights[x, y] = GetHeightFromPerlinNoise(x, y);
            }
        }
        return heights;
    }

    private float GetHeightFromPerlinNoise(float x, float y)
    {
        float perlinX = ((x / width) * perlinGrids) + offsetX;
        float perlinY = ((y / height) * perlinGrids) + offsetY;

        return Mathf.PerlinNoise(perlinX, perlinY);
    }
}
