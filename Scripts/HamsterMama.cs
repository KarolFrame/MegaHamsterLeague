using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HamsterMama : MonoBehaviour
{
    //COMPONENTES
    Transform tr;
    public Transform expulsaBebes;

    public GameObject prefabBebe;
    float temporizador = 5;

    Player player;
    StatsIA stats;

    NavMeshAgent agent;

    public GameObject sangrePrefab;


    void Awake()
    {
        tr = transform;
        stats = GetComponent<StatsIA>();
        temporizador = 5;
        player = FindObjectOfType<Player>();
        
        stats.SetTipoIA(0);

        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (agent.enabled)
        {
            agent.SetDestination(player.transform.position);
        }

        temporizador -= Time.deltaTime;
        if(temporizador <= 0)
        {
            GenerarBebe();
            temporizador = 5;
        }
        if (stats.GetVida() <= 0)
        {
            GeneraCum();
            Destroy(gameObject);
        }

    }
    void GenerarBebe()
    {
        //COMPONENTES DEL SUICIDA
        GameObject hamsterBebe = Instantiate(prefabBebe, expulsaBebes.position, tr.transform.rotation);
        Rigidbody rbBebe = hamsterBebe.GetComponent<Rigidbody>();
        NavMeshAgent agentBebe = hamsterBebe.GetComponent<NavMeshAgent>();
        HamsterSuicida bebe = hamsterBebe.GetComponent<HamsterSuicida>();
        StatsIA statsBebe = hamsterBebe.GetComponent<StatsIA>();
        DestruirPoMalla malla = hamsterBebe.GetComponent<DestruirPoMalla>();
        //QUE LA PASA AL SUICIDA
        agentBebe.enabled = false;
        rbBebe.AddForce(tr.forward*10, ForceMode.Impulse);
        rbBebe.AddTorque(tr.right*360);
        malla.enabled = false;

    }
    void GeneraCum()
    {
        GameObject cloCum = Instantiate(sangrePrefab);
        cloCum.transform.position = tr.position;
        cloCum.transform.Rotate(cloCum.transform.rotation.x, cloCum.transform.rotation.y, Random.Range(0, 361));
    }

}
