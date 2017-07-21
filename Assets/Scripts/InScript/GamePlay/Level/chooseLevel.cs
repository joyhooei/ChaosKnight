using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class chooseLevel : MonoBehaviour {
	public int Level;
    public int Energy;
	Button button;
    player McPlayer;
	void Start(){
        McPlayer = GameObject.Find("database").GetComponent<player>();
		button = GetComponent<Button> ();
		button.onClick.AddListener (ChooseLevel);
	}

	void ChooseLevel(){
		PublicClass.level = Level;
        McPlayer.Player.Energy -= Energy;
        McPlayer.Save();
		SceneManager.LoadScene ("LoadScreenCampaing");
	}

}
