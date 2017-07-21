using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemDbs{
	public ItemDb[] ItemDbList;
}

public class item : MonoBehaviour {
	public ItemDbs Player;
	string dataPath;

	void Start(){
        Load();
	}

    public void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Player.GetType().ToString() + "";
        Player = DataRader.Load<ItemDbs>(dataPath);
    }
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Player.GetType().ToString() +"";
		DataRader.Save (dataPath, Player);
	}
}
