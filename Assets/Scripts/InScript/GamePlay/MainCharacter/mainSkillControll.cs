using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MCSkill{
	public int IDSkill;
	public RuntimeAnimatorController Skill;
	[HideInInspector]
	public SkillDb DBSkill;
}

[RequireComponent(typeof(skill))]
public class mainSkillControll : MonoBehaviour {
    public SkillDb EnableSkill;
	public MCSkill[] MCSkills;
	Button bSkill1,bSkill2,bSkill3,bSkill4;
	Image tSkill1,tSkill2,tSkill3,tSkill4;
	mainController mainControll;
	public RuntimeAnimatorController LocalSkill;
	//
	float cSkill1,cSkill2,cSkill3,cSkill4;
	//
	bool Skill4Enable = false, Skill1Enable = false,Skill2Enable = false,Skill3Enable = false;
	float maxSkill1, maxSkill2, maxSkill3, maxSkill4;
    [HideInInspector]
	public int indexSkillEnable = 0;
	void Start () {
		skill Skills;
        GetComponent<skill>().Load();
		Skills = GetComponent<skill> ();
		mainControll = GetComponent<mainController> ();
		int myId = mainControll.myMC.IdMC;
		//
		foreach (var i in MCSkills) {
			foreach (var j in Skills.Skill.SkillList) {
				if (i.IDSkill == j.IdSkill) {
					i.DBSkill = j;
					break;
				}
			}
		}

		//	Set Button
		bSkill1 = GameObject.Find ("Skill1").GetComponent<Button>();
		bSkill1.onClick.AddListener (Skill1Onclick);
		tSkill1 = GameObject.Find ("ISkill1").GetComponent<Image> ();
		cSkill1 = MCSkills [0].DBSkill.CoolDown;
		maxSkill1 = cSkill1;


        bSkill2 = GameObject.Find("Skill2").GetComponent<Button>();
        bSkill2.onClick.AddListener(Skill2Onclick);
        tSkill2 = GameObject.Find("ISkill2").GetComponent<Image>();
        cSkill2 = MCSkills[1].DBSkill.CoolDown;
        maxSkill2 = cSkill2;

        bSkill3 = GameObject.Find("Skill3").GetComponent<Button>();
        bSkill3.onClick.AddListener(Skill3Onclick);
        tSkill3 = GameObject.Find("ISkill3").GetComponent<Image>();
        cSkill3 = MCSkills[2].DBSkill.CoolDown;
        maxSkill3 = cSkill3;

		bSkill4 = GameObject.Find ("Skill4").GetComponent<Button>();
		bSkill4.onClick.AddListener (Skill4Onclick);
		tSkill4 = GameObject.Find("ISkill4").GetComponent<Image>();
		cSkill4 =  MCSkills [3].DBSkill.CoolDown;
		maxSkill4 = cSkill4;
	}

	void Update(){
		Check (indexSkillEnable);
		//Skill1
		if (cSkill1 > 0) {
			cSkill1 -= 1 * Time.deltaTime;
			if (indexSkillEnable != 1)
                bSkill1.interactable = false;
		} else {
			if (!Skill1Enable) {
				if (indexSkillEnable == 0) {
					bSkill1.interactable = checkMP(MCSkills[0].DBSkill);
				}
			}
			else {
				cSkill1 = MCSkills[0].DBSkill.CoolDown;
				maxSkill1 = MCSkills [0].DBSkill.CoolDown;
				Skill1Enable = false;
				SetEffectToDefault (MCSkills [0].DBSkill);
			}
		}
		tSkill1.fillAmount = cSkill1 / maxSkill1;

        //Skill2

        if (cSkill2 > 0)
        {
            cSkill2 -= 1 * Time.deltaTime;
            if (indexSkillEnable != 2)
                bSkill2.interactable = false;
        }
        else
        {
            if (!Skill2Enable)
            {
                if (indexSkillEnable == 0)
                    bSkill2.interactable = checkMP(MCSkills[1].DBSkill);
            }
            else
            {
                cSkill2 = MCSkills[1].DBSkill.CoolDown;
                SetEffectToDefault(MCSkills[1].DBSkill);
                Skill2Enable = false;
                maxSkill2 = MCSkills[1].DBSkill.CoolDown;
            }
        }
        tSkill2.fillAmount = cSkill2 / maxSkill2;

        //Skill3

        if (cSkill3 > 0)
        {
            cSkill3 -= 1 * Time.deltaTime;
            if (indexSkillEnable != 3)
                bSkill3.interactable = false;
        }
        else
        {
            if (!Skill3Enable)
            {
                if (indexSkillEnable == 0)
                    bSkill3.interactable = checkMP(MCSkills[2].DBSkill);
            }
            else
            {
                cSkill3 = MCSkills[2].DBSkill.CoolDown;
                SetEffectToDefault(MCSkills[2].DBSkill);
                Skill3Enable = false;
                maxSkill3 = MCSkills[2].DBSkill.CoolDown;
            }
        }
        tSkill3.fillAmount = cSkill3 / maxSkill3;

		//Skill4
		if (cSkill4 > 0) {
			cSkill4 -= 1 * Time.deltaTime;
			if (indexSkillEnable != 4)
				bSkill4.interactable = false;
			//else 
				//bSkill4.animationTriggers = true;
		} else {
			if (!Skill4Enable) {
				if(indexSkillEnable == 0)
                    bSkill4.interactable = checkMP(MCSkills[3].DBSkill);
			}
			else {
				cSkill4 = MCSkills [3].DBSkill.CoolDown;
				SetEffectToDefault ( MCSkills [3].DBSkill);
				Skill4Enable = false;
				maxSkill4 = MCSkills [3].DBSkill.CoolDown;
			}
		}
		tSkill4.fillAmount = cSkill4 / maxSkill4;
	}

