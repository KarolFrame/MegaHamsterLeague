using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirCofre : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Ataque1"))
        {
            Ray rayo = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Si el rayo choca con algo, se procesa el choque
            if (Physics.Raycast(rayo, out hit))
            {
                if (hit.collider.GetComponent<GeneradorCofre>() != null)
                {
                    //Si el cofre esta cerrado
                    if(!hit.collider.GetComponent<GeneradorCofre>().GetBoolAbierto())
                    {
                        hit.collider.GetComponent<GeneradorCofre>().GenerarPrefabs();
                        hit.collider.GetComponent<GeneradorCofre>().SetBoolAbierto();
                        hit.collider.GetComponent<GeneradorCofre>().GestionarCofreMalla();
                    }
                }
            }
        }
    }
}
