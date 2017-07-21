using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class CampaingDbs{
	public CampaingDb[] CampaingDbList;
}

public class camping : MonoBehaviour {

	public CampaingDbs Player;
	string dataPath;

	void Start(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
        Player =	DataRader.Load<CampaingDbs> (dataPath);
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}

    internal void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString() + "";
        Player = DataRader.Load<CampaingDbs>(dataPath);
    }
}
