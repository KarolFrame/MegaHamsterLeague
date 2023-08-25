using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlOleadas : MonoBehaviour
{
    enum EstadoJuego {cuentaAtras, generando, enOleada, subirStatsEnemigos}
    EstadoJuego estadoJuego = EstadoJuego.cuentaAtras;

    public GameObject[] prefabEnemigo;
    /*
     TIPOS DE IA
        -Mama = 0
        -Sucida = 1
        -Distancia = 2
        -MeleeDano = 3
        -MeleeVida = 4
     */
    public Transform[] spawns;
    Transform posicionSpawn;
    int numeroEnemigosEnJuego;
    int enemigosGenerar;
    int numeroDeOleada = 1;
    float timer = 10;

    Player jugador;
    public Text textOleadas, textEnemigosRestantes;

    //COFRES
    GeneradorCofre[] cofres;
    Transform[] posicionCofres;
    public GameObject prefabCofre;

    SoundManager soundManager;
    void Awake()
    {
        jugador = FindObjectOfType<Player>();
        enemigosGenerar = 6;
        soundManager = FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //CUENTA ATRAS PARA LA SIGUIENTE OLEADA
        if(estadoJuego == EstadoJuego.cuentaAtras)
        {
            //ACCIONES
            timer -= Time.deltaTime;
            textOleadas.text = "COMIENZA EN " + Mathf.Round(timer);
            textEnemigosRestantes.text = " ";
            //CAMBIO ESTADO
            if (timer <= 0)
                estadoJuego = EstadoJuego.generando;

        }
        //GENERA LOS ENEMIGOS Y COFRES
        if (estadoJuego == EstadoJuego.generando)
        {
            //ACCIONES
            GeneradorEnemigos(enemigosGenerar);
            textOleadas.text = "OLEADA " + numeroDeOleada;
            //sonido empieza oleada
            soundManager.SeleccionAudio(16, 0.5f);

            //CAMBIO ESTADO
            estadoJuego = EstadoJuego.subirStatsEnemigos;

        }
        //SUBIR LAS STATS DE LOS ENEMIGOS
        if(estadoJuego == EstadoJuego.subirStatsEnemigos)
        {
            //ACCIONES
            SubirStasEnemigos();
            //CAMBIO ESTADO
            estadoJuego = EstadoJuego.enOleada;
        }
        //EL JUGADOR ESTA EN OLEADA
        if (estadoJuego == EstadoJuego.enOleada)
        {
            //ACCIONES
            numeroEnemigosEnJuego = FindObjectsOfType<StatsIA>().Length;
            textEnemigosRestantes.text = "ENEMIGOS RESTANTES: " + numeroEnemigosEnJuego;

            //CAMBIO ESTADO
            if (numeroEnemigosEnJuego == 0)
            {
                //Subir las stats del jugador
                SubirStatsPlayer();

                //Cosa
                numeroDeOleada++;
                enemigosGenerar++;
                timer = 10;
                estadoJuego = EstadoJuego.cuentaAtras;
            }
        }

    }
    void SubirStatsPlayer()
    {
        //LO QUE LE SUBE AL JUGADOR CADA VEZ QUE ACABA UNA RONDA
        jugador.SetDano(
            jugador.GetDano() * (1.1f*numeroDeOleada)
            );
    }
    void SubirStasEnemigos()
    {
        //LO QUE LE SUBE A CADA ENEMIGO DE BASE
        StatsIA[] arrayEnemigos = FindObjectsOfType<StatsIA>();
        for (int i = 0; i < arrayEnemigos.Length; i++)
        {
            arrayEnemigos[i].SetVida(
                arrayEnemigos[i].GetVida() * (1.1f * numeroDeOleada)
                );
        }
    }
    void GeneradorEnemigos(int enemigosgenerar)
    {
        for (int i = 0; i < enemigosgenerar; i++)
        {
            /*if (i % 10 == 0)
                Instantiate(prefabMama, GetPosicionJugador().position, prefabMama.transform.rotation);
            else*/
                Instantiate(prefabEnemigo[GetEnemigoQueSeGenera()], GetPosicionJugador().position, prefabEnemigo[2].transform.rotation);
        }
    }

    int GetEnemigoQueSeGenera()
    {
        int random = Random.Range(0, prefabEnemigo.Length);
        return random;
    }
    Transform GetPosicionJugador()
    {
        int random = Random.Range(0, spawns.Length);
        posicionSpawn = spawns[random];
        return posicionSpawn;
    }
    //GET
    public int GetOleada()
    {
        return numeroDeOleada;
    }
}
