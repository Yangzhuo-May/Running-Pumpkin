using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicandSound : MonoBehaviour
{
    public PlayerControl player;
    public AudioSource bgmAudioSource;
    private bool hasReducedVolume = false;
    
    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = GetComponent<AudioSource>();
        bgmAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.isDead && !hasReducedVolume)
        {
            bgmAudioSource.volume *= 0.5f;
            hasReducedVolume = true;
        }
    }
}
