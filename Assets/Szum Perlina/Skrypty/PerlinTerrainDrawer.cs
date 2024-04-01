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
        terrain.terrainData = PerlinTerrainGenerator.GenerateTerrain(terrain.terrainData, width, height, length, seed, octaves, perlinGrids, persistance, lacunarity, offsetX, offsetY);
    }
}
