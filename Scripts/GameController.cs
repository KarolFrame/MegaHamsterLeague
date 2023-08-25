using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    enum JugadorMuere { no, record, paraTiempo}
    JugadorMuere estadoJugador;
    public Player jugador;
    public TMPro.TextMeshProUGUI textoBalas;
    public TMPro.TextMeshProUGUI textoTorretas;

    public Image vida, estamina;

    public Camera camPlayer;

    web ranking;

    public GameObject eliminado;

    CursorManager cursorManager;

    private void Awake()
    {
        Camera.SetupCurrent(camPlayer);
        jugador = FindObjectOfType<Player>();
        ranking = FindObjectOfType<web>();
        estadoJugador = JugadorMuere.no;
        eliminado.SetActive(false);
        cursorManager = FindObjectOfType<CursorManager>();

        cursorManager.MouseMirilla();

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Vida del jugador
        textoBalas.text = "X " + jugador.GetBalas();
        textoTorretas.text = "X " + jugador.GetTorretas();
        vida.fillAmount = jugador.GetVida() / jugador.GetVidaMax();



        if (estadoJugador == JugadorMuere.no)
        {
            if (jugador.GetBalas() < 0)
                cursorManager.MouseMirillaSinVida();
            else
                cursorManager.MouseMirilla();
            if (jugador.GetVida() <= 0)
            {
                estadoJugador = JugadorMuere.record;
            }
        }
        if (estadoJugador == JugadorMuere.record)
        {
            cursorManager.BotonSeleccion();
            ranking.ProcesoInicialLectura();
            eliminado.SetActive(true);
            estadoJugador = JugadorMuere.paraTiempo;
        }
        if(estadoJugador == JugadorMuere.paraTiempo)
        {
            Time.timeScale = 0;
        }

    }
    public void eliminadoBorrar()
    {
        Destroy(eliminado);
    }
    public void CambiarEscena(string nombreEscena)
    {
        LevelLoader.LoadLevel(nombreEscena);
    }

}
