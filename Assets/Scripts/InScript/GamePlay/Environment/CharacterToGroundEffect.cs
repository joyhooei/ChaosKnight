using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterToGroundEffect : MonoBehaviour {

	public GameObject ToGroundEffect;
	void Start () {
		
	}
	
	void OnCollisionEnter2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player") ) {
			var e = Instantiate (ToGroundEffect, cll.gameObject.transform.position, Quaternion.identity);
			var f = Instantiate (ToGroundEffect, cll.gameObject.transform.position, Quaternion.identity);
			//e.gameObject.transform.position = new Vector3 (e.gameObject.transform.position.x + 0.7f, e.transform.position.y - 0.8f, -3);
			//f.gameObject.transform.position = new Vector3 (f.gameObject.transform.position.x - 0.7f, f.transform.position.y - 0.8f, -3);
			if (cll.gameObject.GetComponent<mainController> ().useSkill) {
				e.gameObject.transform.localScale = new Vector3 (3, 3, 3);
				f.gameObject.transform.localScale = new Vector3 (3, 3, 3);
			} else {
				e.gameObject.transform.localScale = new Vector3 (1.5f, 1.5f, 2);
				f.gameObject.transform.localScale = new Vector3 (1.5f, 1.5f, 2);
			}
		}
	}
}
