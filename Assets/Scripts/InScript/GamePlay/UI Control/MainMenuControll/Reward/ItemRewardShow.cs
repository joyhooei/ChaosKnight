using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(player))]
public class ItemRewardShow : MonoBehaviour {
    reward Reward;
    player Player;
    public ItemDb Item;
    //
    public int IdReward;
    public Button GetReward;
    public Image Content;
    public bool IsGetReward;


    void Awake()
    {
        Reward = GameObject.Find("Event").GetComponent<reward>();
        Player = GetComponent<player>();
    }

    void Start()
    {
        GetReward.interactable = false;
        Content.sprite = Resources.Load<Sprite>("UI/Rewards/Content/-1");
	}

    internal void Load()
    {
        Player.Load();
        foreach (var item in Reward.Reward.RewardDbList)
        {
            if (item.IdReward == IdReward)
            {
                Content.sprite = Resources.Load<Sprite>("UI/Rewards/Content/" + IdReward);
                GetReward.onClick.AddListener(GetReward_OnClick);
                GetReward.GetComponentInChildren<Text>().text = item.Count.ToString() + " " + Item.NameItem;
                GetReward.interactable = true;
                if (!IsGetReward || item.Status == 1)
                {
                    GetReward.interactable = false;
                    if (item.Status == 1)
                    {
                        GetReward.GetComponentInChildren<Text>().text = "Received";
                    }
                    else
                    {
                        GetReward.GetComponentInChildren<Text>().text = "Can't receive";
                    }
                }
            }
        }
    }

    private void GetReward_OnClick()
    {
        foreach (var item in Reward.Reward.RewardDbList)
        {
            if (item.IdReward == IdReward)
            {
                //
                if (Item.NameItem == "Gold")
                {
                    Player.Player.Coin += item.Count;
                } if (Item.NameItem == "Energy")
                {
                    Player.Player.Energy += item.Count;
                } if (Item.NameItem == "IAP")
                {
                    Player.Player.IAP += item.Count;
                }
                item.Status = 1;
                PublicClass.stringNotify = "Received " + item.Count.ToString() + " " + Item.NameItem;
                PublicClass.indexNotify = 8;
                SceneManager.LoadSceneAsync("notifyBox", LoadSceneMode.Additive);
                Player.Save();
                break;
            }
        }
        Reward.Save();
        Load();
    }

}
