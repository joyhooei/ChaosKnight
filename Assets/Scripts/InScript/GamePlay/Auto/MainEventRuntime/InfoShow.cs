using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(player))]
public class InfoShow : MonoBehaviour {
    PlayerDb playerDb;
    public Text txtCoin, txtIAP;
    public bool coinShow, IAPShow;
	void Start () {
        playerDb = GetComponent<player>().Player;
	}

    void Update()
    {
        if (SceneManager.sceneCount > 1)
        {
            GetComponent<player>().Load();
            playerDb = GetComponent<player>().Player;
        }
    }
	
	void FixedUpdate () {
        if(coinShow) txtCoin.text = playerDb.Coin.ToString() +" Coin";
        if(IAPShow) txtIAP.text = playerDb.IAP.ToString() +" IAP";
	}
}
