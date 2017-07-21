using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(player))]
public class ItemRewardControll : MonoBehaviour {
    public RewardDb Reward;
    [HideInInspector]
    public bool IsGetReward;
    //
    player Player;
    public ItemDb Item;
    //
    public Button ShowReward;
    public Image Content;
    public Text TextName;
    
    //
	void Start () {
        Player = GetComponent<player>();
        Player.Load();
        //
        switch (Reward.IdReward)
        {
            case 1:
                TextName.text = "Monday";
                break;
            case 2:
                TextName.text = "Tuesday";
                break;
            case 3:
                TextName.text = "Wednesday";
                break;
            case 4:
                TextName.text = "Thusday";
                break;
            case 5:
                TextName.text = "Friday";
                break;
            case 6:
                TextName.text = "Saturday";
                break;
            case 7:
                TextName.text = "Sunday";
                break;
        }
        Content.sprite = Resources.Load<Sprite>("UI/Rewards/Icons/" + Reward.IdReward);
        //Content.color = new Color(0.5f, 0.5f, 0.5f, 1);
        if (IsGetReward )
        {
            ShowReward_Onclick();
            if (Reward.Status == 0)
            {
                Content.color = new Color(1f, 1f, 1f, 1f);
            }
        }
        ShowReward.interactable = true;
        ShowReward.onClick.AddListener(ShowReward_Onclick);
	}

    private void ShowReward_Onclick()
    {
        var g = GameObject.FindGameObjectWithTag("ItemReward").GetComponent<ItemRewardShow>() ;
        g.Item = Item;
        g.IsGetReward = IsGetReward;
        g.IdReward = Reward.IdReward;
        g.Load();
    }
}
