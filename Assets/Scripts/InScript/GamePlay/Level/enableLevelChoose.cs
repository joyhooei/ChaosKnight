using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public enum TypeMap{
	Campaing,Bloodcastle,Defendsebase
}

[RequireComponent(typeof(player))]
public class enableLevelChoose : MonoBehaviour {

	public TypeMap typeMap;
	player Player;
	int maxLevel, maxEnergy;
	void Start () {
        GetComponent<player>().Load();
		Player = GetComponent<player> ();
		switch (typeMap) {
		case TypeMap.Campaing:{
			maxLevel = Player.Player.LvCamping;
				break;
			}
		case TypeMap.Bloodcastle:{
            maxLevel = Player.Player.LvBloodCastle;
				break;
			}
		case TypeMap.Defendsebase:{
            maxLevel = Player.Player.LvDefenseBase;
			break;
			}
        }
    }

    void Update()
    {
        maxEnergy = Player.Player.Energy;
		var Button = GameObject.FindGameObjectsWithTag ("buttonChoose");
		foreach (var item in Button) {
			if (int.Parse (item.name) > maxLevel+1 || item.GetComponent<chooseLevel>().Energy > maxEnergy) {
				item.GetComponent<Button> ().interactable = false;
			}
		}
	}
}
