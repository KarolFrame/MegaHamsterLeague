using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruirPoMalla : MonoBehaviour
{
    bool tocandoSuelo;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dwn = transform.TransformDirection(Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(transform.position, dwn, out hit, 0.2f) && hit.collider.CompareTag("Floor"))
        {
            tocandoSuelo = true;
        }
        else
        {
            tocandoSuelo = false;
        }

        if (!tocandoSuelo)
            Destroy(gameObject);
    }

}
