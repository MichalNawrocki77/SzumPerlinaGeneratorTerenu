

using Unity.VisualScripting;

using UnityEngine;

public class PerlinTextureDrawer : MonoBehaviour
{
    
    [SerializeField]
    Material perlinMat;
    [SerializeField]
    float offsetX, offsetY, perlinGrids;
    [SerializeField]
    int textureWidth, textureHeight;

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
        renderComponent.material.mainTexture = GenerateTextureWithPerlinNoise(textureWidth, textureHeight, offsetX, offsetY, perlinGrids);
    }
    Texture2D GenerateTextureWithPerlinNoise(
        int width, int height,float offsetX, float offsetY, float perlingGrids)
    {
        Texture2D texture = new Texture2D(width,height);
        for(int x=0; x < width; x++)
        {
            for(int y=0; y < height; y++)
            {
                float perlinX = (((float)x / width) * perlingGrids) + offsetX;
                float perlinY = (((float)y / height) * perlingGrids) + offsetY;
                float perlinOut = PerlinNoise.Perlin2D(perlinX, perlinY);
                //float perlinOut = Mathf.PerlinNoise(perlinX,perlinY);
                


                texture.SetPixel(x, y, new Color(-perlinOut, -perlinOut,
                    -perlinOut));
            }
        }
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
}
