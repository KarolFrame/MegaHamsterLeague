using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lol : MonoBehaviour
{
    public List<MeshRenderer> mesh;
    public List<Material> material;
    void Start()
    {
        foreach (MeshRenderer child in transform)
        {
            Debug.Log(child.name);
        }
    }

    
    
}
