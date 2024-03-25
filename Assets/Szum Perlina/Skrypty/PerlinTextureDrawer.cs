

using System.IO;

using Unity.VisualScripting;

using UnityEngine;

public class PerlinTextureDrawer : MonoBehaviour
{
    StreamWriter writer;

    
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

        writer = new StreamWriter("C:\\Users\\Zibz¹77\\Desktop\\wynikiSzumu.csv");
    }
    void Update()
    {
        renderComponent.material.mainTexture = GenerateTextureWithPerlinNoise(textureWidth, textureHeight, offsetX, offsetY, perlinGrids);
    }
    Texture2D GenerateTextureWithPerlinNoise(
        int width, int height,float offsetX, float offsetY, float perlingGrids)
    {
        //writer.Write("sep=;\n");
        //writer.Write("moj;mathf\n");
        Texture2D texture = new Texture2D(width,height);
        for(int x=0; x < width; x++)
        {
            for(int y=0; y < height; y++)
            {
                float perlinX = (((float)x / width) * perlingGrids) + offsetX;
                float perlinY = (((float)y / height) * perlingGrids) + offsetY;
                float myPerlinOut = (PerlinNoise.GetPerlin2DPoint(perlinX, perlinY)+1)/2;
                float mathfPerlinOut = Mathf.PerlinNoise(perlinX,perlinY);

                texture.SetPixel(x, y, new Color(myPerlinOut, myPerlinOut,
                    myPerlinOut));
                //texture.SetPixel(x, y, new Color(mathfPerlinOut, mathfPerlinOut,
                //    mathfPerlinOut));

                //writer.Write($"{myPerlinOut};{mathfPerlinOut}\n");
            }
        }
        writer.Close();
        texture.filterMode = FilterMode.Point;
        texture.Apply();
        return texture;
    }
}
