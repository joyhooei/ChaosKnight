using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(player))]
[RequireComponent(typeof(item))]

public class ItemInventoryLoad : MonoBehaviour
{

    public GameObject ItemInventory;
    public Transform ContentView;
    public int TypeItem;
    //
    item Items;
    player Player;
    //
    void Start()
    {
        GetComponent<item>().Load();
        GetComponent<player>().Load();
        //
        Items = GetComponent<item>();
        Player = GetComponent<player>();
        //
        LoadItem();
    }

    public void LoadItem()
    {
        for (int i = 0; i < ContentView.childCount; i++)
        {
            Destroy(ContentView.GetChild(i).gameObject);
        }
        foreach (var item in Items.Player.ItemDbList)
        {
            //if (item.IdItem < 10) continue;
            if (item.TypeItem == TypeItem)
            {
                var e = Instantiate(ItemInventory, ContentView);
                e.GetComponent<ItemInventoryControll>().IdItem = item.IdItem;
                e.GetComponent<ItemInventoryControll>().IdMC = Player.Player.MC;
            }
        }
    }
    public void ReLoadItem()
    {
        for (int i = 0; i < ContentView.childCount; i++)
        {
            ContentView.GetChild(i).gameObject.GetComponent<ItemInventoryControll>().Load();
        }
    }
}
