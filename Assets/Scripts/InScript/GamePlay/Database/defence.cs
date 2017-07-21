using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class defence : MonoBehaviour {

	public DefenseBaseDb Player;
	string dataPath;

    void Start()
    {
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		Player = DataRader.Load<DefenseBaseDb> (dataPath);
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
}
