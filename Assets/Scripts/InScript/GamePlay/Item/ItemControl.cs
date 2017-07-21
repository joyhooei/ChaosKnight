using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(EdgeCollider2D))]


public class ItemControl : MonoBehaviour {
	public int IdItem;
	ItemDb item;
	Rigidbody2D myRg;
    Transform main;
    bool update = false;
	void Start () {
		var g = GameObject.Find ("database").GetComponent<item> ();
		foreach (var i in g.Player.ItemDbList) {
			if (i.IdItem == IdItem) {
				item = new ItemDb (i);
				break;
			}
		}
		GetComponent<CircleCollider2D> ().isTrigger = true;
        GetComponent<EdgeCollider2D>().offset = new Vector2(0, -0.3f);
		myRg = GetComponent<Rigidbody2D> ();
        myRg.bodyType = RigidbodyType2D.Dynamic;
        myRg.freezeRotation = true;
        transform.position += new Vector3(Random.Range(-2f, 2f), 0, -3);
        main = GameObject.FindGameObjectWithTag("Player").transform;
        float time = Random.Range(2f, 3f);
        float localScale = Random.Range(1f, 1.3f);
        transform.localScale = new Vector3(localScale, localScale, localScale);
        Invoke("OnUpdate", time);
	}

    void OnUpdate()
    {
        update = true;
        myRg.mass = 0.01f;
        myRg.gravityScale = 0.1f;
        speed = Random.Range(9f, 12f);
    }

    float f = 0;
    float speed = 12f;

    void Update()
    {
        if (!update) return;
        f = speed*Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, main.position, f);
    }

	void OnTriggerEnter2D(Collider2D cll){
		if (cll.gameObject.layer == LayerMask.NameToLayer ("Player")) {
            //if (!update) return;
			mainController main = cll.gameObject.GetComponent<mainController> ();
            main.GetCoin(1);
			main.myMC.HpDefault += (int)( 
				(1 + item.Uplevel * item.UpAbility) * item.HpIncrease
			);
			//AM increase
			main.myMC.AMDefault += (int)( 
				(1 + item.Uplevel * item.UpAbility) * item.AMIncrease
			);
			//MP increase
			main.myMC.MRDefault += (int)( 
				(1 + item.Uplevel * item.UpAbility) * item.MRIncrease
			);
			//damge increase
			main.damgeAttack += (int)(
				(1 + item.Uplevel * item.UpAbility) * item.DameAtk
			);
			//Crist Up
			main.Crist += (1 + item.Uplevel * item.UpAbility) * item.CristUp;
			main.Vamp += (1 + item.Uplevel * item.UpAbility) * item.Vamp;
			main.myMC.DodgeDefault += (1 + item.Uplevel * item.UpAbility) * item.DodgeIncrease;
			Destroy (gameObject);
		}
	}
    void OnTriggerStay2D(Collider2D cll)
    {
        if (cll.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //if (!update) return;
            mainController main = cll.gameObject.GetComponent<mainController>();
            if (IdItem < 4)
            {
                if (IdItem == 1) main.GetCoin(1); 
                if (IdItem == 2) main.GetIAP(1);
                if (IdItem == 3) main.GetIAP(1);
            }
            else
            {
                main.myMC.HpDefault += (int)(
                    (1 + item.Uplevel * item.UpAbility) * item.HpIncrease
                );
                //AM increase
                main.myMC.AMDefault += (int)(
                    (1 + item.Uplevel * item.UpAbility) * item.AMIncrease
                );
                //MP increase
                main.myMC.MRDefault += (int)(
                    (1 + item.Uplevel * item.UpAbility) * item.MRIncrease
                );
                //damge increase
                main.damgeAttack += (int)(
                    (1 + item.Uplevel * item.UpAbility) * item.DameAtk
                );
                //Crist Up
                main.Crist += (1 + item.Uplevel * item.UpAbility) * item.CristUp;
                main.Vamp += (1 + item.Uplevel * item.UpAbility) * item.Vamp;
                main.myMC.DodgeDefault += (1 + item.Uplevel * item.UpAbility) * item.DodgeIncrease;
            }
            Destroy(gameObject);
        }
    }
}
