using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerToGhostMatLink : MonoBehaviour
{
    public MeshRenderer mesh;
    public Material defaultMat, cosmetic1Mat, cosmetic2Mat;

    public void SetMaterial(int i)
    {
        if (i == 0) mesh.material = defaultMat;
        else if (i == 1) mesh.material = cosmetic1Mat;
        else if (i == 2) mesh.material = cosmetic2Mat;
    }
}
