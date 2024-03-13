using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;

public class PerinIn3D : MonoBehaviour
{
    [SerializeField]
    float offsetX, offsetY;
    [SerializeField]
    int Width, Height, perlinGrids;

    List<GameObject> cubes;
    private void Start()
    {
        cubes = new List<GameObject>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateNoiseMap()
    {
        foreach(GameObject cube in cubes)
        {
            Destroy(cube);
        }


        for(int x = 0; x < Width; x++)
        {
            for(int y = 0; y < Height; y++)
            {
                //for (int z = 0; z < resolution; z++)
                //{
                //    double perlinOut = PerlinNoise.Perlin3D(x*scale, y*scale, z*scale);
                //    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                //    cube.transform.position = new Vector3(x, y, z);
                //    cube.transform.localScale = new Vector3((float)perlinOut, (float)perlinOut, (float)perlinOut);

                //    cubes.Add(cube);
                //}

                float perlinX = (((float)x/Width) * perlinGrids) + offsetX;
                float perlinY = (((float)y/Height) * perlinGrids) + offsetY;

                float perlinOut = Mathf.PerlinNoise(perlinX, perlinY);
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = new Vector3(x, perlinOut * 3, y);
                //cube.transform.GetComponent<Renderer>().material.color = new Color(perlinOut, perlinOut, perlinOut);

                cubes.Add(cube);
            }
        }
    }
}
