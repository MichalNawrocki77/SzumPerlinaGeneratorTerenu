using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PerlinMeshGenerator
{
    static MeshGeometry GenerateMeshGeometry(float[,] heightMap)
    {
        MeshGeometry meshGeometry = new MeshGeometry(heightMap.GetLength(0), heightMap.GetLength(1));
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        //I want the whole Mesh to be centered (i.e. the 0,0,0 cooridnates of the local position of the object "holding" this mesh is supposed to be in the middle of this mesh) thus I have to make sure that the range of X coordinates is (-HalfOfWidth;HalfOfWidth), with both ends inclusive. Same goes for Z coordinates, their range is supposed to be (-HalfOfHeight;HalfOfHeight), where -HalfOfHeight is at the bottom and HalfOfHeight is at the top. By giving the vertices such X and Y coordinates, I make sure that they are offset by said amount resulting in the object's localPosition of 0,0,0 to be in the center of generated mesh, instead of the bottom left conrner. That's why I need the coords of topLeftX and topLeftZ.
        int topLeftX = (width - 1) / -2;
        int topLeftZ = (width - 1) / -2;

        int currentVericiesIndex = 0;

        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < height; z++)
            {
                //I add the x value to the topLeftX because each consecutive X is supposed to placed a bit to the right
                //I subtract the z value from topLeftZ becauses each consecutive Z is supposed to be placed a bit lower
                meshGeometry.verticies[currentVericiesIndex] = new Vector3(topLeftX+x, heightMap[x, z], topLeftZ-z);
                meshGeometry.uvs[currentVericiesIndex] = new Vector2(x / width, z / height);
                

                //No triangle will have it's starting vertex index be the rightmost index of X dimension. Same goes for Z dimension. The reason is that after the rightmost vertex there are no more verticies to create the triangle.
                if(x<width-1 && z < height - 1)
                {
                    meshGeometry.AddTriangle(currentVericiesIndex,
                        currentVericiesIndex + width,
                        currentVericiesIndex + width + 1);

                    meshGeometry.AddTriangle(currentVericiesIndex,
                        currentVericiesIndex + 1,
                        currentVericiesIndex + width + 1);
                }
            }
        }

        return meshGeometry;
    } 

    public static Mesh GenerateMesh(float[,] heightMap)
    {
        MeshGeometry meshGeometry = GenerateMeshGeometry(heightMap);

        Mesh mesh = new Mesh();
        mesh.SetVertices(meshGeometry.verticies);
        mesh.SetUVs(0,meshGeometry.uvs);
        mesh.triangles = meshGeometry.triangles;
        mesh.RecalculateNormals();

        return mesh;
    }
}
