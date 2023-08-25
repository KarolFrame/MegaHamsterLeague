using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioClip[] audios;

    AudioSource controlAudio;


    private void Awake()
    {
        controlAudio = GetComponent<AudioSource>();
    }
    public void SeleccionAudio(int i, float volume)
    {
        controlAudio.PlayOneShot(audios[i], volume);
    }
    public void PulsarBoton()
    {
        SeleccionAudio(0, 0.5f);
    }
}
