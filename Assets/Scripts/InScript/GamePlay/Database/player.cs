using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {

	public PlayerDb Player;
	string dataPath;

    void Start()
    {
        Load();
	}

    public void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString();
        Player = DataRader.Load<PlayerDb>(dataPath);
    }
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString();
		DataRader.Save (dataPath, Player);
	}
}
