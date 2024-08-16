using UnityEngine;
using Assets.Szum_Perlina.Skrypty;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;

public class PerlinTerrainDrawer : MonoBehaviour
{
    Terrain terrain;


    [SerializeField]
    float offsetX, offsetY, scale, terrainHeight, lacunarity, persistance;

    [SerializeField]
    int Width, Length, octaves, seed;

    [SerializeField]
    AnimationCurve heightCurve;

    [SerializeField]
    Region[] regions;

 

    
    // Start is called before the first frame update
    void Start()
    {
        terrain = GetComponent<Terrain>();
        if(terrain is null)
        {
            Debug.LogError("Terrain Component Not Found! at PerlinTerrainDrawer");
        }
    }
    private void Update()
    {
        DrawTerrain();
    }

    void DrawTerrain()
    {
        float[,] heightMap = PerlinNoise.Get2DPerlinMap(Width, Length,
                                                        seed, scale,
                                                        octaves, persistance, lacunarity,
                                                        offsetX, offsetY);

        Texture2D texture = PerlinTextureGenerator.Generate2DRegionMapForTerrainTool(heightMap, regions);
        TerrainLayer[] terrainLayers = { new TerrainLayer() };
        terrainLayers[0].diffuseTexture = texture;
        terrainLayers[0].tileSize = new Vector2(Width, Length);


        heightMap = PerlinTerrainGenerator.ApplyCurveToHeightMap(heightMap, heightCurve);

        terrain.drawInstanced = true;

        terrain.terrainData.heightmapResolution = Width + 1;
        terrain.terrainData.size = new Vector3(Width, terrainHeight, Length);
        terrain.terrainData.SetHeights(0, 0, heightMap);
        terrain.terrainData.terrainLayers = terrainLayers;

    }
}
