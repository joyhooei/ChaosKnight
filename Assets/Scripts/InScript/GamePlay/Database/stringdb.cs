using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    vi,en,fr
}

[System.Serializable]
public class StringDBs
{
    public StringDb[] StringDbList;
}

public class stringdb : MonoBehaviour {

    public StringDBs StringDbs;
    string dataPath;

    public stringdb()
    {
        StringDbs = new StringDBs();
    }

    public void Save()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + StringDbs.GetType().ToString() + "";
        DataRader.Save(dataPath, StringDbs);
    }

    internal void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + StringDbs.GetType().ToString() + "";
        StringDbs = DataRader.Load<StringDBs>(dataPath);
    }

    public static string getString(string name, Language type, StringDBs stringDbs)
    {
        name = name.ToLower();
        foreach (var e in stringDbs.StringDbList)
        {
            if (e.name.Contains(name))
            {
                switch (type)
                {
                    case Language.vi:
                        return e.vi;
                    case Language.en:
                        return e.en;
                    case Language.fr:
                        return e.fr;
                }
            }
        }
        return name;
    }

}
