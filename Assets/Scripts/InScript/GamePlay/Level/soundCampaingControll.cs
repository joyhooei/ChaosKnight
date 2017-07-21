using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class soundCampaingControll : MonoBehaviour {

	public AudioClip soundPhaseStart;
	AudioSource myAudio;

	void Awake () {
		myAudio = GetComponent<AudioSource> ();
	}

    void LateUpdate()
    {
        myAudio.volume = PublicClass.music;
    }

	public void PlayAudioPhaseStart(){
		myAudio.PlayOneShot (soundPhaseStart);
	}
}
