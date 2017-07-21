using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillControl : MonoBehaviour {

	public int m_nDamage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D cll) {
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
			cll.gameObject.GetComponent<mainController>().TakeDamage(m_nDamage, 0, gameObject, false, null);
		}
	}
}
