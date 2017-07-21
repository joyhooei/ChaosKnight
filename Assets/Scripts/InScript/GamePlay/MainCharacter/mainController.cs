using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class mainController : MonoBehaviour
{
	// public database
	public int MyID;
	mc MCs;

	// My Properties
	//[HideInInspector]
	public MCDb myMC;

	public player MyPlayer;

	// Tham bien su dung coong khai
	[HideInInspector]
	public Animator myAnim;
	public float jumpHeight;
	public MonsterFxDb monsterFx;
	[HideInInspector]
	public float speed;
	[HideInInspector]
	public int attack;
	//For run Effect
	public Transform Footer;
	public GameObject EffectRun;

	// Tham bien su dung noi bo
	Image hpBar, mpBar;
    Text textGold;
	[HideInInspector]
	public Rigidbody2D myRg;
	SpriteRenderer mySprite;
	Animator CameraSkaing;
	Button BtnJump;

	//set for animator
	[HideInInspector]
	public bool update = false, jump = false, gethit = false, die = false, start = true,skill = false;
	int jumpcount = 0;

	// tham bien khac
	public float maxHp,maxMp;
	[HideInInspector]
	public int damgeAttack;
	//
	[HideInInspector]
	public float moveSpeed;
	[HideInInspector]
	public float Crist, Vamp;
    bool HCrist = false;
	//
	[HideInInspector]
	public bool useSkill = false;

	/// <summary>
	/// Awake this instance.
	/// </summary>
	void Awake(){
		//
		//
		mySprite = GetComponent<SpriteRenderer> ();
		myRg = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();
		myAnim.SetBool ("start", true);
		hpBar = GameObject.Find ("HPBAR").gameObject.GetComponent<Image> ();
		mpBar = GameObject.Find ("MPBAR").gameObject.GetComponent<Image> ();
        textGold = GameObject.Find("txtGold").gameObject.GetComponent<Text>();
		CameraSkaing = GameObject.FindGameObjectWithTag("CameraShaking").GetComponent<Animator> ();
        textdamage = GameObject.Find("txtDamage");
    }

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		//Time.timeScale = 0.7f;
		OnStart();
		//Invoke("OnStart",0.1f);
		Invoke ("OnUpdateFirst", 0.3f);
		//
		BtnJump = GameObject.Find("btnJump").GetComponent<Button>();
		BtnJump.onClick.AddListener (jumpButton);
	}


	/// <summary>
	/// Raises the start event.
	/// </summary>
	void OnStart(){
        GetComponent<mc>().Load();
		MCs = GetComponent<mc> ();
		MyPlayer = GetComponent<player> ();
		foreach (var item in MCs.Player.MC) {
			if (item.IdMC == MyID) {
				myMC = new MCDb(item);
				break;
			}
		}
		attack = 0;
	}

	void OnUpdateFirst(){
		myAnim.SetBool ("start", false);
		moveSpeed = myMC.MoveSpeed;
		maxHp = myMC.HpDefault;
		maxMp = myMC.MpDefault;
		start = false;
		update = true;
	}

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update () {
		if (!update || die || start)
			return;

		if (myMC.HpDefault <= 0) {
			Die ();
		}

		hpBar.fillAmount = myMC.HpDefault*1f/ maxHp;
		mpBar.fillAmount = myMC.MpDefault*1f / maxMp;
        textGold.text = MyPlayer.Player.Coin.ToString();
        OnMonsterFxAtHit();
		UpdateAnimator ();
	}

	//Bien cho nhay
	//Bien cho tan cong
	float  InBagDelay = 4f, lastInBag;
	bool inbag = true;

	/// <summary>
	/// Fixeds the update.
	/// </summary>
	void FixedUpdate(){

		if (!update || die || start)
			return;
		// Move
		var stage = myAnim.GetCurrentAnimatorStateInfo (0);
		speed = CnControls.CnInputManager.GetAxisRaw("Horizontal");
        if (speed != 0) speed = speed>0?1:-1;
		if(speed != 0)
			inbag = true;
	
		if (speed != 0 && (stage.IsName ("Run") || stage.IsName ("Jump") || stage.IsName ("JumpI"))) {
			Move (speed, moveSpeed);
		} else {
			if (!jump) {
				myRg.velocity = new Vector2 (0, myRg.velocity.y);
			}
		}

		// Attack
		if (CnControls.CnInputManager.GetButtonUp ("Attack")) {
			if (jump && useSkill)
				return;
			inbag = false;
            float indexCrist = Random.Range(0.0f, 1.0f);
            HCrist = indexCrist <= Crist;
			attack++;
		}
		
		//
		if ( stage.IsName("GetHit") || stage.IsName("McJumpATKEnd") || stage.IsName("McJumpATKToGround")) {
			myRg.velocity = new Vector2 (myRg.velocity.x, -5);
		}
		else{
			if(attack > 0 && jump ){
                if(attack <4)
				    myRg.velocity = new Vector2 (myRg.velocity.x/2, -0.1f);
                else
                    myRg.velocity = new Vector2(myRg.velocity.x / 2, -0.8f);
			}
		}

		if (attack == 0) {
			if (Time.time >= lastInBag) {
				inbag = true;
			}
		} else
			inbag = false;

		if (jumpcount > 1 || useSkill)
			BtnJump.interactable = false;
		else {
			if (stage.IsName ("Idle") || stage.IsName ("Run") || stage.IsName ("JumpI") || stage.IsName("McAtkgroundPre2Idle"))
				BtnJump.interactable = true;
			else
				BtnJump.interactable = false;
		}
        if(myMC.MpDefault < maxMp) {
            float a = myMC.MpRegen * Time.deltaTime /myMC.MpRegenTime;
            myMC.MpDefault += a;
		}
	}

	void jumpButton(){
		if (inbag && !die) {
			jump = true;
			++jumpcount;
			myRg.velocity = new Vector2 (myRg.velocity.x, jumpHeight);
		}
        inbag = true;
	}

	void GetAttack2Zero(){
		if (attack == 0)
			return;
		lastInBag = Time.time + InBagDelay;
		attack = 0;
	}


	/// <summary>
	/// Lates the update.
	/// </summary>
	void LateUpdate(){
		if (!update || die)
			return;
		gethit = false;
		myAnim.SetBool ("start", false);
        ShakingDisable2();
        textdamage.GetComponent<Text>().rectTransform.eulerAngles = new Vector3(0, 0, 0);
	}

	/// <summary>
	/// Raises the collision enter2d event.
	/// </summary>
	/// <param name="cll">Cll.</param>
	void OnCollisionEnter2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			jumpcount = 0;
			TimeJump2Ground = Time.time + TimeJumpDelay;
			myAnim.SetBool ("jump", false);
		}
        if (cll.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var x = transform.position.x - cll.transform.position.x;
            if (x <= 0) DirectionBlood = new Vector3(0, 180, 0);
            else DirectionBlood = new Vector3(0, 0, 0);
        }
	}

	float TimeJump2Ground, TimeJumpDelay = 1f;

	/// <summary>
	/// Raises the collision stay2d event.
	/// </summary>
	/// <param name="cll">Cll.</param>
	void OnCollisionStay2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			myAnim.SetBool ("jump", false);
			jump = false;
			jumpcount = 0;
			if (Time.time >= TimeJump2Ground && inbag && !useSkill)
				BtnJump.interactable = true;
		} 
	}

	void OnCollisionExit2D(Collision2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			myAnim.SetBool ("jump", true);
			jump = true;
			jumpcount += 1;
		}
	}

	/// <summary>
	/// Raises the trigger enter2d event.
	/// </summary>
	/// <param name="cll">Cll.</param>
	void OnTriggerEnter2D(Collider2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			if (attack != 0) {
				if (cll.gameObject.GetComponent<EnemyControll> () != null) {
                    DealDamage(cll.gameObject, HCrist);
				}
			}
		}
	}

	/// <summary>
	/// Raises the trigger exit2d event.
	/// </summary>
	/// <param name="cll">Cll.</param>
	void OnTriggerExit2D(Collider2D cll)
	{
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {
			ShakingDisable2 ();
		}
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="destination"></param>

    public void DealDamage(GameObject destination,bool Crist)
    {
        int inDamge = HCrist ? 2 * damgeAttack : damgeAttack;
        int HP = (int)(damgeAttack * Vamp);
        if (ZeroDame)
            inDamge = 0;
        destination.GetComponent<EnemyControll>().TakeDamage(inDamge, gameObject, HCrist);
        myMC.HpDefault += HP;
        myMC.HpDefault = myMC.HpDefault > maxHp ? maxHp : myMC.HpDefault;
        ShakingEnable2();
    }

    public void TakeDamage(int Damage,int m_nDamageType, GameObject source, bool critical, MonsterFxDb FX)
    {
        float Dodge = Random.Range(0.0f, 1.0f);
        if (Dodge < myMC.DodgeDefault)
        {
            TextEffectDamage(0, false);
            return;
        }
        gethit = true;
        monsterFx = FX;
        bloodEnable();
        // mirror force
        Damage -= myMC.AMDefault;
        if (Damage < 0)
        {
            if (source)
            {
                source.GetComponent<EnemyControll>().TakeDamage(-Damage, gameObject, false);
            }
            return;
        }
        else
        {
            myMC.HpDefault -= Damage;
            TextEffectDamage(Damage, false);
        }
    }

	/// <summary>
	/// Move the specified index and Speed.
	/// </summary>
	/// <param name="index">Index.</param>
	/// <param name="Speed">Speed.</param>
	void Move(float index, float Speed){
		myRg.velocity = new Vector2 (Speed * index, myRg.velocity.y); 	// di chuyen nhan vat, index trotimeng khoang (-1.0,1.0)																							// khi index < 0, nhân vật sẽ đi sang bên trái
		if (index > 0)																				//	thực hiện quay nhân vật trái hoặc phải theo index
			transform.eulerAngles = new Vector3 (0, 0, 0);
		else if (index < 0)
			transform.eulerAngles = new Vector3 (0, 180, 0);
		hpBar.rectTransform.eulerAngles = new Vector3 (0, 0, 0);
	}

	/// <summary>
	/// The time last get monster fx.
	/// </summary>
    [HideInInspector]
	public float timeLastGetMonsterFx = 0;
	/// <summary>
	/// Raises the monster fx at hit event.
	/// </summary>
	bool ZeroDame = false;
	void OnMonsterFxAtHit(){
        if (monsterFx == null) return;
		if (Time.time > timeLastGetMonsterFx + monsterFx.Duration) {
			myMC.HpDefault = (int)(myMC.HpDefault * 1.0f - maxHp * monsterFx.HpStealth);
			myMC.MpDefault = (int)(myMC.MpDefault * 1.0f - maxMp * monsterFx.MpStealth);
			monsterFx = null;
			ZeroDame = false;
			return;
		} else {
			myAnim.SetBool ("gethit", true);
			if(monsterFx.Blind == 1)
                ZeroDame = true;
			if (Time.time < timeLastGetMonsterFx + monsterFx.Cooldown) {
				int Direction = transform.localRotation.y == 0 ? -1 : 1;
				myRg.velocity = new Vector2 (Direction * monsterFx.Knock, myRg.velocity.y);
			}
			moveSpeed -= moveSpeed * monsterFx.MoveSlow;
		}
	}

	/// <summary>
	/// Updates the animator.
	/// </summary>
	void UpdateAnimator(){
		int a = speed != 0 ? 1 : 0;
		myAnim.SetBool ("gethit", gethit);
		myAnim.SetInteger ("speed", a);
		myAnim.SetInteger ("attack", attack);
		myAnim.SetInteger ("jumpcount", jumpcount);
		myAnim.SetBool ("inbag", inbag);
	}

	/// <summary>
	/// The blood effect.
	/// </summary>
	public GameObject BloodEffect, BloodScreenEffect;
	/// <summary>
	/// The direction of blood effect.
	/// </summary>
	public Vector3 DirectionBlood;
	/// <summary>
	/// set Animation Bloods enable.
	/// </summary>
	public void bloodEnable(){
        Instantiate(BloodScreenEffect, Camera.main.transform);
		var e = Instantiate (BloodEffect, transform);
		float Scale = Random.Range (0.4f, 1f);
		float PosY = Random.Range (-0.2f, 0.2f);
		e.transform.position += new Vector3 (0, PosY, -1);
		e.transform.localScale = new Vector3(Scale, Scale, Scale);
		e.gameObject.transform.eulerAngles = DirectionBlood;
	}

	/// <summary>
	/// Exits the game.
	/// </summary>
	void ExitGame(){
		Application.Quit ();
	}

	/// <summary>
	/// Die this instance.
	/// </summary>
	void Die(){
        die = true;
        Destroy(GetComponent<mainSkillControll>());
        myAnim.SetBool("die", true);
        resultStatic.win = false;
        MyPlayer.Save();
		Destroy (GetComponent<BoxCollider2D>());
        Invoke("AfterDie",3f);
        //Destroy(this);
	}

    void AfterDie()
    {
        SceneManager.LoadSceneAsync("Result", LoadSceneMode.Additive);
    }

	/// <summary>
	/// The effect of run.
	/// </summary>
	/// <summary>
	/// The footer's Transform of MC.
	/// </summary>
	/// <summary>
	/// Starts the effect run.
	/// </summary>
	public void StartEffect(){
		var e = Instantiate (EffectRun, Footer.position, Quaternion.identity);
		float Scale = Random.Range (1f, 2f);
		float index = useSkill ? 2 : 1;
		e.transform.eulerAngles = transform.eulerAngles;
		e.transform.localScale = new Vector3 (Scale * index, Scale * index, Scale * index);
	}
	/// <summary>
	/// Shakings camera Start.
	/// </summary>
	public void ShakingEnable(){
		CameraSkaing.SetBool ("shake", true);
	}
	/// <summary>
	/// disable Shakings Start.
	/// </summary>
	public void ShakingDisable(){
		CameraSkaing.SetBool ("shake", false);
	}
	/// <summary>
	/// Enable Shaking of attack.
	/// </summary>
	public void ShakingEnable2(){
		CameraSkaing.SetBool ("shake2", true);
	}
	/// <summary>
	/// Disable Shaking of attack.
	/// </summary>
	public void ShakingDisable2(){
		CameraSkaing.SetBool ("shake2", false);
	}
	[HideInInspector]
	public bool stepD = false;
	void DameUp(int indexDamge){
		damgeAttack += indexDamge;
		stepD = true;
	}
	void DameDown(int indexDamge){
		damgeAttack -= indexDamge;
		stepD = false;
	}
    //
    internal void GetScore(int score)
    {
		MyPlayer.Player.Score += score;
        MyPlayer.Save();
	}

    internal void GetCoin(int coin)
    {
        resultStatic.Coin += coin;
        MyPlayer.Player.Coin += coin;
        MyPlayer.Save();
    }

    internal void GetEnergy(int Energy)
    {
        MyPlayer.Player.Energy += Energy;
        MyPlayer.Save();
    }

    internal void GetIAP(int IAP)
    {
        MyPlayer.Player.IAP += IAP;
        MyPlayer.Save();
    }

    void InBag(int Type)
    {
        inbag = Type == 0?false:true;
    }

    GameObject textdamage;
    void TextEffectDamage(int Damage, bool Crist)
    {
        if (Damage == 0)
        {
            textdamage.GetComponent<Text>().text = "Miss";
            return;
        }
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
            textdamage.GetComponent<Text>().fontSize = 18;
        }
    }

}

