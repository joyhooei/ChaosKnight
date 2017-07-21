using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterCountDbs{
	public MonsterCountDb[] MonsterCountDbList;
}

public class monsterCount : MonoBehaviour {

	public MonsterCountDbs Player;
	string dataPath;

    void Start()
    {
        Load();
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}

    internal void Load()
    {
        dataPath = "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString() + "";
        Player = DataRader.Load<MonsterCountDbs>(dataPath);
    }
}
