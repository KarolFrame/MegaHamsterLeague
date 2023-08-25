using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HamsterSuicida : MonoBehaviour
{
    enum EstadoExlposion {nada, explosion, destruccion };
    EstadoExlposion estado;
    Transform tr;
    NavMeshAgent agent;
    Rigidbody rB;
    StatsIA stats;
    public Animator anim;
    float tempoMuerte = 0.5f;

    Player player;

    Camara mainCam;

    Retroceso retrocesoPlayer;

    public GameObject cumSuicida;

    DestruirPoMalla malla;
    private void Awake()
    {
        mainCam = FindObjectOfType<Camara>();

        tr = transform;
        agent = GetComponent<NavMeshAgent>();
        rB = GetComponent<Rigidbody>();
        
        player = FindObjectOfType<Player>();
        retrocesoPlayer = player.GetComponent<Retroceso>();

        stats = GetComponent<StatsIA>();
        estado = EstadoExlposion.nada;

        stats.SetTipoIA(1);

        malla = GetComponent<DestruirPoMalla>();
    }

    // Update is called once per frame
    void Update()
    {

        if(estado == EstadoExlposion.nada)
        {
            if (agent.enabled)
            {
                agent.SetDestination(player.transform.position);
            }
            if (Vector3.Distance(tr.position, player.transform.position) <= 2.5f)
            {
                estado = EstadoExlposion.explosion;
            }
        }
        if(estado == EstadoExlposion.explosion)
        {
            anim.SetBool("explota", true);
            tempoMuerte -= Time.deltaTime;

            StartCoroutine(mainCam.Shake(0.5f, 1f));

            //RETROCESO JUGADOR
            retrocesoPlayer.ActivarRetroceso(Vector3.forward);
            RetrocesoEnemigos();

            if (tempoMuerte<=0)
            {
                estado = EstadoExlposion.destruccion;
            }
        }
        if(estado == EstadoExlposion.destruccion)
        {
            Explota();
            GeneraCum();
        }


        if (stats.GetVida() <= 0)
        {
            RetrocesoEnemigos();
            GeneraCum();
            Destroy(gameObject);
        }

    }
    void Explota()
    {
        if(player.GetTengoEskudo())
        {
            Destroy(gameObject, 0.2f);
            player.SetTengoEskudo(false);
        }
        else
        {
            player.SetVida(
            player.GetVida() - stats.GetDanoAtaque()
            );
            Destroy(gameObject);
        }

    }

    void GeneraCum()
    {
        GameObject cloCum = Instantiate(cumSuicida);
        cloCum.transform.position = tr.position;
        cloCum.transform.Rotate(cloCum.transform.rotation.x, cloCum.transform.rotation.y, Random.Range(0, 361));
    }

    void RetrocesoEnemigos()
    {
        float radio = 5;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radio);
        foreach (Collider nearby in colliders)
        {
            Retroceso retroceso = nearby.GetComponent<Retroceso>();
            if (retroceso != null)
            {
                retroceso.ActivarRetroceso(Vector3.forward);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Floor")
        {
            agent.enabled = true;
            rB.isKinematic = true;
            malla.enabled = true;
        }
    }

}
