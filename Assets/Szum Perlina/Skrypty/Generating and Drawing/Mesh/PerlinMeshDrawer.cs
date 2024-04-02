using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using Assets.Szum_Perlina.Skrypty;

using UnityEngine;

public class PerlinMeshDrawer : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;

    [SerializeField]
    float offsetX, offsetY, scale,meshHeight, lacunarity, persistance;
    
    [SerializeField]
    int Width, Height, octaves, seed;
    
    [SerializeField]
    AnimationCurve heightCurve;

    [SerializeField]
    Region[] regions;


    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if(meshRenderer is null)
        {
            throw new NullReferenceException("Mesh Renderer Component Not Found!");
        }

        meshFilter = GetComponent<MeshFilter>();
        if(meshFilter is null)
        {
            throw new NullReferenceException("MeshFilter Component Not Found!");
        }        
    }
    private void Update()
    {
        float[,] perlinHeightMap = PerlinNoise.Get2DPerlinMap(Width, Height,
            seed, scale, octaves, persistance, lacunarity,
            offsetX, offsetY);

        Mesh perlinMesh = PerlinMeshGenerator.GenerateMesh(perlinHeightMap, meshHeight, heightCurve);
        Texture perlinRegionTexture = PerlinTextureGenerator.Generate2DRegionMap(Width, Height,
            seed, scale, octaves, persistance, lacunarity,
            offsetX, offsetY,
            regions);

        DrawMesh(perlinMesh, perlinRegionTexture);
    }
    void DrawMesh(Mesh mesh, Texture meshTexture)
    {
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = meshTexture;
    }

}
