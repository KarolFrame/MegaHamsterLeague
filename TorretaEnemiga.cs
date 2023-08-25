using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorretaEnemiga : MonoBehaviour
{
    public GameObject ejeCabeza;
    bool apuntaEnemigo = false;

    Player jugador;

    Transform tr;

    public GameObject balaprefab;
    public Transform spawnBala;

    float temporizadorDisparo = 3;
    float temporizadorCambio = 3;

    float vida = 100;
    SoundManager soundManager;

    void Start()
    {
        jugador = FindObjectOfType<Player>();
        tr = transform;

        soundManager = FindObjectOfType<SoundManager>();
    }
    void Update()
    {
        //DISTANCIA
        if (Vector3.Distance(tr.position, jugador.transform.position) <= 20)
        {
            apuntaEnemigo = true;
        }
        else
            apuntaEnemigo = false;

        //ACTIVAR TORRETA
        if (apuntaEnemigo)
        {
            CabezaTorreta();
            Disparo();
        }

        //MUERTE
        if(vida<=0)
        {
            Destroy(gameObject);
        }

    }
    void CabezaTorreta()
    {
        ejeCabeza.transform.LookAt(jugador.transform.position);
    }
    void Disparo()
    {
        temporizadorDisparo -= Time.deltaTime;
        if (temporizadorDisparo <= 0)
        {
            soundManager.SeleccionAudio(15, 0.5f);

            GameObject clonBala = Instantiate(balaprefab);

            clonBala.transform.position = spawnBala.position;
            clonBala.transform.rotation = spawnBala.transform.rotation;

            //ASIGNAR STATS A LA BALA
            BalaEnemigo bala = clonBala.GetComponent<BalaEnemigo>();
            bala.SetDano(10);

            temporizadorDisparo = Random.Range(1, 3);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Bala>() != null)
        {
            vida -= other.GetComponent<Bala>().GetDano();
            Destroy(other);
        }
    }
}
