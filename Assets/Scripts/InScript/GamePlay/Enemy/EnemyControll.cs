using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyControll : MonoBehaviour {

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // variables
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // database
	monster m_dbMonsterList; // list of all monster in the game
    monsterFx m_dbMonsterFXList; // list of all monsterFX in the game
    public MonsterFxDb m_dbMonsterFX; // database of the monster'sFX
    public MonsterDb m_dbMonster; // database of the monster : HP, ATK, Dodge etc..

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // components
    Transform m_tfTarget; // the target, which monster try to find, follow and attack etc..
    Rigidbody2D m_rb2dBody; // the rigidbody2d of the monster
	Animator m_animatorController; // animator controller of the monster
	Animator m_animatorCameraSkaing; // animator controller the camera to shake screen

	//
    public Image m_imgHPBar; // UI display HP/MaxHP of the monster

    ///////////////////////////////////////////////////////////////////////////////////////////////
	// properties
	public int m_nID; // <IdMonster>
	// <NameMonster> // skip
	int m_nMonsterType; // <MonsterType>
	int m_nScore; // <Score>
	float m_fTimeLife; // <TimeLife>
	float m_fMaxHP; // <HPDefault>
	float m_fMovementSpeed; // <MoveSpeed>
	float m_fDelayAttack; // <DelayAtk>
	int m_nDamageType; // <DameType>
	ATK[] m_arATKList; // <ATKList>
	float m_fRateDodge; // <RateDodge> // 0.0f <-> 1.0f // the possibility of dodging the attack
	float m_fRateDrop; // <RateDrop> // 0.0f <-> 1.0f // the possibility of item dropping when the monster die
	float m_fMCRegen; // <MCRegen> // 0.0f <-> 1.0f // 
	ESC m_oMonsterEsc; // <ESC>
	SlotItem[] m_arDropList;// <DropList> // list of items can be dropped when the monster dies

    ///////////////////////////////////////////////////////////////////////////////////////////////
	// supporter
	[HideInInspector]
    public float m_fCurrentHP; // current HP of the monster
	bool m_bIsDead = false; // check if monster was dead
    bool m_bIsAbleToMove = false; // check if the monster can move
    public int m_nDirection; // check if the monster is facing left
    bool m_bIsAbleToAttack; // check if the monster was close enough to attack target
	bool m_bIsCritical = false; // check if the monster can double damage of it's next attack
	public ATK m_oSelectedAttack; // the monster will act this attack for it's next attack 
	float m_fAttack1Rate; // variable to hold rate of monster's attack 1
	float m_fAttack2Rate; // variable to hold rate of monster's attack 2
	float m_fAttack3Rate; // variable to hold rate of monster's attack 3
    int m_nAttackCounter = 0; // number to say, what attack action monster is going to act
    public bool m_bGotHit = false; // check if the monster was hit by hero or something else
	float m_fAnchorTimeForNextMove; // ulti this anchor time, the monster can NOT move
	float m_fAnchorTimeForNextAttack = 0.0f; // ulti this anchor time, the monster can NOT attack
	float m_fAnchorTimeForNextLockAttack = 0.0f; // ulti this anchor time, the monster can NOT be disable it's attack
    int m_indexAttack; // unknow, NEVER_USED
    float m_fMoveCycle; // cycle of the movement of the monster
    float m_fBreakTimeCycle; // cycle of the movement of the monster
    float m_TimeOld; // unknow, NEVER_USED
    bool m_bDataLoaded = false;
    float m_fLeftLimit; // the monster can NOT move to left if it's positionX <= this limit
    float m_fRightLimit; // the monster can NOT move to right if it's positionX >= this limit

    public GameObject m_goExplosionEffect; // object to display explosion when the monster dies
	public GameObject m_goBloodEffect; // object to display blood when the monster is attacked
	public GameObject m_goEffectDust; // object to display dust when the monster's attack hit the ground
    [HideInInspector]
    public Vector3 m_v3DirectionBlood; // direction of the blood effect
    bool m_bIsAffectedFreeze = false; // current HP of the monster
    public GameObject m_goEffectFreeze; // object to display blood when monster is affected by skill of the hero
    GameObject m_goTextDamage; // object to display number of damage monster took for an attack (maybe critial)

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // functions and methods
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // get components : rb2d, animator controller from the instance, which this script was added
    // set 'true' to 'start' parameter of the animator, SO the monster will do action appear
    void Awake() {
        m_rb2dBody = GetComponent<Rigidbody2D>();
        m_animatorController = GetComponent<Animator>();
        m_animatorController.SetBool("isAppeared", true);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // get database on the current scene
    // load list of the monsterFX from database
    // load list of the monster from database
    // CHEAT : maybe action load take some time, create an invoke function to do something with data
    void Start() {
        m_goTextDamage = gameObject.GetComponentInChildren<Text>().gameObject;
        GameObject _dbDatabaseOfScene = GameObject.Find("database");
        m_dbMonsterFXList = _dbDatabaseOfScene.GetComponent<monsterFx>();
        m_dbMonsterList = _dbDatabaseOfScene.GetComponent<monster>();
		m_animatorCameraSkaing = GameObject.FindGameObjectWithTag("CameraShaking").GetComponent<Animator>();
        Invoke("LoadProperties", 0.1f);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // set 'false' to 'start' parameter of the animator, SO the monster will do action idle, after action appear finished
    // load and set 'Player' tranform as the target of the monster
    // check in the monster list to find databases of the monster and monsterFX via ID of the monster and ID of the monsterFX
    // release monster list and monsterFX list after get data
    // load all data we need from db to variables (via db construction)
    // if the monster has timelife, set 'Destroy' method to the monster with a delay equal to monster's timelife, SO it will be die after it's timelife
    void LoadProperties() {
        m_animatorController.SetBool("isAppeared", false);
        m_tfTarget = GameObject.FindGameObjectWithTag("Player").transform;

        foreach (var _aMonsterFX in m_dbMonsterFXList.Player.MonsterFxList) {
            if (m_nID == _aMonsterFX.IdMonsterFX) {
                m_dbMonsterFX = _aMonsterFX;
                break;
            }
        }
        foreach (var _aMonster in m_dbMonsterList.Player.MonsterDbList) {
			if (m_nID == _aMonster.IdMonster) {
                m_dbMonster = new MonsterDb(_aMonster);
                break;
            }
        }

        m_dbMonsterFXList = null;
        m_dbMonsterList = null;

		m_nMonsterType = m_dbMonster.MonterType;
        m_nScore = m_dbMonster.Score;
        m_fTimeLife = m_dbMonster.TimeLife;
        m_fMaxHP = (float)m_dbMonster.HpDefault;
        m_fMovementSpeed = m_dbMonster.MoveSpeed;
        m_fDelayAttack = m_dbMonster.delayAtk;
        m_nDamageType = m_dbMonster.DameType;
		m_arATKList = m_dbMonster.ATKList;
        m_fRateDodge = m_dbMonster.RateDodge;
        m_fRateDrop = m_dbMonster.RateDrop;
        m_fMCRegen = m_dbMonster.MCRegen;
		m_oMonsterEsc = m_dbMonster.ESC;
        m_arDropList = m_dbMonster.Droplist;

		foreach (ATK _oTempAttack in m_arATKList) {
			if (_oTempAttack.RankATK == 1) {
				m_fAttack1Rate = _oTempAttack.Rate;
			}
			if (_oTempAttack.RankATK == 2) {
				m_fAttack2Rate = _oTempAttack.Rate;
			}
			if (_oTempAttack.RankATK == 3) {
				m_fAttack3Rate = _oTempAttack.Rate;
			}
		}

        m_fCurrentHP = m_fMaxHP;
        m_fLeftLimit = Camera.main.GetComponent<CameraFollow>().StartLook + 2.0f;
        m_fRightLimit = Camera.main.GetComponent<CameraFollow>().EndLook + Camera.main.orthographicSize * 2.0f - 3.0f;

        // LOG : check if rate attack of the monster is wrong
        //if (m_fRateATK1 + m_fRateATK2 + m_fRateATK3 != 1.0f) {
        //    Debug.Log("Attack rates of the monster is wrong!");
        //}

        m_fMoveCycle = Random.Range(1.0f, 4.0f);
        m_fBreakTimeCycle = Random.Range(1.0f, 2.0f);

        if (m_fTimeLife > 0.0f) {
            Invoke("ExplodeBeforeDie", m_fTimeLife);
        }

		m_bDataLoaded = true;
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void Update() {
		if (m_bDataLoaded == false) {
			return;
		}
		if (m_bIsDead == true) {
			ExplodeBeforeDie();
			return;
		}
		OnAffectedBySkill();
		if (m_bIsAbleToMove == true) {
			Move (m_fMovementSpeed / 10.0f * m_animatorController.speed, m_nDirection);
		}
		else {
			Move (0.0f, m_nDirection);
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void FixedUpdate() {
		if (m_bDataLoaded == false) {
			return;
		}
		if (m_bIsDead == true) {
			m_bGotHit = false;
			m_bIsAbleToMove = false;
			m_bIsAbleToAttack = false;
			m_nAttackCounter = 0;
			UpdateAnimator();
			return;
		}
		if (m_fCurrentHP <= 0.0f) {
			m_bIsDead = true;
		}
		AnimatorStateInfo _asiCurrentState = m_animatorController.GetCurrentAnimatorStateInfo(0);
		if (m_nAttackCounter == 0) {
			float _fDecideAttack = Random.Range (0.0f, 1.0f);
			float _fCritical = Random.Range (0.0f, 1.0f);
			foreach (ATK _oTempAttack in m_arATKList) {
				if (_oTempAttack.RankATK == 1) {
					if (_fDecideAttack <= _oTempAttack.Rate) {
						m_nAttackCounter = 1;
						m_bIsCritical = _fCritical < _oTempAttack.RateCrist ? true : false;
						m_oSelectedAttack = _oTempAttack;
					}
				}
				if (_oTempAttack.RankATK == 2) {
					if ((m_fAttack1Rate < _fDecideAttack) && (_fDecideAttack <= (m_fAttack1Rate + m_fAttack2Rate))) {
						m_nAttackCounter = 2;
						m_bIsCritical = _fCritical < _oTempAttack.RateCrist ? true : false;
						m_oSelectedAttack = _oTempAttack;
					}
				}
				if (_oTempAttack.RankATK == 3) {
					if ((m_fAttack1Rate + m_fAttack2Rate) < _fDecideAttack) {
						m_nAttackCounter = 3;
						m_bIsCritical = _fCritical < _oTempAttack.RateCrist ? true : false;
						m_oSelectedAttack = _oTempAttack;
					}
				}
			}
		}
		float _fDistance = transform.position.x - m_tfTarget.position.x;
		m_nDirection = _fDistance > 0.0f ? -1 : 1;
		_fDistance = Mathf.Abs(_fDistance);
		if (_fDistance > m_oSelectedAttack.RangerATK) {
			m_bIsAbleToMove = true;
			m_bIsAbleToAttack = false;
		}
		else {
			m_bIsAbleToMove = false;
			m_bIsAbleToAttack = true;
		}
		if (_fDistance <= m_oMonsterEsc.RangerArlet) {
			AvoidHero ();
			if (m_fAnchorTimeForNextMove < Time.time + m_oMonsterEsc.DelayESC) {
				m_bIsAbleToMove = false;
			}
			if (Time.time > m_fAnchorTimeForNextMove) {
				m_fAnchorTimeForNextMove += m_oMonsterEsc.RangerESC / m_fMovementSpeed + m_oMonsterEsc.DelayESC;
			}
		}
		else {
			if (m_fAnchorTimeForNextMove < Time.time + m_fBreakTimeCycle) {
				m_bIsAbleToMove = false;
			}
			if (Time.time > m_fAnchorTimeForNextMove) {
				m_fAnchorTimeForNextMove += m_fMoveCycle + m_fBreakTimeCycle;
			}
		}
		if (m_fAnchorTimeForNextLockAttack >= Time.time) {
			m_bIsAbleToAttack = false;
		}
		if (m_fAnchorTimeForNextAttack >= Time.time) {
			m_bIsAbleToAttack = true;
		}

		if (_asiCurrentState.IsName ("atk_1") == true || _asiCurrentState.IsName ("atk_2") == true || _asiCurrentState.IsName ("atk_3") == true) {
			if (m_fAnchorTimeForNextLockAttack <= Time.time) {
				m_fAnchorTimeForNextAttack = Time.time + _asiCurrentState.length;
				m_fAnchorTimeForNextLockAttack = m_fAnchorTimeForNextAttack + m_fDelayAttack;
				Invoke ("ResetAttackCounter", _asiCurrentState.length);
			}
			m_bIsAbleToMove = false;
			m_rb2dBody.constraints = RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
		}
		else {
			//gameObject.GetComponent<CircleCollider2D> ().enabled = false;
			//gameObject.GetComponent<CircleCollider2D> ().isTrigger = false;
			ShakingDisable ();
			m_rb2dBody.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		if (m_bGotHit == true) {
			m_bIsAbleToAttack = false;
			m_bIsAbleToMove = false;
			if (_asiCurrentState.IsName ("get_hit")) {
				m_bGotHit = false;
			}
		}
		if (_asiCurrentState.IsName ("fall") || _asiCurrentState.IsName ("get_up")) {
			m_animatorController.SetBool("isFall", false);
			m_bGotHit = false;
			m_bIsAbleToMove = false;
		}

		UpdateAnimator();
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // set 'false' to 'm_bIsAbleToAttack', SO the monster will NOT attack when it needs to run away from hero
    // get the monster's current state, TO make sure that it just runs away after finish it's current action (often attack action)
    // set 'true' to 'm_bIsAbleToMove', SO the monster will run away
    // reverse direction of the monster, so it will NOT face to the hero anymore
    // BUT if the monster run to the limit of the camera, it can NOT run to that direction anymore, it will turn back, CHECK ON FUNCTION'S BODY
    void AvoidHero() {
        m_bIsAbleToAttack = false;
        AnimatorStateInfo _asiCurrentState = m_animatorController.GetCurrentAnimatorStateInfo(0);
        if (m_nAttackCounter == 0
            && !_asiCurrentState.IsName("atk_1")
            && !_asiCurrentState.IsName("atk_2")
            && !_asiCurrentState.IsName("atk_3")
            )
		{
            m_bIsAbleToMove = true;
            m_nDirection = -m_nDirection;
            if ((transform.position.x - m_fLeftLimit <= 0.0f) // the monster run over the left limit
                || (m_fRightLimit - transform.position.x <= 0.0f) // the monster run over the right limit
                || ((transform.position.x - m_fLeftLimit) < (m_tfTarget.position.x - m_fLeftLimit) // the monster is between left limit and the hero
					&& (m_tfTarget.position.x - m_fLeftLimit < m_oMonsterEsc.RangerArlet)) // and distance from hero to left limit < monster's RangerArlet
                || ((m_fRightLimit - transform.position.x) < (m_fRightLimit - m_tfTarget.position.x) // the monster is between right limit and the hero
					&& (m_fRightLimit - m_tfTarget.position.x < m_oMonsterEsc.RangerArlet)) // and distance from hero to right limit < monster's RangerArlet																			
                )
			{
                m_nDirection = -m_nDirection;
            }
        }
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
	//
	void ResetAttackCounter() {
        m_nAttackCounter = 0;
        m_animatorController.SetInteger("nAttackCounter", m_nAttackCounter);
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // update UI of the HP bar
    // set value for parameters of the animator controller of the monster, via variables
    void UpdateAnimator() {
        m_imgHPBar.fillAmount = m_fCurrentHP / m_fMaxHP;
        m_animatorController.SetBool("isDead", m_bIsDead);
        m_animatorController.SetBool("gotHit", m_bGotHit);
		m_animatorController.SetFloat("velocity", Mathf.Abs(m_rb2dBody.velocity.x));
		m_animatorController.SetInteger("nAttackCounter", m_nAttackCounter);
		m_animatorController.SetBool("isAbleToAttack", m_bIsAbleToAttack);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
	void ExplodeBeforeDie() {
		m_bIsDead = true;
		m_animatorController.speed = 1.0f;
		Move(0, 0);
        m_imgHPBar.transform.parent.gameObject.SetActive(false);
        if (m_fTimeLife > 0.0f) {
            m_animatorController.SetBool("isPrepareToExplode", true);
            AnimationClip[] _arClips = m_animatorController.runtimeAnimatorController.animationClips;
            foreach (AnimationClip _acClip in _arClips) {
                if (_acClip.name == "prepare_to_explode") {
                    float _fPrepareDuration = _acClip.length;
                    Invoke("ShowExplosion", _fPrepareDuration);
					Invoke("Die", _fPrepareDuration + Time.deltaTime);
                }
            }
        }
        else {
            Die();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
	void Die() {
		m_animatorController.SetBool("isPrepareToExplode", false);
        m_tfTarget.gameObject.GetComponent<mainController>().GetScore(m_nScore);
        //m_tfTarget.gameObject.GetComponent<mainController>().RegenByKilling(m_fMCRegen);
        foreach (var _aItem in m_arDropList) {
            float _fRateSlot = Random.Range(0.0f, 1.0f);
            if (_fRateSlot < _aItem.RateSlot) {
                var _aItemControl = GameObject.Find("ItemControll").GetComponent<ListItemControll>();
                for (int i = 0; i < _aItem.Count; i++) {
                    _aItemControl.BornItem(_aItem.IdItem, transform.position);
                    break;
                }
            }
        }
        m_animatorController.speed = 1.0f;
        m_animatorController.SetBool("isDead", true);
        Destroy(gameObject, 2.0f);
        Destroy(this);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    void Move(float p_fSpeed, int p_nDirection) {
        m_rb2dBody.velocity = new Vector2(p_fSpeed * p_nDirection, m_rb2dBody.velocity.y);
		if (m_bIsAffectedFreeze == false && m_bIsDead == false) {
			transform.eulerAngles = new Vector3(0.0f, p_nDirection < 0 ? 180.0f : 0.0f, 0.0f);
		}
        m_imgHPBar.rectTransform.eulerAngles = new Vector3(0, p_nDirection > 0 ? 180 : 0, 0);
        m_goTextDamage.GetComponent<Text>().rectTransform.eulerAngles = new Vector3(0, 0, 0);
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // // function call to make the monster deal damage for hero, parameter 'p_goDestination' is the hero
    // check if the hero does NOT have a script controller, we can NOT deal damage for him, then return
    // via 'nAttackCounter', we decide to choose damage to deal
    // if 'm_bIsCritical' is 'true', SO it means the attack is critical, we NEED TO double it's damage
    // make the hero 'TakeDamage' with 'nDamage' was chosen base on 'nAttackCounter', and tell him, this monster is the source of that damage
    public void DealDamage(GameObject p_goDestination) {
		if (p_goDestination.GetComponent<mainController>() == null || m_bIsAbleToAttack == false)
            return;
		int _nRealDamage = (int)m_oSelectedAttack.Damage;
        if (m_bIsCritical == true) {
            _nRealDamage += _nRealDamage;
        }
		p_goDestination.GetComponent<mainController>().TakeDamage(_nRealDamage, m_nDamageType, gameObject, m_bIsCritical, m_dbMonsterFX);
    }

	///////////////////////////////////////////////////////////////////////////////////////////////
	// // shake and stop shaking camera
	public void ShakingEnable() {
		m_animatorCameraSkaing.SetBool("shake", true);
	}
	public void ShakingDisable() {
		m_animatorCameraSkaing.SetBool("shake", false);
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    public void TakeDamage(int p_nDamage, GameObject p_goSource, bool p_bCritial) {
		float _fDodge = Random.Range(0.0f, 1.0f);
		AnimatorStateInfo _asiCurrentState = m_animatorController.GetCurrentAnimatorStateInfo(0);
		if (_asiCurrentState.IsName ("atk_1") == true || _asiCurrentState.IsName ("atk_2") == true || _asiCurrentState.IsName ("atk_3") == true) {
			if (m_oSelectedAttack.DamageInfu == 1) {
				TextEffectDamage(0, p_bCritial);
				m_bGotHit = false;
				return;
			}
		}
		if (_fDodge < m_fRateDodge) {
            TextEffectDamage(0, p_bCritial);
            m_bGotHit = false;
            return;
        }
        if (p_bCritial == true) {
            p_nDamage += p_nDamage;
            float _fKnockDistance = Random.Range(1.0f, 3.0f);
            if (_fKnockDistance >= 1.5f) {
                m_animatorController.SetBool("isFall", true);
            }
            m_rb2dBody.velocity = new Vector2(m_nDirection * (-_fKnockDistance), m_rb2dBody.velocity.y);
        }
        m_fCurrentHP -= p_nDamage;
        TextEffectDamage(p_nDamage, p_bCritial);
        m_bGotHit = true;
        ShowBlood();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    void TextEffectDamage(int nDamage, bool bCritial) {
        if (nDamage == 0) return;
        m_goTextDamage.GetComponent<Text>().text = nDamage.ToString();
        if (bCritial) {
            m_goTextDamage.GetComponent<Text>().fontSize = 28;
            m_goTextDamage.GetComponent<Text>().fontStyle = FontStyle.Bold;
        }
        else {
            m_goTextDamage.GetComponent<Text>().fontStyle = FontStyle.Normal;
            m_goTextDamage.GetComponent<Text>().fontSize = 14;
        }
        /*
        if (nDamage == 0) {
            m_goTextDamage.GetComponent<Text>().text = "Miss";
            m_goTextDamage.GetComponent<Text>().fontStyle = FontStyle.Normal;
            m_goTextDamage.GetComponent<Text>().fontSize = 14;
        }
         * */
        m_goTextDamage.GetComponent<Animator>().Play("TextEffect");
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    public void ShowExplosion() {
        GameObject _goEffectExplosion = Instantiate(m_goExplosionEffect, transform.position, Quaternion.identity);
        float _fDistanceExplosionToHero = Mathf.Abs(_goEffectExplosion.transform.position.x - m_tfTarget.position.x);
		if (_fDistanceExplosionToHero < 10.0f) {
			m_animatorController.SetBool("isPrepareToExplode", false);
            m_nAttackCounter = 1;
            m_bIsCritical = true;
            DealDamage(m_tfTarget.gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    public void ShowBlood() {
		GameObject _goEffectBlood = Instantiate(m_goBloodEffect, transform.position, Quaternion.identity);
		float _fPositionY = Random.Range(-0.5f, -0.1f);
        float _fScale = Random.Range(0.7f, 1.3f);
		_goEffectBlood.transform.position += new Vector3(0, _fPositionY, -1);
        _goEffectBlood.transform.position += new Vector3(0.3f, -0.1f, -1);
		_goEffectBlood.transform.localScale = new Vector3(_fScale, _fScale, _fScale);
        _goEffectBlood.gameObject.transform.eulerAngles = m_v3DirectionBlood;
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	void ShowDustOnAttack() {
		Vector3 _v3EffectPosition = transform.Find ("Attack2Effect").transform.position;
		GameObject _goEffectDust = Instantiate (m_goEffectDust, _v3EffectPosition, Quaternion.identity);
		float _fScale = 5.0f;
		_goEffectDust.transform.localScale = new Vector3 (_fScale, _fScale, _fScale);
		_v3EffectPosition.y += _goEffectDust.GetComponent<Renderer> ().bounds.size.y / 4;
		_goEffectDust.transform.position = _v3EffectPosition;
		_goEffectDust.transform.eulerAngles = new Vector3 (0, m_nDirection < 0 ? 180 : 0, 0);
		Destroy (_goEffectDust, 2.0f);
	}

	///////////////////////////////////////////////////////////////////////////////////////////////
	// 
	public void ShowDustOnMove() {
		Vector3 _v3EffectPosition = transform.Find ("Foot").transform.position;
		GameObject _goEffectDust = Instantiate (m_goEffectDust, _v3EffectPosition, Quaternion.identity);
		float _fScale = 5.0f;
		_goEffectDust.transform.localScale = new Vector3 (_fScale, _fScale, _fScale);
		_v3EffectPosition.y += _goEffectDust.GetComponent<Renderer> ().bounds.size.y / 4;
		_goEffectDust.transform.position = _v3EffectPosition;
		_goEffectDust.transform.eulerAngles = new Vector3 (0, m_nDirection < 0 ? 180 : 0, 0);
		Destroy (_goEffectDust, 2.0f);
	}

    ///////////////////////////////////////////////////////////////////////////////////////////////
    // 
    void OnAffectedBySkill() {
        if (m_animatorController.speed <= 0.5f && !m_bIsAffectedFreeze) {
			GameObject _goEffectFreeze = Instantiate(m_goEffectFreeze, transform.Find("Foot").transform.position, Quaternion.identity, transform);
			float _fScale = gameObject.GetComponent<BoxCollider2D> ().bounds.size.x / _goEffectFreeze.GetComponent<Renderer> ().bounds.size.x;
			_goEffectFreeze.transform.localScale = new Vector3 (_fScale, _fScale, _fScale);
			m_bIsAffectedFreeze = true;
        }
        else {
            if (m_animatorController.speed != 0) {
                if (m_bIsAffectedFreeze == true) {
					Destroy (transform.Find ("Effect Freeze(Clone)").gameObject);
                    m_bIsAffectedFreeze = false;
				}
            }
        }
	}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // collision handle
    // to add this script controller, gameobject has to have 3 collider 2D
    //         + edge collider 2D : for the foot, keep the rigidbody2D of the gameobject stay on the ground
    //         + box collider 2D : for the body of the gameobject, check when monster was hit
    //                  IS TRIGGER, so the gameobject can move through other one
    //         + circle collider 2D : for the weapon the the monster, deal damage to hero when it hit hero's body
    //                  default 'enable' to 'false', just be enabled when the monster attack (ON ANIMATION)
    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
	void OnCollisionEnter2D(Collision2D cll) {
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
			gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            if (m_bIsAbleToAttack == true) {
				DealDamage(cll.gameObject);
            }
		}
	}

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
	void OnCollisionStay2D(Collision2D cll) {
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
		}
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // 
    void OnTriggerEnter2D(Collider2D cll) {
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
            var x = transform.position.x - cll.transform.position.x;
            if (x <= 0)
                m_v3DirectionBlood = new Vector3(0, 180, 0);
            else
                m_v3DirectionBlood = new Vector3(0, 0, 0);
		}
    }

}
