using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class blood : MonoBehaviour {
	
	public BloodCastleDb Player;
	string dataPath;

	void Start(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
			Player = DataRader.Load<BloodCastleDb> (dataPath);
	}

	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
}
