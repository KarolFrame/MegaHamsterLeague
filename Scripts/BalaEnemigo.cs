using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    Transform tr;
    public float velocidad = 4;
    float temporizador = 10;

    Player jugador;

    SoundManager soundManager;

    float danoHaceBala;

    void Start()
    {
        tr = transform;
        jugador = FindObjectOfType<Player>();
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.SeleccionAudio(11, 0.5f);
    }
    void Update()
    {
        tr.Translate(Vector3.forward * velocidad * Time.deltaTime);
        temporizador -= Time.deltaTime;
        if (temporizador <= 0)
            Destroy(gameObject);
    }

    //SET
    public void SetDano(float dano)
    {
        danoHaceBala = dano;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == jugador)
        {
            jugador.SetVida(
                jugador.GetVida() - danoHaceBala
                );
            Retroceso retroceso = other.GetComponent<Retroceso>();
            if (retroceso != null)
                retroceso.ActivarRetroceso(Vector3.forward);
            Destroy(gameObject);
        }
    }
}

