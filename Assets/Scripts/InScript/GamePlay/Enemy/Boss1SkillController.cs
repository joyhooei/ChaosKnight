using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1SkillController : MonoBehaviour {

	public GameObject m_goEffectSkill1; // object to display effect on monster's attack 1
	public GameObject m_goEffectSkill2; // object to display effect on monster's attack 2
	public GameObject m_goEffectSkill3; // object to display effect on monster's attack 3

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void ShowEffectOnAttack1() {
		if (m_goEffectSkill1) {
			Vector3 _v3SkillEffectPosition = transform.Find ("Attack1Effect").transform.position;
			GameObject _goSkillEffect = Instantiate (m_goEffectSkill1, _v3SkillEffectPosition, Quaternion.identity);
			_goSkillEffect.GetComponent<EnemySkillControl> ().m_nDamage = (int)gameObject.GetComponent<EnemyControll>().m_oSelectedAttack.Damage;
			_goSkillEffect.transform.eulerAngles = new Vector3 (0, gameObject.GetComponent<EnemyControll>().m_nDirection < 0 ? 180 : 0, 0);
			Destroy (_goSkillEffect, 2.0f);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void ShowEffectOnAttack2() {
		if (m_goEffectSkill2) {
			Vector3 _v3SkillEffectPosition = transform.Find ("Attack2Effect").transform.position;
			GameObject _goSkillEffect = Instantiate (m_goEffectSkill2, _v3SkillEffectPosition, Quaternion.identity);
			_goSkillEffect.GetComponent<EnemySkillControl> ().m_nDamage = (int)gameObject.GetComponent<EnemyControll> ().m_oSelectedAttack.Damage;
			_goSkillEffect.transform.localScale = new Vector3 (1.6f, 1.6f, 1.6f);
			_goSkillEffect.transform.eulerAngles = new Vector3 (0, gameObject.GetComponent<EnemyControll> ().m_nDirection < 0 ? 180 : 0, 0);
			Destroy (_goSkillEffect, 2.0f);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void ShowEffectOnAttack3() {
		if (m_goEffectSkill3) {
			Vector3 _v3SkillEffectPosition = transform.Find ("Attack3Effect1").transform.position;
			GameObject _goSkillEffect = Instantiate (m_goEffectSkill3, _v3SkillEffectPosition, Quaternion.identity);
			_goSkillEffect.GetComponent<EnemySkillControl> ().m_nDamage = (int)gameObject.GetComponent<EnemyControll> ().m_oSelectedAttack.Damage;
			_goSkillEffect.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			_goSkillEffect.transform.eulerAngles = new Vector3 (0, gameObject.GetComponent<EnemyControll> ().m_nDirection < 0 ? 180 : 0, 0);
			Destroy (_goSkillEffect, 2.0f);

			Vector3 _v3SkillEffectPosition2 = transform.Find ("Attack3Effect2").transform.position;
			GameObject _goSkillEffect2 = Instantiate (m_goEffectSkill3, _v3SkillEffectPosition2, Quaternion.identity);
			_goSkillEffect2.GetComponent<EnemySkillControl> ().m_nDamage = (int)gameObject.GetComponent<EnemyControll> ().m_oSelectedAttack.Damage;
			_goSkillEffect2.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			_goSkillEffect2.transform.eulerAngles = new Vector3 (0, gameObject.GetComponent<EnemyControll> ().m_nDirection < 0 ? 0 : 180, 0);
			Destroy (_goSkillEffect2, 2.0f);
		}
	}
}
