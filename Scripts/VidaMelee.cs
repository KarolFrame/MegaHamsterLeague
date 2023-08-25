using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VidaMelee : MonoBehaviour
{
    bool llegaJugador = false;
    Player statsPlayer;
    int danoEnemigo;
    float cadacuantopega;
    float cooldown;
    StatsIA stats;
    NavMeshAgent agent;

    Animator animMelee;

    Retroceso retrocesoPlayer;

    public GameObject sangrePrefab;
    void Awake()
    {
        llegaJugador = false;
        danoEnemigo = 1;
        cadacuantopega = 1.5f;
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<StatsIA>();
        statsPlayer = FindObjectOfType<Player>();
        retrocesoPlayer = statsPlayer.GetComponent<Retroceso>();

        stats.SetTipoIA(4);

        animMelee = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (agent != null && agent.enabled)
        {
            agent.SetDestination(statsPlayer.transform.position);
        }

        if (statsPlayer !=null && Vector3.Distance(statsPlayer.transform.position, transform.position)<=2f)
            llegaJugador = true;
        else
            llegaJugador = false;
        if (statsPlayer != null && llegaJugador)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <=0)
            {
                animMelee.SetTrigger("Melee");

                if (statsPlayer.GetTengoEskudo())
                {
                    statsPlayer.SetTengoEskudo(false);

                    cooldown = cadacuantopega;
                }
                else
                {
                    statsPlayer.SetVida(
                    statsPlayer.GetVida() - stats.GetDanoAtaque()
                    );

                    cooldown = cadacuantopega;
                }
                if (retrocesoPlayer != null)
                    retrocesoPlayer.ActivarRetroceso(Vector3.forward);
            }
        }
        if (stats.GetVida() <= 0)
        {
            GeneraCum();
            Destroy(gameObject);
        }

    }

    void GeneraCum()
    {
        GameObject cloCum = Instantiate(sangrePrefab);
        cloCum.transform.position = transform.position;
        cloCum.transform.Rotate(cloCum.transform.rotation.x, cloCum.transform.rotation.y, Random.Range(0, 361));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            cooldown = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
