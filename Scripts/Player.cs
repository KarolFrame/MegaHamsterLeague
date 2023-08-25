using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public GameObject mallaLanzaKikos;
    public GameObject eskudo, explosicon;
    bool tengoEskudo = false;
    public GameObject metralleta;

    public GameObject prefabMuro;

    Transform tr;
    public Rigidbody rb;
    NavMeshAgent agent;
    public float moveSpeed = 5f;
    Vector3 movement;
    public Camera cam;

    public GameObject prefabBala;
    public Transform spawnBala;

    public Animator animLanzaKikos;

    public float vida = 100, vidaMax = 100;
    float dano = 30;
    public float balas = 50, estamina = 100, cargaUlti = 0;
    public int torretas = 0;

    bool puedeDisparar = true;
    public float cooldown = 2;
    bool activarCooldown = false;

    public TextMesh nombreJugador;

    Bala bala;

    bool fueraDelArea;

    bool envenenado;
    bool activarDesenvenenamiento;
    float tiempoenvenenado;

    SoundManager soundManager;

    Camara mainCam;

    public GameObject fogueoPrefab;

    //PARTICULAS
    public ParticleSystem polvoPies;
    ParticleSystem.EmissionModule emisionPolvoPies;

    void Awake()
    {
        mainCam = FindObjectOfType<Camara>();

        rb.GetComponent<Rigidbody>();
        fueraDelArea = false;
        if (tr == null)
            tr = transform;
        else
            nombreJugador.text = "";
        eskudo.SetActive(false);

        agent = GetComponent<NavMeshAgent>();

        envenenado = false;
        activarDesenvenenamiento = false;

        soundManager = FindObjectOfType<SoundManager>();
        vida = vidaMax;
        dano = 30;


        balas = 50;

        emisionPolvoPies = polvoPies.emission;
    }
    void Update()
    {
        if (Time.timeScale == 1)
        {
            //MOVIMIENTO JUGADOR
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.z = Input.GetAxisRaw("Vertical");
            movement.y = 0.38f;
            if(agent.enabled)
            agent.destination = tr.position + movement;

            CheckPolvoPies();

            //JUGADOR GIRE CON RATON
            Plane playerPLane = new Plane(Vector3.up, tr.position);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
            float hitDist = 0.0f;

            //ESKUDO
            if (tengoEskudo)
                eskudo.SetActive(true);
            else
                eskudo.SetActive(false);

            if (playerPLane.Raycast(ray, out hitDist))
            {

                Vector3 targetPointKiko = ray.GetPoint(hitDist);
                Quaternion targetRotationKiko = Quaternion.LookRotation(targetPointKiko - metralleta.transform.position);
                targetRotationKiko.x = 0;
                targetRotationKiko.z = 0;

                metralleta.transform.rotation = Quaternion.Slerp(metralleta.transform.rotation, targetRotationKiko, 7f * Time.deltaTime);
            }

            //

            if (activarCooldown)
            {
                cooldown -= Time.deltaTime;
                puedeDisparar = false;
            }
                
            if (cooldown <=0)
            {
                activarCooldown = false;
                puedeDisparar = true;
            }



            if (Input.GetButtonDown("Ataque1") && puedeDisparar)
            {
                if (balas >= 1)
                {

                    GameObject clonFogueo = Instantiate(fogueoPrefab);

                    clonFogueo.transform.position = spawnBala.position;
                    clonFogueo.transform.rotation = metralleta.transform.rotation;


                    GameObject clonBala = Instantiate(prefabBala);

                    clonBala.transform.position = spawnBala.position;
                    clonBala.transform.rotation = metralleta.transform.rotation;

                    Bala bala = clonBala.GetComponent<Bala>();
                    bala.SetDano(dano);

                    animLanzaKikos.SetTrigger("Disparo");

                    soundManager.SeleccionAudio(4, 0.8f);

                    //StartCoroutine(mainCam.Shake(0.05f, 0.2f));
                    
                    balas--;
                    activarCooldown = true;
                    cooldown = 0.1f;
                    puedeDisparar = false;
                }

                //ENVENENAMIENTO
                if (envenenado)
                    vida -= 5 * Time.deltaTime;
                if (activarDesenvenenamiento)
                {
                    tiempoenvenenado -= Time.deltaTime;
                    if (tiempoenvenenado <= 0)
                    {
                        envenenado = false;
                        activarDesenvenenamiento = false;
                    }
                }
            }


            if (vida <= 0)
            {
                Destroy(gameObject);
                Application.Quit();
            }

            if (fueraDelArea)
                vida -= 5 * Time.deltaTime;
        }
    }

   
    //COLISIONES
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Eskudo")
        {
            soundManager.SeleccionAudio(8, 0.5f);

            tengoEskudo = true;
            Destroy(other.gameObject);
        }
        if (tengoEskudo)
        {
            if (other.gameObject.tag == ("Bala"))
            {
                tengoEskudo = false;
            }
        }
        if (other.gameObject.tag == ("Manzana"))
        {
            soundManager.SeleccionAudio(10, 0.5f);

            vida += Random.Range(5, 10);
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == ("Kiko"))
        {
            soundManager.SeleccionAudio(9, 0.5f);

            balas += 20;
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag== "Torreta")
        {
            soundManager.SeleccionAudio(9, 0.5f);

            torretas++;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "AreaMuerte")
        {
            fueraDelArea = false;
        }

        if(other.gameObject.tag == "Muro")
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

    private void CheckPolvoPies()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            emisionPolvoPies.rateOverTime = 25;
        else
            emisionPolvoPies.rateOverTime = 0;
    }


    //GET
    public float GetVida()
    { 
        return vida;
    }
    public float GetVidaMax()
    {
        return vidaMax;
    }
    public float GetDano()
    {
        return dano;
    }
    public float GetVelocidad()
    {
        return moveSpeed;
    }
    public float GetBalas()
    {
        return balas;
    }
    public bool GetTengoEskudo()
    {
        return tengoEskudo;
    }
    public int GetTorretas()
    {
        return torretas;
    }

    //SET
    public void SetVida(float nuevaVida)
    {
        vida = nuevaVida;
    }
    public void SetDano(float danoNuevo)
    {
        dano = danoNuevo;
    }
    public void SetVidaMax(float nuevaVidaMax)
    {
        vidaMax = nuevaVidaMax;
    }
    public void SetVelocidad(float nuevaVelocidad)
    {
        moveSpeed = nuevaVelocidad;
    }
    public void SetBalas(float nuevasBalas)
    {
        balas = nuevasBalas;
    }
    public void SetCargaUlti(float suma)
    {
        cargaUlti = cargaUlti + suma;
    }
    public void SetTengoEskudo(bool eskudo)
    {
        tengoEskudo = eskudo;
    }
    public void SetTorretas(int i)
    {
        torretas = i;
    }
}
