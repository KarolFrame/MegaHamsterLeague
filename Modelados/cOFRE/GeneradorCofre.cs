using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCofre : MonoBehaviour
{
    public GameObject[] prefabs;
    Transform tr;
    int numeroPrefabs = 0;
    float gradosCirculo = 360;
    float arco = 0;

    public GameObject mallaCerrado, mallaAbierto;
    bool cofreAbierto = false;

    //UI LIGADA
    public GameObject uI;

    void Awake()
    {
        tr = transform;
        if(uI != null)
        uI.SetActive(false);

        GestionarCofreMalla();
    }

    public void GenerarPrefabs()
    {
        numeroPrefabs = Random.Range(5,15);

        arco = (float)gradosCirculo / numeroPrefabs;

        GameObject prefab;

        for (int i = 0; i < numeroPrefabs; i++)
        {
            prefab = Instantiate(GenerarRandom(), tr.position, Quaternion.identity);
            prefab.transform.Rotate(Vector3.up, i * arco);
        }
    }
    GameObject GenerarRandom()
    {
        int random = Random.Range(0, prefabs.Length);
        return prefabs[random];
    }

    
    //GET
    public bool GetBoolAbierto()
    {
        return cofreAbierto;
    }
    
    
    //SET
    public void SetBoolAbierto()
    {
        cofreAbierto = true;
    }

    public void GestionarCofreMalla()
    {
        if(!cofreAbierto)
        {
            mallaCerrado.SetActive(true);
            mallaAbierto.SetActive(false);
        }
        else
        {
            mallaCerrado.SetActive(false);
            mallaAbierto.SetActive(true);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && uI != null)
            uI.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player" && uI != null)
            uI.SetActive(false);
    }
}



