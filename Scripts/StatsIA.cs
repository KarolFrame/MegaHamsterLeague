using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsIA : MonoBehaviour
{
    int tipoIA;

    /*
     TIPOS DE IA
        -Mama = 0
        -Sucida = 1
        -Distancia = 2
        -MeleeDano = 3
        -MeleeVida = 4
     */

    float danoAtaque, vida, velocidad;
    private void Awake()
    {

    }

    private void Start()
    {
        //MAMA
        if(tipoIA == 0)
        {
            vida = 500;
        }
        //SUICIDA
        if (tipoIA == 1)
        {
            vida = 100;
            danoAtaque = 20;
        }
        //DISTANCIA
        if (tipoIA == 2)
        {
            vida = 100;
            danoAtaque = 10;
        }
        //MELEEDANO
        if (tipoIA == 3)
        {
            vida = 50;
            danoAtaque = 20;
        }
        //MELEEVIDA
        if (tipoIA == 4)
        {
            vida = 100;
            danoAtaque = 10;
        }
    }

    //GET
    public float GetVida()
    {
        return vida;
    }
    public float GetDanoAtaque()
    {
        return danoAtaque;
    }
    public float GetVelocidad()
    {
        return velocidad;
    }

    //SET
    public void SetVida(float nuevaVida)
    {
        vida = nuevaVida;
    }
    public void SetDanoAtaque(float nuevoDano)
    {
        vida = nuevoDano;
    }
    public void SetVelocidad(float nuevaVelocidad)
    {
        vida = nuevaVelocidad;
    }
    public void SetTipoIA(int tipo)
    {
        tipoIA = tipo;
    }
}
