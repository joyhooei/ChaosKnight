using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class CutsceneDbs{
	public CutsceneDb[] CutsceneDbList;
}
public class cutscene : MonoBehaviour {

	public CutsceneDbs Player;
	string dataPath;

    void Start()
    {
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
			Player = DataRader.Load<CutsceneDbs> (dataPath);
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
}
