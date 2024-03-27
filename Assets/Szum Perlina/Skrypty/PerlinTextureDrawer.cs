

using System;
using System.IO;

using Assets.Szum_Perlina.Skrypty;

using Unity.VisualScripting;

using UnityEngine;

public class PerlinTextureDrawer : MonoBehaviour
{
    
    [SerializeField]
    Material perlinMat;

    [SerializeField]
    float offsetX, offsetY, perlinGrids, scale, lacunarity, persistance;
    [SerializeField]
    int textureWidth, textureHeight, octaves, seed;

    [SerializeField]
    DrawType drawType;
    [SerializeField]
    Region[] regions;


    Renderer renderComponent;
    // Start is called before the first frame update
    void Start()
    {
        renderComponent = GetComponent<Renderer>();
        if(renderComponent == null)
        {
            renderComponent = transform.AddComponent<MeshRenderer>();
            renderComponent.sharedMaterial = perlinMat;
        }

    }
    void Update()
    {
        switch (drawType)
        {
            case DrawType.BlackAndWhite:
                renderComponent.material.mainTexture = GenerateBlackWhiteTextureWithPerlinNoise(textureWidth, textureHeight, offsetX, offsetY, perlinGrids);
                break;
            case DrawType.RegionColorMap:
                renderComponent.material.mainTexture = Generate2DRegionMap(
                    textureWidth, textureHeight,
                    seed,
                    scale, octaves, persistance, lacunarity,
                    offsetX, offsetY);
                break;
        }
    }   

    Texture2D GenerateBlackWhiteTextureWithPerlinNoise(
        int width, int height,float offsetX, float offsetY, float perlingGrids)
    {
        Texture2D texture = new Texture2D(width,height);
        for(int x=0; x < width; x++)
        {
            for(int y=0; y < height; y++)
            {
                float perlinX = (((float)x / width) * perlingGrids) + offsetX;
                float perlinY = (((float)y / height) * perlingGrids) + offsetY;
                float myPerlinOut = (PerlinNoise.GetPerlin2DPoint(perlinX, perlinY)+1)/2;
                //float mathfPerlinOut = Mathf.PerlinNoise(perlinX,perlinY);

                texture.SetPixel(x, y, new Color(myPerlinOut, myPerlinOut,
                    myPerlinOut));
                //texture.SetPixel(x, y, new Color(mathfPerlinOut, mathfPerlinOut,
                //    mathfPerlinOut));
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
    private Texture Generate2DRegionMap(int textureWidth, int textureHeight,
        int seed,
        float scale, int octaves, float persistance, float lacunarity,
        float offsetX, float offsetY)
    {
        Texture2D texture = new Texture2D(textureWidth,textureHeight);
        float[,] heightMap = PerlinNoise.Get2DPerlinMap(textureWidth, textureHeight,
            seed,
            scale, octaves, persistance, lacunarity,
            offsetX, offsetY);

        Color[] colors = new Color[textureWidth * textureHeight];
        for(int x = 0; x < heightMap.GetLength(0); x++)
        {
            for(int y = 0; y < heightMap.GetLength(1); y++)
            {
                for(int i=0;i<regions.Length;i++)
                {
                    if (heightMap[x,y] <= regions[i].height)
                    {
                        colors[x*heightMap.GetLength(1)+y] = regions[i].color;
                    }
                }

            }
        }

        texture.SetPixels(colors);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
}
