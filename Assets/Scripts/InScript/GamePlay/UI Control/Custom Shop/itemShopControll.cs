using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class itemShopControll : MonoBehaviour
{

    public GameObject LeftItem, ContentItem, RightItem;
    public ItemDb myItem;
    mainController main;

    void Start()
    {
        var e = ContentItem.GetComponentsInChildren<Text>();
        foreach (var item in e)
        {
            if (item.gameObject.name.Contains("Name"))
            {
                item.text = myItem.NameItem;
            }
            if (item.gameObject.name.Contains("tion"))
            {
                item.text = myItem.Description;
            }
        }

        GameObject[] g = SceneManager.GetSceneByName("PlayCampaing").GetRootGameObjects();
        foreach (var item in g)
        {
            if (item.tag.Contains("Player"))
            {
                main = item.GetComponent<mainController>();
                break;
            }
        }
        RightItem.GetComponentInChildren<Text>().text = myItem.UpCoin.ToString();
        RightItem.GetComponentInChildren<Button>().onClick.AddListener(BuyItem);
        LeftItem.GetComponentInChildren<Image>().sprite = (Sprite)Resources.Load<Sprite>("UI/Shop/" + myItem.TypeItem.ToString());
        OnLoad();

    }

    void OnLoad()
    {
        GameObject.Find("textGold").GetComponent<Text>().text = main.MyPlayer.Player.Coin.ToString();
        if (RightItem.GetComponentInChildren<Button>().interactable)
            RightItem.GetComponentInChildren<Button>().interactable
                = main.MyPlayer.Player.Coin >= myItem.UpCoin ? true : false;
    }

    void LateUpdate()
    {
        OnLoad();
    }


    void BuyItem()
    {
        main.GetComponent<mainItemControll>().GetItem(myItem);
        RightItem.GetComponentInChildren<Button>().interactable = false;
    }
}
