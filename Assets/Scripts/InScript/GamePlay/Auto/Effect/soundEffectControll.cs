using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundEffectControll : MonoBehaviour {

    AudioSource audios;
    void Start()
    {
        audios = Camera.main.GetComponent<AudioSource>();
    }
    void PlaySound(string Path)
    {
        AudioClip clip = (AudioClip)Resources.Load<AudioClip>(Path);
        audios.PlayOneShot(clip);
    }
}
