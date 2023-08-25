using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SumarCargaUlti : MonoBehaviour
{
    GameObject jugador;
    Player scripsJugador;
    void Start()
    {
        jugador = GameObject.Find("Player Holder");
        scripsJugador = jugador.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="IA")
        {
            //scripsJugador.SetCargaUlti(1);
        }
    }
}
