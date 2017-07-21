using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using UnityEngine;

[RequireComponent(typeof(reward))]
[RequireComponent(typeof(item))]
public class ItemRewardLoad : closeOnClick {

    reward Reward;
    item Items;

    public GameObject ItemReward;
    public Transform ContentView;
    public int indexDay;

	void Start () {
        Items = GetComponent<item>();
        Items.Load();
        DateTime d = GetNistTime();
        switch (d.DayOfWeek)
        {
            case DayOfWeek.Monday:
                indexDay = 1;
                break;
            case DayOfWeek.Tuesday:
                indexDay = 2;
                break;
            case DayOfWeek.Wednesday:
                indexDay = 3;
                break;
            case DayOfWeek.Thursday:
                indexDay = 4;
                break;
            case DayOfWeek.Friday:
                indexDay = 5;
                break;
            case DayOfWeek.Saturday:
                indexDay = 6;
                break;
            case DayOfWeek.Sunday:
                indexDay = 7;
                break;
        }
        //
        Reward = GetComponent<reward>();
        //
        Reward.Load();
        //
        if (indexDay == 1)
        {
            if (Reward.Reward.RewardDbList[0].Status == 0)
            {
                for (int i = 0; i < Reward.Reward.RewardDbList.Length; i++)
                {
                    Reward.Reward.RewardDbList[i].Status = 0;
                }
                Reward.Save();
            }
        }
        //
        foreach (var item in Reward.Reward.RewardDbList)
        {
            var e = Instantiate(ItemReward, ContentView);
            var RewardS = e.GetComponent<ItemRewardControll>();
            RewardS.Reward = item;
            RewardS.IsGetReward = item.IdReward == indexDay;
            foreach (var itemItem in Items.Player.ItemDbList)
            {
                if (item.IdItem == itemItem.IdItem)
                {
                    RewardS.Item = itemItem;
                    break;
                }
            }
        }
	}
	
	void Update () {
		
	}

    public static DateTime GetNistTime()
    {
        try
        {
            var myHttpWebRequest = (HttpWebRequest)WebRequest.Create("http://www.google.com");
            var response = myHttpWebRequest.GetResponse();
            string todaysDates = response.Headers["date"];
            return DateTime.ParseExact(todaysDates,
                                       "ddd, dd MMM yyyy HH:mm:ss 'GMT'",
                                       CultureInfo.InvariantCulture.DateTimeFormat,
                                       DateTimeStyles.AssumeUniversal);
        }
        catch
        {
            return DateTime.Now;
        }
    }
}
