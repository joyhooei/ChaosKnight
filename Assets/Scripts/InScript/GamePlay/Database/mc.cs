using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MCDbs{
	public MCDb[] MC;
}
public class mc : MonoBehaviour {

	public MCDbs Player;
	string dataPath;

    void Start()
    {
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
			Player = DataRader.Load<MCDbs> (dataPath);
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}

    internal void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString() + "";
        Player = DataRader.Load<MCDbs>(dataPath);
    }
}
