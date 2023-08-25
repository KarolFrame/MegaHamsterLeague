using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    //COMPONENTES
    public Animator animKiko, animMago;
    NavMeshAgent agent;
    Transform tr;
    StatsIA stats;

    float targetHorizontal, targetVertical;

    //MALLAS
    public GameObject  bolamagica;
    public GameObject prefabBola;

    public Transform spawnBola;

    float temporizadorDisparo = 3;
    float temporizadorCambio = 3;

    GameObject jugador;
    float distanciaEntreIAyJugador;
    Vector3 direccion;


    public float vida = 100;
    float danoProducidoPorBala = 0;

    bool fueraDelArea;

    Vector3 movement;
    Vector3 destino;

    bool envenenado;
    bool activarDesenvenenamiento;
    float tiempoenvenenado;

    public GameObject sangrePrefab;



    void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        tr = transform;

        jugador = GameObject.Find("Player Holder");

        fueraDelArea = false;

        envenenado = false;
        activarDesenvenenamiento = false;

        targetHorizontal = Random.Range(-1f, 1f);
        targetVertical = Random.Range(-1f, 1f);

        //STATS
        stats = GetComponent<StatsIA>();
        stats.SetTipoIA(2);
    }
    void Update()
    {
        if(agent.enabled)
        {
            agent.destination = jugador.transform.position;
            temporizadorDisparo -= Time.deltaTime;
            if (temporizadorDisparo <= 0)
            {
                GameObject clonBala = Instantiate(prefabBola);

                clonBala.transform.position = spawnBola.position;
                clonBala.transform.rotation = bolamagica.transform.rotation;

                //ASIGNAR STATS A LA BALA
                Bola bala = clonBala.GetComponent<Bola>();
                bala.SetDano(stats.GetDanoAtaque());

                temporizadorDisparo = Random.Range(1, 5);
            }
        }


        //ENVENENAMIENTO
        if (envenenado)
            stats.SetVida(
                stats.GetVida() - 5 * Time.deltaTime
                );
        if (activarDesenvenenamiento)
        {
            tiempoenvenenado -= Time.deltaTime;
            if (tiempoenvenenado <= 0)
            {
                envenenado = false;
                activarDesenvenenamiento = false;
            }
        }

        //VIDA
        if (stats.GetVida() <= 0)
        {
            GeneraCum();
            Destroy(gameObject);
        }

        /*if (fueraDelArea)
            vida -= 5 * Time.deltaTime;*/
    }
    void GeneraCum()
    {
        GameObject cloCum = Instantiate(sangrePrefab);
        cloCum.transform.position = tr.position;
        cloCum.transform.Rotate(cloCum.transform.rotation.x, cloCum.transform.rotation.y, Random.Range(0, 361));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "AreaMuerte")
        {
            fueraDelArea = false;
        }
        if (other.gameObject.tag == "Muro")
        {
            envenenado = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "AreaMuerte")
        {
            fueraDelArea = true;
        }
        if (other.gameObject.tag == "Muro")
        {
            activarDesenvenenamiento = true;
            tiempoenvenenado = 5;
        }
    }
}
