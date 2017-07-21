using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterFxDbs{
	public MonsterFxDb[] MonsterFxList;
}
public class monsterFx : MonoBehaviour {

	public MonsterFxDbs Player;
	string dataPath;

    void Start()
    {
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
			Player =	DataRader.Load<MonsterFxDbs> (dataPath);
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
}
