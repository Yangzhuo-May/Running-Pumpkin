using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public PlayerController player;
    public AudioSource bgmAudioSource;
    private bool hasReducedVolume = false;
    
    void Start()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        bgmAudioSource.Play();
    }

    void Update()
    {
        if (player.isDead && !hasReducedVolume)
        {
            bgmAudioSource.volume *= 0.5f;
            hasReducedVolume = true;
        }
    }
}
