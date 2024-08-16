

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

                renderComponent.material.mainTexture = PerlinTextureGenerator.GenerateBlackWhiteTextureWithPerlinNoise(textureWidth, textureHeight, offsetX, offsetY, perlinGrids);

                break;

            case DrawType.RegionColorMap:

                renderComponent.material.mainTexture = PerlinTextureGenerator.Generate2DRegionMap(
                    textureWidth, textureHeight,
                    seed,
                    scale, octaves, persistance, lacunarity,
                    offsetX, offsetY,
                    regions);

                break;
        }
    }       
}
