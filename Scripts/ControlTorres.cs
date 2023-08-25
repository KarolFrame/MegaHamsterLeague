using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlTorres : MonoBehaviour
{
    enum EstadoGenerador { generando, jugadorDesactivando, jugadorDesactiva, cuentaAtras, torreDesactivada, torreGana, escalaTiempo}
    EstadoGenerador estado;
    public GameObject UITorre;
    public Image torreActivaImage, torreDesactivandoseImage, torreFiled;
    public TMPro.TextMeshProUGUI tiempoRestanteTex;

    float cadaCuantoGenera= 5;
    float tempoEnArea = 40, maxTempoEnArea = 40;
    float tempoParaDesactivar = 120;
    GameObject torreActivaObject;
    public GameObject prefabTorre;

    int distancia = 25;

    Player jugador;

    public Transform[] spawnsTorres;

    public GameObject flecha;
    LineRenderer line;

    web ranking;

    SoundManager soundManager;

    public AudioSource torretaActivadaSonido, torreDesactivaSonido;


    private void Awake()
    {
        UITorre.SetActive(false);
        jugador = FindObjectOfType<Player>();
        estado = EstadoGenerador.cuentaAtras;
        ranking = FindObjectOfType<web>();
        line = flecha.GetComponent<LineRenderer>();
        soundManager = FindObjectOfType<SoundManager>();

        torretaActivadaSonido.enabled = false;
        torreDesactivaSonido.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        //ESTADOS
        if(estado == EstadoGenerador.cuentaAtras)
        {
            //Bajamos el tempo que dice cuando se genera la torre
            cadaCuantoGenera -= Time.deltaTime;

            //Si el tempo llega a 0 cambiamos de estado a generando
            if(cadaCuantoGenera<=0)
            {
                estado = EstadoGenerador.generando;
            }
        }

        if(estado == EstadoGenerador.generando)
        {
            //Generamos la torre
            GenerarTorre();

            //Activamos la Interfaz
            UITorre.SetActive(true);

            //Reset tempo en area
            tempoEnArea = maxTempoEnArea;

            //Sonido de activar torreta
            if (jugador.GetVida() > 0)
            {
                torretaActivadaSonido.enabled = true;
                torreDesactivaSonido.enabled = false;
            }


            // Cambiamos de estado
            estado = EstadoGenerador.jugadorDesactivando;
        }

        if(estado == EstadoGenerador.jugadorDesactivando)
        {
            //SI EL JUGADOR ESTA NE LE AREA DE LA TORRE
            if(Vector3.Distance(jugador.transform.position, torreActivaObject.transform.position)<= distancia && tempoParaDesactivar > 0)
            {
                //Baja el tiempo de area, ya que estás en el área
                tempoEnArea -= Time.deltaTime;

                //Representación de la interfaz
                torreActivaImage.enabled = false;
                torreDesactivandoseImage.enabled = true;
                torreFiled.enabled = true;


                torreFiled.fillAmount = tempoEnArea / maxTempoEnArea;

                //Jugador desactiva la torre
                if(tempoEnArea <= 0)
                {
                    estado = EstadoGenerador.torreDesactivada;
                }
                //Quitamos el sonido
                torretaActivadaSonido.enabled = false;
                torreDesactivaSonido.enabled = true;

            }
            //SI EL JUGADOR NO ESTA EN EL AREA DE LA TORRE
            if(Vector3.Distance(jugador.transform.position, torreActivaObject.transform.position) > distancia)
            {
                //baja el tiempo para poder desactivar
                tempoParaDesactivar -= Time.deltaTime;

                //Aumentar el timepo que debes estar en el area
                if (tempoEnArea < maxTempoEnArea)
                    tempoEnArea += Time.deltaTime;
                else
                    tempoEnArea = maxTempoEnArea;

                //Intefaz
                torreActivaImage.enabled = true;
                torreDesactivandoseImage.enabled = false;
                torreFiled.enabled = false;
                tiempoRestanteTex.text = "TIEMPO RESTANTE: " + Mathf.Round(tempoParaDesactivar);

                //Ponemos el sonido
                if(jugador.GetVida()>0)
                {
                    torretaActivadaSonido.enabled = true;
                    torreDesactivaSonido.enabled = false;
                }


                //El jugador pierde
                if (tempoParaDesactivar <= 0)
                {
                    estado = EstadoGenerador.torreGana;
                }

            }

        }

        if(estado == EstadoGenerador.torreDesactivada)
        {
            //Asignamos cuando se generará la siguiente torre
            cadaCuantoGenera = Random.Range(20,50);
            tempoParaDesactivar = 120;

            //Interfaz
            UITorre.SetActive(false);

            //Destruimos la torre y reseteamos la variable
            Destroy(torreActivaObject.gameObject);
            torreActivaObject = null;

            //Sonido de la otrreta desactivada
            soundManager.SeleccionAudio(17, 0.5f);

            //Quitamos el sonido
            torretaActivadaSonido.enabled = false;
            torreDesactivaSonido.enabled = false;

            //Cambio de estado a cuenta atras
            estado = EstadoGenerador.cuentaAtras;
        }

        if (estado == EstadoGenerador.torreGana)
        {
            //Quitamos el sonido
            torretaActivadaSonido.enabled = false;
            //WEB
            ranking.ProcesoInicialLectura();
            estado = EstadoGenerador.escalaTiempo;
        }
        if(estado == EstadoGenerador.escalaTiempo)
        {
            Time.timeScale = 0;
        }

        //RAYO MINIMAPA
        if (torreActivaObject != null && jugador.GetVida()>0)
        {

            line.enabled = true;
            line.SetPosition(0, flecha.transform.position);
            line.SetPosition(1, torreActivaObject.transform.position);

        }
        else
            line.enabled = false;


        /*//FLECHA MINIMAPA
        if (torreActivaObject != null)
        {
            flecha.SetActive(true);
            flecha.transform.LookAt(new Vector3(
                torreActivaObject.transform.position.x, flecha.transform.position.y, torreActivaObject.transform.position.z)
                );
        }
        else
            flecha.SetActive(false);*/
    }
    void GenerarTorre()
    {
        torreActivaObject = Instantiate(prefabTorre);
        torreActivaObject.transform.position = spawnsTorres[GetPosicionTorre()].position;
    }
    int GetPosicionTorre()
    {
        int random = Random.Range(0, spawnsTorres.Length);
        return random;
    }
}
