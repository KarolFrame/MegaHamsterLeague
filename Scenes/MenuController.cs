using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    void Update()
    {
        if(Input.anyKeyDown)
        {
            LevelLoader.LoadLevel("Lobby");
        }
    }
    public void CambioDeEscena(string nombreEscena)
    {
        LevelLoader.LoadLevel(nombreEscena);
    }
    public void SalirDelJuego()
    {
        Application.Quit();
    }
}
