using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RewardDbs
{
    public RewardDb[] RewardDbList;
}

public class reward : MonoBehaviour {
    public RewardDbs Reward;
    string dataPath;

    void Start()
    {
        Load();
    }
    public void Save()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Reward.GetType().ToString() + "";
        DataRader.Save(dataPath, Reward);
    }

    internal void Load()
    {
        dataPath = DataRader.GetPath() + "com." + Application.companyName + "." + Application.productName + "." + Reward.GetType().ToString() + "";
        Reward = DataRader.Load<RewardDbs>(dataPath);
    }

    /*
    public static DateTime GetNistTime()
    {
        var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.microsoft.com");
        var response = myHttpWebRequest.GetResponse();
        string todaysDates = response.Headers["date"];
        return DateTime.ParseExact(todaysDates,
                                   "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                   CultureInfo.InvariantCulture.DateTimeFormat,
                                   DateTimeStyles.AssumeUniversal);
    }
     * */
	
}
