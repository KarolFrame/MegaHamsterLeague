using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorretaJugador : MonoBehaviour
{
    float distanciaDeteccion = 10;
    bool seActiva = false;

    public GameObject ejeCabeza;
    public GameObject balaPrefab;
    public Transform spawnBalas;

    float temporizadorDisparo = 1;
    float temporizadorCambio = 1;

    StatsIA[] enemigos;
    SoundManager soundManager;

    public Image filledVida;

    float timepoDeVida = 40;
    void Start()
    {
        seActiva = false;
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemigos = FindObjectsOfType<StatsIA>();
        if (enemigos.Length > 0 && Vector3.Distance(transform.position, enemigos[0].transform.position) <= 20)
        {
            ejeCabeza.transform.LookAt(enemigos[0].transform.position);
            Disparo();
        }

        //vida
        timepoDeVida -= Time.deltaTime;
        filledVida.fillAmount = timepoDeVida/ 40;

        if (timepoDeVida <=0)
        {
            Destroy(gameObject);
        }
    }
    void Disparo()
    {
        temporizadorDisparo -= Time.deltaTime;
        if (temporizadorDisparo <= 0)
        {
            soundManager.SeleccionAudio(15, 0.5f);

            GameObject clonBala = Instantiate(balaPrefab);

            clonBala.transform.position = spawnBalas.position;
            clonBala.transform.rotation = spawnBalas.transform.rotation;

            //ASIGNAR STATS A LA BALA
            Bala bala = clonBala.GetComponent<Bala>();
            bala.SetDano(10);

            temporizadorDisparo = Random.Range(0.1f, 0.9f);
        }
    }
}
