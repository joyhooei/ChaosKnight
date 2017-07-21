using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainItemControll : MonoBehaviour {
	item Items;
	[HideInInspector]
	List<ItemDb> myItem;
	mainController main;

	void Awake(){
		myItem = new List<ItemDb> ();
	}

	void Start () {
		GameObject g = GameObject.Find ("database");
		Items = g.GetComponent<item> ();
		Invoke ("OnStart", 0.8f);
	}

	void OnStart(){
		main = GameObject.FindGameObjectWithTag ("Player").GetComponent<mainController> ();
		int myId = main.myMC.IdMC;
		foreach (var item in Items.Player.ItemDbList) {
			if (item.IdMC == myId && item.Status == 1) {
				myItem.Add (new ItemDb (item));
			}
		}

		foreach (var item in myItem) {
			//Hp increase
            main.myMC.HpDefault += (int)(main.myMC.HpDefault*item.HpIncrease);
			//AM increase
			main.myMC.AMDefault += (int)(main.myMC.AMDefault*item.AMIncrease);
			//MP increase
			main.myMC.MRDefault +=(int)(main.myMC.MRDefault* item.MRIncrease);
			//damge increase
            main.damgeAttack += item.DameAtk;
			//Crist Up
			main.Crist +=  (int)(main.Crist * item.CristUp);
			main.Vamp +=  (int)(main.Vamp * item.Vamp);
			main.myMC.DodgeDefault += (1 + item.Uplevel * item.UpAbility) * item.DodgeIncrease;
		}
	}

    public void GetItem(ItemDb item)
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
        main.MyPlayer.Player.Coin -= item.UpCoin;
        main.MyPlayer.Save();
    }
}