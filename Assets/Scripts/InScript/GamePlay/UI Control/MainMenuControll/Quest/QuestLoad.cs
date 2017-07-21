using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(quest))]
[RequireComponent(typeof(item))]
public class QuestLoad : MonoBehaviour {

    quest Quest;
    item Item;

    public GameObject QuestItem;
    public Transform Content;

	void Start () {
        Load();
	}

    internal void Load()
    {
        GetComponent<quest>().Load();
        GetComponent<item>().Load();
        Quest = GetComponent<quest>();
        Item = GetComponent<item>();
        int i = 0;
        for (int j = 0; j < Content.childCount; j++)
        {
            Destroy(Content.GetChild(j).gameObject);
        }
        foreach (var item in Quest.Quest.QuestList)
        {
            var e = Instantiate(QuestItem, Content);
            e.gameObject.GetComponent<QuestItemControll>().idQuest = i;
            foreach (var item1 in Item.Player.ItemDbList)
            {
                if (item.ItemReward == item1.IdItem)
                {
                    e.gameObject.GetComponent<QuestItemControll>().itemDb = item1;
                    break;
                }
            }
            i += 1;
        }
    }

    public void Close_OnClick()
    {
        SceneManager.UnloadSceneAsync("Quest");
    }

}
