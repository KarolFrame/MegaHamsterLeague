using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Camara cam;
    Transform tr;
    void Start()
    {
        cam = FindObjectOfType<Camara>();
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.LookAt(cam.transform);
    }
}
