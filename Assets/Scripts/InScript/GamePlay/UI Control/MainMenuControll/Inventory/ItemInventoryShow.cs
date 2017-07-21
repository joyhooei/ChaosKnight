using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class ChiSo
{
    public Image icon;
    public Text text;
}
[RequireComponent(typeof(item))]
[RequireComponent(typeof(player))]
public class ItemInventoryShow : MonoBehaviour {
    item Items;
    public player Player;
    //
    public Text NameItem, LevelItem;
    public Image ImageView;
    public Button ButtonUpgrade;
    //
    public ChiSo chiSo1, chiSo2, chiSo3;
    //
    public int IdItem;
    //
	void Start () {
        //
        ButtonUpgrade.onClick.AddListener(ButtonUpgrade_OnClick);
        Load();
	}
    //
    public void Load()
    {
        //
        GetComponent<item>().Load();
        GetComponent<player>().Load();
        //
        Player = GetComponent<player>();
        Items = GetComponent<item>();
        //
        foreach (var item in Items.Player.ItemDbList)
        {
            if (item.IdItem == IdItem)
            {
                NameItem.text = item.NameItem;
                LevelItem.text = "Level: +" + item.Uplevel.ToString();
                ImageView.sprite = Resources.Load<Sprite>("UI/Items/Icons/" + IdItem.ToString());
                if (item.Uplevel >= item.UpMax)
                {
                    ButtonUpgrade.gameObject.GetComponentInChildren<Text>().text = "In Max Level Upgrade";
                    ButtonUpgrade.interactable = false;
                }
                else
                {
                    ButtonUpgrade.gameObject.GetComponentInChildren<Text>().text = item.UpCoin.ToString() +" Coin";
                    if (Player.Player.Coin < item.UpCoin)
                    {
                        ButtonUpgrade.interactable = false;
                    }
                    else
                    {
                        ButtonUpgrade.interactable = true;
                    }
                }
                switch (item.TypeItem)
                {
                    case 0:
                        chiSo1.text.text = item.DameAtk.ToString("0.") +"Damage+";
                        chiSo2.text.text = (item.CristUp*100).ToString("0.") + "%Crist";
                        chiSo3.text.text = (item.Vamp*100).ToString("0.") +"%Vamp";
                        chiSo1.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/DameAtk");
                        chiSo2.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/CristUp");
                        chiSo3.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/Vamp");
                        break;
                    case 1:
                        chiSo1.text.text = item.AMIncrease.ToString("0.") + " AM+";
                        chiSo2.text.text = (item.HpIncrease).ToString("0.") +" HP+";
                        chiSo3.text.text = (item.DodgeIncrease * 100).ToString("0.") + "%Dodge";
                        chiSo1.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/AMIncrease");
                        chiSo2.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/HpIncrease");
                        chiSo3.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/DodgeIncrease");
                        break;
                    case 2:
                        chiSo1.text.text = (item.MRIncrease).ToString("0.") + "MR+";
                        chiSo2.text.text = (item.HpIncrease).ToString("0.") + "HP+";
                        chiSo3.text.text = (item.DodgeIncrease * 100).ToString("0.") + "%Dodge";
                        chiSo1.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/MRIncrease");
                        chiSo2.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/HpIncrease");
                        chiSo3.icon.sprite = Resources.Load<Sprite>("UI/Items/Icons/DodgeIncrease");
                        break;
                }
                break;
            }
        }
    }

    private void ButtonUpgrade_OnClick()
    {
        foreach (var item in Items.Player.ItemDbList)
        {
            if (item.IdItem == IdItem)
            {
                Player.Player.Coin -= item.UpCoin;
                item.Uplevel += 1;
                item.UpCoin = (int)(item.UpCoin + item.UpCoin * item.UpCoinIncrease);
                item.CristUp += item.CristUp * item.UpAbility;
                item.Vamp += item.Vamp * item.UpAbility;
                item.DameAtk +=(int)( item.DameAtk * item.UpAbility);
                item.HpIncrease +=  item.HpIncrease * item.UpAbility;
                item.MRIncrease +=  item.MRIncrease * item.UpAbility;
                item.AMIncrease +=  item.AMIncrease * item.UpAbility;
                item.DodgeIncrease = item.DodgeIncrease * item.DodgeIncrease;
                break;
            }
        }
        Player.Save();
        Items.Save();
        /*
        PublicClass.indexNotify = 8;
        PublicClass.ScenePrev = "Inventory";
        SceneManager.LoadScene("notifyBox", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("Inventory");
         */
        Load();
        GameObject.FindGameObjectWithTag("ItemInventoryContent").GetComponentInParent<ItemInventoryLoad>().ReLoadItem();
    }
}
