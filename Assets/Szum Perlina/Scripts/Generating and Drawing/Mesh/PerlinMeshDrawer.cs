using System;

using Assets.Szum_Perlina.Skrypty;

using UnityEngine;
using UnityEngine.Profiling;

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

    int framecount;

    private void Awake()
    {
        framecount = 0;
        Profiler.maxUsedMemory = 1610612736;
    }
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

        float[,] perlinHeightMap = PerlinNoise.Get2DPerlinMap(Width, Height,
            seed, scale, octaves, persistance, lacunarity,
            offsetX, offsetY);

        Mesh perlinMesh = PerlinMeshGenerator.GenerateMesh(perlinHeightMap, meshHeight, heightCurve);
        Texture2D perlinRegionTexture = PerlinTextureGenerator.Generate2DRegionMap(perlinHeightMap, regions);

        DrawMesh(perlinMesh, perlinRegionTexture);
    }
    private void Update()
    {
        framecount++;
        if (framecount >= 99)
        {
            Profiler.enabled = false;
        }
    }
    void DrawMesh(Mesh mesh, Texture2D meshTexture)
    {        
        meshFilter.sharedMesh = mesh;
        meshRenderer.sharedMaterial.mainTexture = meshTexture;
    }

}
