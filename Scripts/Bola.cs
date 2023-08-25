using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bola : MonoBehaviour
{
    Transform tr;
    public float velocidad=4;
    float temporizador = 10;

    public Animator anim;


    float dano;

    void Start()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.forward*velocidad*Time.deltaTime);
        temporizador -= Time.deltaTime;
        if (temporizador <= 0)
            Destroy(gameObject);
    }
    public void SetDano(float ndano)
    {
        dano = ndano;
    }
    private void OnTriggerEnter(Collider other)
    {
        Player jugador = other.GetComponent<Player>();
        if (jugador != null)
        {
            Retroceso retrocesoPlayer = jugador.GetComponent<Retroceso>();
            if(jugador.GetTengoEskudo())
            {
                Destroy(gameObject, 0.2f);
                jugador.SetTengoEskudo(false);
            }
            else
            {
                jugador.SetVida(
                    jugador.GetVida() - dano
                    );
                Destroy(gameObject);
            }
            if (retrocesoPlayer != null)
                retrocesoPlayer.ActivarRetroceso(tr.forward);

        }
        else
        {
            Destroy(gameObject);
        }

    }
}
