using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ItemRegen{
    public TimeSpan inTimeDelay;
    public int maxTimeDelay;
    public int maxItemIn;
    public Text textViewRegen, textView;
    public Button button;
    public bool Enable, Show;
}

[RequireComponent(typeof(player))]
public class ReplayProperties : MonoBehaviour {
    PlayerDb McPlayer;
    public ItemRegen Energy, Blood, Defense;
	void Start () {
        GetComponent<player>().Load();
         TimeStatic.realTimeStatic  = GetComponent<realtime>().RealTime;
         McPlayer = GetComponent<player>().Player;
         GetData(Energy, 1);
         GetData(Blood, 2);
         GetData(Defense, 3);
         if (TimeStatic.realTimeStatic.TimeBlood == 0)
             Blood.Enable = false;
         if (TimeStatic.realTimeStatic.TimeDefense == 0)
             Defense.Enable = false;
         StartCoroutine("DatabaseUpdate");
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Item">ItemRegen Data</param>
    /// <param name="Type">
    /// <value>1-Energy</value>
    /// <value>2-Blood</value>
    /// <value>2-Defense</value>
    /// </param>
    void GetData(ItemRegen Item, int Type)
    {
        TimeSpan nows = DateTime.Now - TimeStatic.realTimeStatic.LastTimeUpdate;
        int Time = 0;
        switch (Type)
        {
            case 1:
                Time = TimeStatic.realTimeStatic.TimeEnergy;
                break;
            case 2:
                Time = TimeStatic.realTimeStatic.TimeBlood;
                break;
            case 3:
                Time = TimeStatic.realTimeStatic.TimeDefense;
                break;
            default :
                break;
        }
        int second =(int)( Math.Abs(-Time + nows.TotalSeconds));
        TimeSpan now = new TimeSpan(0, 0, 0, second);
        int itemCount =(int)now.TotalSeconds / Item.maxTimeDelay;
        Item.inTimeDelay = new TimeSpan(0, 0, 0, (int)(now.TotalSeconds - itemCount * Item.maxTimeDelay),0);
        switch (Type)
        {
            case 1:
                McPlayer.Energy += itemCount;
                break;
            default:
                break;
        }
    }

    float x, y, z; //x-Energy,y-Coin,z-IAP
    void Update()
    {
        if (SceneManager.sceneCount > 1)
        {
            GetComponent<player>().Load();
            McPlayer = GetComponent<player>().Player;
        }
        if (Energy.Enable)
        {
            if (McPlayer.Energy >= Energy.maxItemIn)
                Energy.Enable = false;
            else
                Energy.Enable = true;
            x += 1 * Time.deltaTime;
            if (x >= 1)
            {
                Energy.inTimeDelay -= new TimeSpan(0, 0, 0, 1);
                x = 0;
            }
            if (Energy.button != null) Energy.button.interactable = false;
            if (Energy.inTimeDelay.TotalSeconds <= 0)
                Add(new Vector3(1, 0, 0));
        }
        else
            if(Energy.Show)
            Energy.textViewRegen.text = "";
        if (Blood.Enable)
        {
            y += 1 * Time.deltaTime;
            if (y >= 1)
            {
                Blood.inTimeDelay -= new TimeSpan(0, 0, 0, 1);
                y = 0;
            }
            if (Blood.Show) Blood.button.interactable = false;
            if (Blood.inTimeDelay.TotalSeconds <= 0)
                Add(new Vector3(0, 1, 0));
            if (Blood.inTimeDelay.TotalSeconds <= 0)
                Add(new Vector3(1, 0, 0));
            }
        else
            if(Blood.Show)
                Blood.textViewRegen.text = "";
        if (Defense.Enable)
        {
            z += 1 * Time.deltaTime;
            if (z >= 1)
            {
                Defense.inTimeDelay -= new TimeSpan(0, 0, 0, 1);
                z = 0;
            }
            if (Defense.Show) Defense.button.interactable = false;
            if (Defense.inTimeDelay.TotalSeconds <= 0)
                Add(new Vector3(0, 0, 1));
        }
        else
            if (Defense.Show)
            Defense.textViewRegen.text = "";
    }

    void Add(Vector3 item)
    {
        McPlayer.Energy += (int)item.x;
        if (item.x == 1)
            Energy.inTimeDelay = new TimeSpan(0,0,0, Energy.maxTimeDelay);
        if (item.y == 1 && Blood.Show)
        {
            Blood.button.interactable = true;
            Blood.Enable = false;
        }
        if (item.z == 1 && Defense.Show)
        {
            Defense.button.interactable = true;
            Defense.Enable = false;
        }
        Save();
    }
	
	void FixedUpdate () {
        if (Blood.inTimeDelay.TotalSeconds <= 0 && Blood.Show) 
        { 
            Blood.inTimeDelay = new TimeSpan(0, 0, 0, 0);
            Blood.Enable = false;
            Blood.button.interactable = true;
        }
        if (Defense.inTimeDelay.TotalSeconds <= 0 && Defense.Show)
        {
            Defense.inTimeDelay = new TimeSpan(0, 0, 0, 0);
            Defense.Enable = false;
            Defense.button.interactable = true;
        }
        //Energy
        if (Energy.Show)
        {
            Energy.textView.text = McPlayer.Energy.ToString() + "/" + Energy.maxItemIn.ToString() + " Energy";
            Energy.textViewRegen.text = Energy.inTimeDelay.Minutes.ToString("00") + ":" + Energy.inTimeDelay.Seconds.ToString("00");
        }
        if (Blood.Show)
        {
            Blood.textViewRegen.text = Blood.inTimeDelay.Minutes.ToString("00") + ":" + Blood.inTimeDelay.Seconds.ToString("00");
        }
        if (Defense.Show)
        {
            Defense.textViewRegen.text = Defense.inTimeDelay.Minutes.ToString("00") + ":" + Defense.inTimeDelay.Seconds.ToString("00");
        }
	}

    IEnumerator DatabaseUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            Save();
            yield return null;
        }
    }

    public bool Save()
    {
        TimeStatic.realTimeStatic.TimeEnergy = (int)Energy.inTimeDelay.TotalSeconds;
        TimeStatic.realTimeStatic.TimeBlood = (int)Blood.inTimeDelay.TotalSeconds;
        TimeStatic.realTimeStatic.TimeDefense = (int)Defense.inTimeDelay.TotalSeconds;
        TimeStatic.realTimeStatic.LastTimeUpdate = DateTime.Now;
        GetComponent<realtime>().RealTime = TimeStatic.realTimeStatic;
        GetComponent<player>().Player = McPlayer;
        GetComponent<realtime>().Save();
        GetComponent<player>().Save();
        return true;
    }

    void OnApplicationQuit()
    {
        Save();
    }
}

