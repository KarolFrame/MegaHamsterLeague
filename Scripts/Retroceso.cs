using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Retroceso : MonoBehaviour
{
    NavMeshAgent nav;
    Transform tr;
    float tempoRetroceso = 0.03f;
    bool activarRetroceso = false;
    Vector3 direccionRetroceso;
    Rigidbody rb;
    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        tr = transform;
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (activarRetroceso)
        {
            tempoRetroceso -= Time.deltaTime;
            nav.enabled = false;
            
            tr.Translate(direccionRetroceso * 60 * Time.deltaTime);
            if (tempoRetroceso <= 0)
                activarRetroceso = false;
        }
        else
        {
            nav.enabled = true;
        }

    }
    public void ActivarRetroceso(Vector3 dir)
    {
        tempoRetroceso = 0.05f;
        dir = direccionRetroceso.normalized;
        activarRetroceso = true;
    }
}
