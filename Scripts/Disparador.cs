using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparador : MonoBehaviour
{
    float temporizador = 5;
    float cambioDeSentido = 3;
    bool vaDerecha = true;
    public Transform spawnBala, jugador;
    Transform tr;
    public GameObject prefabBala;
    public float velocidad = 3;

    float vidaEnemigo = 100;
    float danoQueHaceElJugador = 0;
    void Start()
    {
        tr = transform;
        temporizador = Random.Range(1, 5);
        cambioDeSentido = Random.Range(1, 5);
    }

    // Update is called once per frame
    void Update()
    {
        cambioDeSentido -= Time.deltaTime;
        if(cambioDeSentido <= 0)
		{
            vaDerecha = !vaDerecha;
            cambioDeSentido = Random.Range(3, 5);
        }
        
        if(vaDerecha)
		{
            tr.Translate(Vector3.right * velocidad * Time.deltaTime);
        }
        else
		{
            tr.Translate(-Vector3.right * velocidad * Time.deltaTime);
        }
        
        tr.LookAt(jugador);
        temporizador -= Time.deltaTime;
        if(temporizador <= 0)
		{
            GameObject clonBala = Instantiate(prefabBala);
            clonBala.transform.position = spawnBala.position;
            clonBala.transform.rotation = tr.rotation;
            temporizador = Random.Range(3, 5);
        }
        if(vidaEnemigo<=0)
		{
            Destroy(gameObject);
		}
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == ("Bala"))
        {
            danoQueHaceElJugador = Random.Range(20, 30);
            vidaEnemigo -= danoQueHaceElJugador;
        }
        Destroy(collider.gameObject);
    }
}
