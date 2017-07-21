using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestDbs{
	public QuestDb[] QuestList;
}
public class quest : MonoBehaviour {

	public QuestDbs Quest;
	string dataPath;

    void Start()
    {
        Load();
	}

    public void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Quest.GetType().ToString();
        Quest = DataRader.Load<QuestDbs>(dataPath);
    }
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Quest.GetType().ToString() +"";
		DataRader.Save (dataPath, Quest);
	}
}
