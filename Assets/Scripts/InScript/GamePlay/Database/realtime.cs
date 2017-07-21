using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class realtime : MonoBehaviour {
    public RealTimeDb RealTime;
    string dataPath;

    void Awake()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + RealTime.GetType().ToString();
       RealTime = DataRader.Load<RealTimeDb>(dataPath);
    }
    public void Save()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + RealTime.GetType().ToString() + "";
        DataRader.Save(dataPath, RealTime);
    }
}
