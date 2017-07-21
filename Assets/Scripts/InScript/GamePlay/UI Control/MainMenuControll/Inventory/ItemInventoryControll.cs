using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(item))]
public class ItemInventoryControll : MonoBehaviour {

    item Items;
    [HideInInspector]
    public int IdItem, IdMC;
    public Image ImageView;
    public Button ButtonShow;
    public Text TextView;
    public Toggle CheckView;
    public Text textName;

	void Start () {
        GetComponent<item>().Load();
        //
        Items = GetComponent<item>();
        //
        foreach (var item in Items.Player.ItemDbList)
        {
            if (item.IdItem == IdItem)
            {
                TextView.text ="Level: +" + item.Uplevel.ToString();
                textName.text = item.NameItem;
                if (item.IdMC == IdMC)
                {
                    CheckView.isOn = true;
                    ButtonShow_OnClick();
                }
                else
                    CheckView.isOn = false;
                break;
            }
        }
        ButtonShow.onClick.AddListener(ButtonShow_OnClick);
        ImageView.sprite = Resources.Load<Sprite>("Items/Icons/" + IdItem.ToString());
	}

    public void Load()
    {
        Items.Load(); 
        foreach (var item in Items.Player.ItemDbList)
        {
            if (item.IdItem == IdItem)
            {
                TextView.text = "Level: +" + item.Uplevel.ToString();
                break;
            }
        }
    }

    private void ButtonShow_OnClick()
    {
        var show = GameObject.FindGameObjectWithTag("ItemInventoryShow").GetComponent<ItemInventoryShow>();
        show.IdItem = IdItem;
        show.Load();

    }
}
