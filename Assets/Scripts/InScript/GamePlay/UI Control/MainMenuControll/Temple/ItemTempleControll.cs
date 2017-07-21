using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(player))]
[RequireComponent(typeof(item))]
public class ItemTempleControll : MonoBehaviour {
    [HideInInspector]
    public MCDb MCDB;
    [HideInInspector]
    public item Item;
    player Player;
    //
    public Text TextName;
    public Image Viewer;
    public Button BuyButton;
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Item_OnClick);
        Load();
	}

    private void BuyMC_OnClick()
    {
        Player.Player.Coin -= MCDB.CoinUnlock;
        Player.Player.IAP -= MCDB.IAPUnlock;
        var item = GameObject.Find("Event").GetComponent<ItemTempleLoad>();
        item.Mc.Player.MC[MCDB.IdMC - 1].CoinUnlock = 0;
        item.Mc.Player.MC[MCDB.IdMC - 1].IAPUnlock = 0;
        item.Mc.Save();
        item.Load();
    }

    private void EquipMC_OnClick(int index)
    {
        Player.Player.MC = index;
        Player.Save();
        GameObject.Find("Event").GetComponent<ItemTempleLoad>().Load();
    }

    private void Load()
    {
        GetComponent<player>().Load();
        GetComponent<item>().Load();
        //
        Item = GetComponent<item>();
        Player = GetComponent<player>();
        //
        TextName.text = MCDB.NameMC; 
        Viewer.sprite = Resources.Load<Sprite>("Player/Default/" + MCDB.IdMC.ToString());
    
        if (MCDB.CoinUnlock != 0 || MCDB.IAPUnlock != 0)
        {
            if (MCDB.CoinUnlock != 0)
            {
                BuyButton.GetComponentInChildren<Text>().text = MCDB.CoinUnlock.ToString() + " Coin";
                if (Player.Player.Coin < MCDB.CoinUnlock)
                    BuyButton.interactable = false;
                else
                {
                    BuyButton.interactable = true;
                }
            }
            else
            {
                BuyButton.GetComponentInChildren<Text>().text = MCDB.IAPUnlock.ToString() + " IAP";
                if (Player.Player.IAP < MCDB.IAPUnlock)
                    BuyButton.interactable = false;
                else
                    BuyButton.interactable = true;
            }
            BuyButton.onClick.AddListener(BuyMC_OnClick);
        }
        else
        {
            if (MCDB.IdMC == Player.Player.MC)
            {
                BuyButton.interactable = false;
                BuyButton.GetComponentInChildren<Text>().text = "Equipped";
            }
            else
            {
                BuyButton.interactable = true;
                BuyButton.GetComponentInChildren<Text>().text = "Equip";
                BuyButton.onClick.AddListener(() => EquipMC_OnClick(MCDB.IdMC));
            }
        }
    }

    [HideInInspector]
    public int index;
    private void Item_OnClick()
    {
        PublicClass.Player = MCDB;
        SceneManager.LoadSceneAsync("TempleShow", LoadSceneMode.Additive);
    }
}
