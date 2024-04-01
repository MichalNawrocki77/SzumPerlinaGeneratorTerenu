using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMeshDrawer : MonoBehaviour
{
    MeshRenderer meshRenderer;
    MeshFilter meshFilter;
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


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
