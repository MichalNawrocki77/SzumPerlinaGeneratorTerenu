
using Assets.Szum_Perlina.Skrypty;

using UnityEngine;

public static class PerlinTextureGenerator
{
    public static Texture Generate2DRegionMap(int textureWidth, int textureHeight,
        int seed, float scale, int octaves, float persistance, float lacunarity, float offsetX, float offsetY, Region[] regions)
    {

        Texture2D texture = new Texture2D(textureWidth, textureHeight);

        float[,] heightMap = PerlinNoise.Get2DPerlinMap(textureWidth, textureHeight,
            seed,
            scale, octaves, persistance, lacunarity,
            offsetX, offsetY);

            Color[] colors = new Color[textureWidth * textureHeight];
        
        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                for (int i = 0; i < regions.Length; i++)
                {
                    //Every single time I go through any noise generated map I go column by column. But since the 1D array given to the Texture2D.SetPixels() must go row by row, I flip the x and y, thus I get heightMap[y, x] and not heightMap[x,y]
                    if (heightMap[y, x] <= regions[i].height)
                    {
                        colors[x * heightMap.GetLength(0) + y] = regions[i].color;
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
    public static Texture2D Generate2DRegionMap(float[,] heightMap, Region[] regions)
    {

        Texture2D texture = new Texture2D(heightMap.GetLength(0), heightMap.GetLength(1));

        Color[] colors = new Color[heightMap.GetLength(0) * heightMap.GetLength(1)];

        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                for (int i = 0; i < regions.Length; i++)
                {
                    //Every single time I go through any noise generated map I go column by column. But since the 1D array given to the Texture2D.SetPixels() must go row by row, I flip the x and y, thus I get heightMap[y, x] and not heightMap[x,y]
                    if (heightMap[y, x] <= regions[i].height)
                    {
                        colors[x * heightMap.GetLength(0) + y] = regions[i].color;
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

    public static Texture2D Generate2DRegionMapForTerrainTool(float[,] heightMap, Region[] regions)
    {

        Texture2D texture = new Texture2D(heightMap.GetLength(0), heightMap.GetLength(1));

        Color[] colors = new Color[heightMap.GetLength(0) * heightMap.GetLength(1)];

        for (int x = 0; x < heightMap.GetLength(0); x++)
        {
            for (int y = 0; y < heightMap.GetLength(1); y++)
            {
                for (int i = 0; i < regions.Length; i++)
                {
                    //When creating a texture for Terrain Tool I have to go column by column and not row by row (like I did using SetPixels()), thus here I get heightMap[x,y] and not heightMap[y,x] like I did using SetPixels()
                    if (heightMap[x, y] <= regions[i].height)
                    {
                        colors[x * heightMap.GetLength(0) + y] = regions[i].color;
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

    public static Texture2D GenerateBlackWhiteTextureWithPerlinNoise(
        int width, int height, float offsetX, float offsetY, float perlingGrids)
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float perlinX = (((float)x / width) * perlingGrids) + offsetX;
                float perlinY = (((float)y / height) * perlingGrids) + offsetY;
                float myPerlinOut = (PerlinNoise.GetPerlin2DPoint(perlinX, perlinY) + 1) / 2;
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
}
