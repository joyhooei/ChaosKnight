using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class soundEnemyControll : MonoBehaviour {

	public AudioClip soundATK1, soundDeath, soundGetHit, soundStart;
	AudioSource myAudio;

	void Awake(){
		myAudio = GetComponent<AudioSource> ();
	}

    void LateUpdate()
    {
        myAudio.volume = PublicClass.sound;
    }

	void PlayAudioAtk1(){
		myAudio.PlayOneShot (soundATK1);
	}
	void PlayAudioDeath(){
		myAudio.PlayOneShot (soundDeath);
	}
	void PlayAudioGetHit(){
		myAudio.PlayOneShot (soundGetHit);
	}
	void PlayAudioStart(){
		myAudio.PlayOneShot (soundStart);
	}

}
