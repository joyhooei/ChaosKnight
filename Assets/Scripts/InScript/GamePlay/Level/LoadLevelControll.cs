using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadLevelControll : MonoBehaviour {
    public GameObject Button;
    public GameObject ViewContent;
    camping Campaing;
    void Start()
    {
        GameObject.Find("database").GetComponent<camping>().Load();
        Campaing = GameObject.Find("database").GetComponent<camping>();
        int i = 1;
        foreach (var item in Campaing.Player.CampaingDbList)
        {
            var e = Instantiate(Button, ViewContent.transform);
            e.gameObject.GetComponentInChildren<Button>().name = i.ToString();
            e.gameObject.GetComponentInChildren<chooseLevel>().Level = i; 
            e.gameObject.GetComponentInChildren<chooseLevel>().Energy = item.EnergyPay;
            e.gameObject.GetComponentInChildren<Text>().text = item.NameCampaing;
            e.gameObject.GetComponentsInChildren<Text>()[1].text = item.EnergyPay.ToString() + " Energy";
            i+=1;
        }
	}
	
	void Update () {
		
	}
}
