using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyController : MonoBehaviour
{
   public GameObject controles;
    bool muestraControles = false;

    public GameObject redes;
    bool muestraRedes = false;

    private void Start()
    {
        controles.SetActive(false);
        redes.SetActive(false);
    }
    public void PanelControles()
    {
        if(!muestraControles)
        {
            redes.gameObject.SetActive(false);
            controles.gameObject.SetActive(true);
            muestraControles = true;
            muestraRedes = false;
        }
        else
        {
            redes.gameObject.SetActive(false);
            controles.gameObject.SetActive(false);
            muestraControles = false;
            muestraRedes = false;
        }

    }
    public void PanelRedes()
    {
        if (!muestraRedes)
        {
            redes.gameObject.SetActive(true);
            controles.gameObject.SetActive(false);
            muestraRedes = true;
            muestraControles = false;
        }
        else
        {
            redes.gameObject.SetActive(false);
            controles.gameObject.SetActive(false);
            muestraRedes = false;
            muestraControles = false;
        }

    }
    public void Enlace(string link)
    {
        Application.OpenURL(link);
    }
    public void CambioDeEscena(string nombreEscena)
    {
        LevelLoader.LoadLevel(nombreEscena);
    }
}
