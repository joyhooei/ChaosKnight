using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FrameSkill{
	public int IDSkill;
	public Sprite FrameSkill1;
}

public class EnemyGetSkillControll : MonoBehaviour {
	SkillDb MCSkill = null;
	Animator myAnim;
	Rigidbody2D myRg;
	EnemyControll myPro;

	void Start () {
		myAnim = GetComponent<Animator> ();
		myRg = GetComponent<Rigidbody2D> ();
		myPro = GetComponent<EnemyControll> ();
	}

    void Update()
    {
        GetSkillToUp();
        if (myPro.m_fCurrentHP <= 0)
        {
            myAnim.speed = 1;
            indexStun = 0;
            indexSlow = 0;
        }
		if (MCSkill == null)
			return;
	}

	void OnTriggerEnter2D(Collider2D cll)
	{
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			if (MCSkill != null)
				return;
            mainSkillControll main = cll.gameObject.GetComponent<mainSkillControll>();
            if (main.indexSkillEnable == 0) return;
            if (main.EnableSkill != null)
            {
                MCSkill = main.EnableSkill;
                GetAtOne();
            }
		}
	} 

	void OnTriggerExit2D(Collider2D cll)
	{
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
            MCSkill = null;
            SetToDefault();
		}
	}

	void GetSkillToUp(){
        if (Time.time <= timeToStopEffect)
        {
            if (indexStun != 1)
            {
                myAnim.speed = 1 - indexSlow;
            }
            else
            {
                myAnim.speed = 0;
            }
        }
        else
        {
            myAnim.speed = 1;
            indexStun = 0;
            indexSlow = 0;
        }
	}

    float timeToStopEffect, indexSlow = 1, indexStun;

    void GetAtOne()
    {
        if (MCSkill == null || myPro == null)
        {
            return;
        }
        if(myPro.m_fCurrentHP <=0){
            return;
        }
        myPro.TakeDamage((int)MCSkill.DameSkill, null,true);
        timeToStopEffect = Time.time + MCSkill.Duration;
        if (MCSkill.TakeDown == 1)
            myAnim.SetBool("isFall", true);
        else
        {
            if(myPro)
                myPro.m_bGotHit = true;
        }
		if(MCSkill.Knock ==1)
			myRg.velocity = 
				new Vector2 (MCSkill.Knock * PublicClass.dpp * (transform.eulerAngles.y > 0 ? 1 : -1), 0);
		if (MCSkill.Fear == 1)
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y == 0 ? 180 : 0, 0);
        indexStun = MCSkill.Stun;
        indexSlow= MCSkill.Slow;
	}

    void SetToDefault()
    {
        myAnim.speed = 1;
    }
}
