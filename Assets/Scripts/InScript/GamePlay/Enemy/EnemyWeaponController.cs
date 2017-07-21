using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void OnTriggerEnter2D(Collider2D cll) {
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
			GetComponentInParent<EnemyControll> ().DealDamage (cll.gameObject);
		}
	}
}