    void LateUpdate()
    {
        if (mainControll.myAnim.runtimeAnimatorController.name.Equals(LocalSkill.name))
        {
            indexSkillEnable = 0;
        }
    }

	void Skill1Onclick(){
        if (indexSkillEnable == 0)
        {
            mainControll.myAnim.runtimeAnimatorController = MCSkills[0].Skill;
            cSkill1 = MCSkills[0].DBSkill.Duration;
            maxSkill1 = cSkill1;
            indexSkillEnable = 1;
            Skill1Enable = true;
            GetSkillEffect(MCSkills[0].DBSkill);
        }
	}


	void Skill2Onclick(){
        if (indexSkillEnable == 0)
        {
            mainControll.myAnim.runtimeAnimatorController = MCSkills[1].Skill;
            cSkill2 = MCSkills[1].DBSkill.Duration;
            maxSkill2 = cSkill2;
            indexSkillEnable = 2;
            Skill2Enable = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 2);
            GetSkillEffect(MCSkills[1].DBSkill);
        }
	}

	void Skill3Onclick(){
        if (indexSkillEnable == 0)
        {
            mainControll.myAnim.runtimeAnimatorController = MCSkills[2].Skill;
            cSkill3 = MCSkills[2].DBSkill.Duration;
            maxSkill3 = cSkill3;
            indexSkillEnable = 3;
            mainControll.myAnim.SetBool("start", true);
            Skill3Enable = true;
            GetSkillEffect(MCSkills[2].DBSkill);
        }
	}

	void Skill4Onclick(){
        if (indexSkillEnable == 0)
        {
            mainControll.myAnim.runtimeAnimatorController = MCSkills[3].Skill;
            cSkill4 = MCSkills[3].DBSkill.Duration;
            maxSkill4 = cSkill4;
            indexSkillEnable = 4;
            mainControll.myAnim.SetBool("start", true);
            Skill4Enable = true;
            GetSkillEffect(MCSkills[3].DBSkill);
        }
	}

	void GetSkillEffect(SkillDb Skill){
        mainControll.attack = 0;
        mainControll.myMC.MpDefault -= Skill.MPCost;
		mainControll.moveSpeed *= (1 + Skill.SpeedUp);
		mainControll.myMC.HpDefault = (int)(mainControll.myMC.HpDefault * (1 + Skill.HpRegen));
		mainControll.myMC.DodgeDefault *= 1 + Skill.DodgeUp;
		mainControll.myMC.AMDefault = (int)(mainControll.myMC.AMDefault * (1 + Skill.AMUp));
        mainControll.damgeAttack = (int)(mainControll.damgeAttack * (1 + Skill.DameUp));
		mainControll.useSkill = true;
        EnableSkill = Skill;
	}

	void SetEffectToDefault(SkillDb Skill){
		mainControll.moveSpeed /= (1+ Skill.SpeedUp);
		mainControll.myMC.DodgeDefault /= 1 + Skill.DodgeUp;
        mainControll.myMC.AMDefault = (int)(mainControll.myMC.AMDefault / (1 + Skill.AMUp));
        mainControll.damgeAttack = (int)(mainControll.damgeAttack / (1 + Skill.DameUp));
		mainControll.useSkill = false;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        mainControll.attack = 0;
        if (indexSkillEnable == 4)
        {
            mainControll.myAnim.runtimeAnimatorController = LocalSkill;
            indexSkillEnable = 0;
            EnableSkill = null;
        } 
	}

	void SetEffectToDefault1(){
		mainControll.useSkill = false;
		mainControll.myAnim.runtimeAnimatorController = LocalSkill;
        mainControll.myAnim.Play("Idle");
        mainControll.attack = 0;
        indexSkillEnable = 0;
        EnableSkill = null;
	}

	void DisableButton(){
		bSkill1.interactable = false;
		bSkill2.interactable = false;
		bSkill3.interactable = false;
		bSkill4.interactable = false;
	}

	void EnableButton(){
		bSkill1.interactable = true;
		bSkill2.interactable = true;
		bSkill3.interactable = true;
		bSkill4.interactable = true;
	}
	/// <summary>
	/// Check the specified i.
	/// </summary>
	/// <param name="i">The index.</param>
	void Check(int i){
        GetComponent<mainController>().useSkill = indexSkillEnable != 0 ? (indexSkillEnable == 4 ? false : true) : false;
		DisableButton ();
		switch (i) {
		case 1:
			bSkill1.interactable = true;
			break;
		case 2:
			bSkill2.interactable = true;
			break;
		case 3:
			bSkill3.interactable = true;
			break;
		case 4:
			bSkill4.interactable = true;
			break;
		default:
			EnableButton ();
			break;
		}
	}

	bool checkMP(SkillDb Skill){
        if(mainControll.myMC.MpDefault < Skill.MPCost)
        return false;
        return true;
	}

    void ChangePosition(float x)
    {
        StartCoroutine(AutoMove(x));
    }
    public IEnumerator AutoMove(float x)
    {
        float i = 0;
        while (i < x)
        {
            i += 0.4f;
            int huong = transform.eulerAngles.y == 0 ? 1 : -1;
            mainControll.transform.position += new Vector3(0.4f *huong, 0, 0);
            yield return new WaitForFixedUpdate();
        }
    }
}
