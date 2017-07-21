using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyControll_Old : MonoBehaviour {

    /*
	//Database
	monster Monsters;
	monsterFx MonsterFx;
	//

	public Image HpBar;
	public int myID;
	public MonsterFxDb MyFx;
	public MonsterDb MyProperties;

	Transform Player;
	Rigidbody2D myRg;
	Animator myAnim;
	 
	public bool attack = false, move = false, boolattack, GetHit = false, update= false;
	float timeNext, timeScale = 1, Damge, TypeAttack, maxHP, timeRangeMove, TimeOld;
	int Direction, indexAttack;
	bool die = false;

	void Awake(){
		//get Database
		//
		myRg = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myAnim.SetBool ("start", true);
	}

	void Start () {
        textdamage = gameObject.GetComponentInChildren<Text>().gameObject;
		GameObject g = GameObject.Find ("database");
		Monsters = g.GetComponent<monster> ();
		MonsterFx = g.GetComponent<monsterFx> ();
		Invoke ("OnStart", 0.1f);
	}

	void OnStart(){
		myAnim.SetBool ("start", false);
		Player= GameObject.FindGameObjectWithTag ("Player").transform;

		foreach (var item in MonsterFx.Player.MonsterFxList) {
			if (myID == item.IdMonsterFX) {
				MyFx =item;
				break;
			}
		}

		foreach (var item in Monsters.Player.MonsterDbList) {
			if (MyFx.IdMonster == item.IdMonster) {
				MyProperties = new MonsterDb (item);
				break;
			}
		}
		Monsters = null;
		MonsterFx = null;
		if(MyProperties.TimeLife != 0)
			Destroy(gameObject,MyProperties.TimeLife);
		maxHP = (float)MyProperties.HpDefault;
		timeScale = MyProperties.delayAtk;
		timeRangeMove = Random.Range (1, 4);
		update = true;
	}

    void Update()
    {
        textdamage.GetComponent<Text>().rectTransform.eulerAngles = new Vector3(0, 0, 0);
        if (!update || !checkMain() || die)
            return;
        OnUpdate();
	}

	void OnUpdate(){
		if (MyProperties.HpDefault <= 0) {
            myAnim.speed = 1;
			Die ();
		}
        if (myAnim.speed == 0) return;
		float distance = transform.position.x - Player.position.x;
		Direction = distance> 0 ? -1 : 1;
		distance = Mathf.Abs (distance);
		move = distance > MyProperties.Ranger ? true : false;
		if (timeNext - Time.time < timeRangeMove) {
			move = false;
		}
		if (Time.time > timeNext) {
			timeNext += timeRangeMove + Random.Range(1,2);
		}

		boolattack = distance <= MyProperties.Ranger ? true : false;
		//
		HpBar.fillAmount = MyProperties.HpDefault/ maxHP;
		if (GetHit)
			boolattack = false;
		UpdateAnimator ();
        if (Time.time > timeNextFx) getFx = true;
		update = true;
	}



	public void hasMove(int index){
		move = index == 0?false:true;
	}

	void FixedUpdate(){
        if (update && !die)
        {
            OnEffectGet();
            if (myAnim.speed == 0) return;
			var now = myAnim.GetCurrentAnimatorStateInfo (0);
			if (move && !GetHit && !now.IsName("down") && !now.IsName("stand") && !now.IsName("gethit"))
				Move (MyProperties.MoveSpeed / 10 *myAnim.speed, Direction);
			if (boolattack) {
				if (Time.time >= timeNext) {
					timeNext = Time.time + timeScale;
					transform.eulerAngles = new Vector3 (0, Direction > 0 ? 0 : 180,0);
					attack = true;
				}
			}

			if (GetHit) {
				if (Time.time >= timeNext) {
					timeNext = Time.time + timeScale;
					GetHit = false;
				}
			}
        }
	}

	void LateUpdate(){
		attack = false;
        GetHit = false;
        myAnim.SetBool("takedown", false);
	}

	void UpdateAnimator(){
		if (GetHit)
			move = false;
		myAnim.SetBool ("move",move);
		myAnim.SetInteger ("attack", attack?1:0);
		myAnim.SetBool ("gethit", GetHit);
	}

	void Move(float speed, int Direction){
		myRg.velocity = new Vector2 (speed * Direction, myRg.velocity.y);
		int Direc = 0;
		if (Direction < 0)
			Direc = 180;
		transform.eulerAngles = new Vector3 (0, Direc,0);
		HpBar.rectTransform.eulerAngles = new Vector3 (0,  0,0);
	}

	void Die(){
		die = true;
		Move (0, 0);
        Player.gameObject.GetComponent<mainController>().GetScore(MyProperties.Score);
        foreach (var item in MyProperties.Droplist)
        {
            float index = Random.Range(0f, 1f);
            if (index < item.RateSlot)
            {
                var ItemControl = GameObject.Find("ItemControll").GetComponent<ListItemControll>();
                 ItemControl.BornItem(item.IdItem,transform.position);
            }
        }
        myAnim.speed = 1;
		myAnim.SetBool ("die", true);
		Destroy (gameObject, 2);
		Destroy (this);
	}


	void OnCollisionEnter2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
			if (boolattack) {
                DealDamage(cll.gameObject);
			}
		}
	}

	void OnCollisionStay2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			gameObject.GetComponent<CircleCollider2D> ().isTrigger = true;
		}
	}

	void OnTriggerExit2D(Collider2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer("Player")) {
			GetComponent<CircleCollider2D> ().isTrigger = false;
			getD = false;
		}
	}
	bool getD = false;
    void OnTriggerEnter2D(Collider2D cll)
    {
        if (cll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var x = transform.position.x - cll.transform.position.x;
            if (x <= 0) DirectionBlood = new Vector3(0, 180, 0);
            else DirectionBlood = new Vector3(0, 0, 0);
        }
    }
	void OnTriggerStay2D(Collider2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			if (cll.gameObject.GetComponent<mainController> ().stepD && !getD) {
				getD = true;
				float Far = Random.Range (1, 3f);
				if (Far >= 2.5f) {
					myAnim.SetBool ("takedown", true);
				}
				else
					GetHit = true;	
				myRg.velocity = new Vector2 (Direction * -Far, myRg.velocity.y);
			}
		}

	}

	public void OnAttackMe(int Damge){
		MyProperties.HpDefault -= Damge;
        GetHit = true;
		bloodEnable ();
	}
	void Await(){
		return;
	}

	bool checkMain(){
		return GameObject.FindGameObjectWithTag ("Player");
	}

	public GameObject BloodEffect;
	[HideInInspector]
	public Vector3 DirectionBlood;
	public void bloodEnable(){
		var e = Instantiate (BloodEffect, transform.position, Quaternion.identity);
		float Scale = Random.Range (0.7f, 1.3f);
		float PosY = Random.Range (-0.5f, -0.1f);
		e.transform.position += new Vector3 (0, PosY, -1);
		e.transform.position += new Vector3 (0.3f, -0.1f, -1);
		e.transform.localScale = new Vector3(Scale, Scale, Scale);
		e.gameObject.transform.eulerAngles = DirectionBlood;
	}

    bool GetEffect = false;
    public GameObject EffectFreeze;

    void OnEffectGet()
    {
        if (myAnim.speed <=0.5 && !GetEffect)
        {
            Instantiate(EffectFreeze, transform.FindChild("Foot").transform.position, Quaternion.identity, transform);
            GetEffect = true;
        }
        else
        {
            if (myAnim.speed != 0)
            {
                if(GetEffect)
                    Destroy(transform.FindChild("Effect Freeze(Clone)").gameObject);
                GetEffect = false;
            }
        }
    }

    bool getFx = true;
    float timeNextFx;
    public void DealDamage(GameObject destination)
    {
        if (destination.GetComponent<mainController>() == null)
            return;
        int damge = MyProperties.DameATK1;
        destination.GetComponent<mainController>().TakeDamage(damge, gameObject);
        if (!getFx) return;
        timeNextFx = Time.time + MyFx.Cooldown;
        destination.GetComponent<mainController>().monsterFx = MyFx;
        destination.GetComponent<mainController>().timeLastGetMonsterFx = Time.time;
        getFx = false;
    }

    public void TakeDamage(int Damage, GameObject source, bool Crist)
    {
        MyProperties.HpDefault -= Damage;
        TextEffectDamage(Damage, Crist);
        GetHit = true;
        bloodEnable();
    }


    GameObject textdamage;
    void TextEffectDamage(int Damage, bool Crist)
    {
        textdamage.GetComponent<Text>().text = Damage.ToString();
        textdamage.GetComponent<Animator>().Play("TextEffect");
        if (Crist)
        {
            textdamage.GetComponent<Text>().fontSize = 28;
            textdamage.GetComponent<Text>().fontStyle = FontStyle.Bold;
        }
        else
        {
            textdamage.GetComponent<Text>().fontStyle = FontStyle.Normal;
            textdamage.GetComponent<Text>().fontSize = 14;
        }
    }

    */
}
