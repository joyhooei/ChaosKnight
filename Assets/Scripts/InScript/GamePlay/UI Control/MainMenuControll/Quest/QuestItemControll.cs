using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(player))]
[RequireComponent(typeof(monsterCount))]
[RequireComponent(typeof(item))]
[RequireComponent(typeof(quest))]

public class QuestItemControll : MonoBehaviour {
    [HideInInspector]
    public ItemDb itemDb;
    [HideInInspector]
    public int inValue, maxValue, idQuest;
    //
    quest questDb;
    player playerDb;
    monsterCount MonsterCount;
    item ItemDB;
    //
    public Text Name, Description;
    public Image Process;
    public Text Value;
    public Button Reward;

	void Start () {
        //
        GetComponent<player>().Load();
        GetComponent<monsterCount>().Load();
        GetComponent<item>().Load();
        GetComponent<quest>().Load();
        //
        playerDb = GetComponent<player>();
        MonsterCount = GetComponent<monsterCount>();
        ItemDB = GetComponent<item>();
        questDb = GetComponent<quest>();
        //
        Name.text = questDb.Quest.QuestList[idQuest].NameQuest;
        Description.text = questDb.Quest.QuestList[idQuest].Description;
        maxValue = questDb.Quest.QuestList[idQuest].QuestCount;
        inValue = 0;
        if (questDb.Quest.QuestList[idQuest].MonsterTarget != 0)
        {
            foreach (var item in MonsterCount.Player.MonsterCountDbList)
            {
                if (questDb.Quest.QuestList[idQuest].MonsterTarget == item.IdMonster)
                {
                    inValue = item.KillCount;
                    break;
                }
            }
        }
        else
        {
            if (questDb.Quest.QuestList[idQuest].ScoreTarget == 1)
            {
                inValue = playerDb.Player.Score;
            }
            else
            {
                if (questDb.Quest.QuestList[idQuest].UpgradeTarget != 0)
                {
                    foreach (var item in ItemDB.Player.ItemDbList)
                    {
                        if (questDb.Quest.QuestList[idQuest].UpgradeTarget == item.IdItem)
                        {
                            inValue = item.Uplevel;
                        }
                    }
                }
            }
        }
        Process.fillAmount = inValue * 1f / maxValue;
        Value.text = inValue.ToString() + "/" + maxValue.ToString();
        if (questDb.Quest.QuestList[idQuest].Rewarded == 1)
        {
            Reward.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("UI/Quest/QuestCheck");
            Reward.GetComponentInChildren<Text>().text = "";
        }
        if (Process.fillAmount >= 1)
            Reward.interactable = true;
        else 
            Reward.interactable = false;
        Reward.onClick.AddListener(() => Reward_OnClick());
	}

    public void Reward_OnClick()
    {
        //
        if (itemDb.NameItem == "Gold")
        {
            playerDb.Player.Coin += questDb.Quest.QuestList[idQuest].RewardCount;
        } if (itemDb.NameItem == "Energy")
        {
            playerDb.Player.Energy += questDb.Quest.QuestList[idQuest].RewardCount;
        } if (itemDb.NameItem == "IAP")
        {
            playerDb.Player.IAP += questDb.Quest.QuestList[idQuest].RewardCount;
        }
        //
        if (questDb.Quest.QuestList[idQuest].QuestLoop == 1)
        {
            questDb.Quest.QuestList[idQuest].QuestCount += questDb.Quest.QuestList[idQuest].LoopCountUp;
            questDb.Quest.QuestList[idQuest].RewardCount += questDb.Quest.QuestList[idQuest].RewardCountUp;
        }
        else
        {
            questDb.Quest.QuestList[idQuest].Rewarded = 1;
        }
        //
        questDb.Save();
        playerDb.Save();

        PublicClass.indexNotify = 7;
        PublicClass.ScenePrev = "Quest";
        SceneManager.LoadScene("notifyBox", LoadSceneMode.Additive);
        GameObject.Find("Event").GetComponent<QuestLoad>().Load();
    }

}
