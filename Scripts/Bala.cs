using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    Transform tr;
    public float velocidad=4;
    float temporizador = 10;

    Player propietario;
    float dano;

    SoundManager soundManager;

    void Start()
    {
        tr = transform;
        soundManager = FindObjectOfType<SoundManager>();
        soundManager.SeleccionAudio(11, 0.5f);
        propietario = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        tr.Translate(Vector3.forward*velocidad*Time.deltaTime);
        temporizador -= Time.deltaTime;
        if (temporizador <= 0)
            Destroy(gameObject);
    }
    //SET
    public void SetDano(float ndano)
    {
        dano = ndano;
    }
    public float GetDano()
    {

        return dano;
    }
    private void OnTriggerEnter(Collider other)
    {
        StatsIA enemigo = other.GetComponent<StatsIA>();
        if( enemigo != null)
        {
            enemigo.SetVida(
                enemigo.GetVida()-dano
                );
            Retroceso retroceso = other.GetComponent<Retroceso>();
            if (retroceso != null)
                retroceso.ActivarRetroceso(Vector3.forward);
            Destroy(gameObject);
        }
        else
        {
            print("no da enemigo");
            Destroy(gameObject);
        }
            
    }
}
