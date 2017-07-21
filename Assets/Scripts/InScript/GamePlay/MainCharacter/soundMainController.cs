using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]

[System.Serializable]
public class SoundATK{
	public AudioClip StepA, StepB,StepC,StepD;
}
[System.Serializable]
public class SoundSkill{
    public AudioClip Skill1, Skill2, Skill3, Skill4;
}
[System.Serializable]
public class SoundATKs{
    public SoundATK soundATKAir, soundATKGround;
    public SoundSkill soundSkill;
	public AudioClip soundDDeath,soundDGetHit,soundDJump,soundStart, soundDRun;
}

public class soundMainController : MonoBehaviour {
	public SoundATKs mySound;
	AudioSource myAudio;
	void Start () {
		myAudio = GetComponent<AudioSource> ();
	}

    void FixedUpdate()
    {
        myAudio.volume = PublicClass.sound;
    }

	void PlayATKAirStepA(){
		myAudio.PlayOneShot (mySound.soundATKAir.StepA);
	}
	void PlayATKAirStepB(){
		myAudio.PlayOneShot (mySound.soundATKAir.StepB);
	}
	void PlayATKAirStepC(){
		myAudio.PlayOneShot (mySound.soundATKAir.StepC);
	}
	void PlayATKAirStepD(){
		myAudio.PlayOneShot (mySound.soundATKAir.StepD);
	}
	void PlayATKGroundStepA(){
		myAudio.PlayOneShot (mySound.soundATKGround.StepA);
	}
	void PlayATKGroundStepB(){
		myAudio.PlayOneShot (mySound.soundATKGround.StepB);
	}
	void PlayATKGroundStepC(){
		myAudio.PlayOneShot (mySound.soundATKGround.StepC);
	}
	void PlayATKGroundStepD(){
		myAudio.PlayOneShot (mySound.soundATKGround.StepD);
	}
	void PlayDDeath(){
		myAudio.PlayOneShot (mySound.soundDDeath);
	}
	void PlayDGetHit(){
		myAudio.PlayOneShot (mySound.soundDGetHit);
	}
	void PlayDJump(){
		myAudio.PlayOneShot (mySound.soundDJump);
	}
	void PlayDStart(){
		myAudio.PlayOneShot (mySound.soundStart);
	}
    void PlayDRun()
    {
        myAudio.PlayOneShot(mySound.soundDRun);
    }


    void PlaySkill1()
    {
        myAudio.PlayOneShot(mySound.soundSkill.Skill1);
    }
    void PlaySkill2()
    {
        myAudio.PlayOneShot(mySound.soundSkill.Skill2);
    }
    void PlaySkill3()
    {
        myAudio.PlayOneShot(mySound.soundSkill.Skill3);
    }
    void PlaySkill4()
    {
        myAudio.PlayOneShot(mySound.soundSkill.Skill4);
    }

	void PlayerOtherSound(string SoundName){
		if (SoundName.Contains ("Idle")) {
			int a = Random.Range (0, 2);
			if (a == 0)
				return;
		}
		AudioClip sound = Resources.Load ("Sounds/Player/War/" + SoundName) as AudioClip;
		myAudio.PlayOneShot (sound);
	}
}
