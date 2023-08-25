using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introducir : MonoBehaviour
{
    public TextMesh nombreDelJugador;

    public Font fuente;
    string nombrePlayer = "";

    bool juegoEnPausa = true;

    SoundManager soundManager;

    private void Start()
    {
        juegoEnPausa = true;
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        if (juegoEnPausa)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    private void OnGUI()
    {
        if(Time.timeScale == 0)
        {
            GUI.skin.font = fuente;
            GUI.contentColor = Color.white;
            GUI.skin.label.fontSize = 60;
            GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 60;
            nombrePlayer = GUI.TextField(new Rect(700, 500, 600, 100), nombrePlayer, 10);
            GUI.skin.label.fontSize = GUI.skin.box.fontSize = GUI.skin.button.fontSize = 60;

            GUI.contentColor = Color.white;

            if (GUI.Button(new Rect(900, 700, 200, 80), "ACEPTAR"))
            {
                soundManager.SeleccionAudio(0, 0.5f);

                PlayerPrefs.SetString("nombrePlayer", nombrePlayer);
                nombreDelJugador.text = nombrePlayer;
                juegoEnPausa = false;
            }
        }

    }
}
