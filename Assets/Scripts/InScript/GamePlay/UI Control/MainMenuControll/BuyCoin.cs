using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ItemCoin
{
    public int Coin, IAP, index;
    public GameObject Item;
}

[RequireComponent(typeof(player))]
public class BuyCoin : MonoBehaviour {
    public ItemCoin[] itemsCoin;
    player Player;

	void Start () {
        GetComponent<player>().Load();
        Player = GetComponent<player>();
        OnStart();
	}

    void OnStart()
    {
        foreach (var item in itemsCoin)
        {
            item.Item.GetComponentInChildren<Text>().text = item.Coin.ToString() + " Coin";
            item.Item.GetComponentsInChildren<Text>()[1].text = item.IAP.ToString() + " IAP";
            if (item.IAP <= Player.Player.IAP)
                item.Item.GetComponentInChildren<Button>().interactable = true;
            else
                item.Item.GetComponentInChildren<Button>().interactable = false;
            item.Item.GetComponentInChildren<Button>().onClick.AddListener(() => buy_OnClick(item.index));
        } 
    }

    private void buy_OnClick(int index)
    {
        print(index);
        Player.Player.Coin += itemsCoin[index].Coin;
        Player.Player.IAP -= itemsCoin[index].IAP;
        Player.Save();
        OnStart();
        PublicClass.indexNotify = 5;
        PublicClass.ScenePrev = "BuyCoin";
        SceneManager.LoadScene("notifyBox", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("BuyCoin");
    }

    public void Close_OnClick()
    {
            SceneManager.UnloadSceneAsync("BuyCoin");
    }
}
