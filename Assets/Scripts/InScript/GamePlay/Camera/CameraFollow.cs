using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	GameObject main;
	Transform PlayerFollow;
	public bool HorizontalFollow;
	public float DistanceX;
	public float StartLook;
	public float EndLook = 1280;
	public float SmoothTime;
	bool move = false, update = false;
	public bool nextPhase = false;
	public GameObject navigator;
	public bool endLevel = false;
	float index;
	CampingControl bg;
	Animator anim;

    AudioSource audios;

	void Start () {
		Invoke ("OnStart", 1f);
        audios = GetComponent<AudioSource>();
	}

	void OnStart(){
		main = GameObject.FindGameObjectWithTag ("Player");
		PlayerFollow = main.transform;
		anim = PlayerFollow.GetComponent<mainController> ().myAnim;
		update = true;
		bg = GameObject.Find ("Background").GetComponent<CampingControl> ();
	}

	void Update () {
		if (update && checkMain()) {
			index = main.GetComponent<mainController> ().useSkill ? 3 : 1;
			if (!nextPhase) {
				if (PlayerFollow.transform.position.x > StartLook + 10 && PlayerFollow.transform.position.x < EndLook) {
					move = true;
				} else
					move = false;
			} else {
				if (transform.position.x < PlayerFollow.transform.position.x -DistanceX) {
					transform.position = Vector3.MoveTowards (transform.position, new Vector3 (PlayerFollow.position.x + DistanceX, transform.position.y, transform.position.z),index * SmoothTime* Time.deltaTime);
					} else
					move = false;
			}
			if (StartLook < EndLook && !nextPhase) {
				if (transform.position.x < StartLook + 5)
					transform.position = Vector3.MoveTowards (transform.position, new Vector3 (StartLook + 5, transform.position.y, transform.position.z),index * SmoothTime* Time.deltaTime);
				if (transform.position.x > EndLook)
					transform.position = Vector3.MoveTowards (transform.position, new Vector3 (EndLook, transform.position.y, transform.position.z),index *SmoothTime* Time.deltaTime);
			}

			//if (transform.position.x >= 3960 * PublicClass.dpp - 5) {
			//	transform.position = Vector3.MoveTowards (transform.position, new Vector3 (3960 * PublicClass.dpp - 5, transform.position.y, transform.position.z),SmoothTime* Time.deltaTime);
			//}
		}

        audios.volume = PublicClass.music;
	}
	void FixedUpdate(){
		if (move) {
			if (HorizontalFollow) {
				if (!nextPhase) {
					if(anim.GetCurrentAnimatorStateInfo(0).IsName("McGetHit") || anim.GetCurrentAnimatorStateInfo(0).IsName("McIdle") || PlayerFollow.gameObject.GetComponent<mainController>().start)
						return;
					Vector3 PointGo = new Vector3 (PlayerFollow.position.x, transform.position.y, transform.position.z);
					float step = SmoothTime * Time.deltaTime * index;
					transform.position = Vector3.MoveTowards (transform.position, PointGo, step);
				}
			}
		}
		navigator.GetComponent<SpriteRenderer> ().enabled = nextPhase;
	}

	void LateUpdate(){
		if (!update || !checkMain () ||endLevel)
			return;
		if (GameObject.FindGameObjectsWithTag ("Enemy").Length <= 0 && bg.countPhase >= bg.inPhase) {
			nextPhase = true;
		} else
			nextPhase = false;
	}
	bool checkMain(){
		return GameObject.FindGameObjectWithTag ("Player");
	}
}
