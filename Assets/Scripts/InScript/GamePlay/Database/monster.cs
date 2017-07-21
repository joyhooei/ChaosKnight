using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterDbs{
	public MonsterDb[] MonsterDbList;
}
public class monster : MonoBehaviour {

	public MonsterDbs Player;
	string dataPath;

    void Start()
    {
        Load();
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
    public void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString() + "";
        Player = DataRader.Load<MonsterDbs>(dataPath);
    }
}
