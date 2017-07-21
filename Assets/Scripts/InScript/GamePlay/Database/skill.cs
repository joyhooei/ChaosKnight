using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SkillDbs{
	public SkillDb[] SkillList;
}

public class skill : MonoBehaviour {
	public SkillDbs Skill;
	string dataPath;

    void Start()
    {
        Load();
	}
	public void Save(){
		dataPath =DataRader.GetPath () + "com." + Application.companyName + "." + Application.productName +"."+ Skill.GetType().ToString() +"";
		DataRader.Save (dataPath, Skill);
	}

    internal void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Skill.GetType().ToString() + "";
        Skill = DataRader.Load<SkillDbs>(dataPath);
        
    }
}
