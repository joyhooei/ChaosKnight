using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Notifys
{
    public NotifyDb[] NotifyDbList;
}

public class Notify : MonoBehaviour {

    public Notifys Notication;
    string dataPath;

    void Start()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Notication.GetType().ToString() + "";
        Notication = DataRader.Load<Notifys>(dataPath);
    }

    public void Save()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Notication.GetType().ToString() + "";
        DataRader.Save(dataPath, Notication);
    }
}
