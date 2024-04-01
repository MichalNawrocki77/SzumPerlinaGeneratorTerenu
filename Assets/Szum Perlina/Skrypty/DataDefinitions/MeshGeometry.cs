using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct MeshGeometry
{
    public Vector3[] verticies;
    public int[] triangles;
    public Vector2[] uvs;

    private int trianglesIndex;

    public MeshGeometry(int width, int height)
    {
        verticies = new Vector3[width * height];
        triangles = new int[((width-1) * (height-1)) * 6];
        uvs = new Vector2[width * height];

        trianglesIndex = 0;
    }
    public void AddTriangle(int x, int y, int z)
    {
        triangles[trianglesIndex] = x;
        triangles[trianglesIndex+1] = y;
        triangles[trianglesIndex+2] = z;

        trianglesIndex += 3;
    }

}
