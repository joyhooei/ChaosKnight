using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseItemInventory : closeOnClick {
    public ItemInventoryLoad show;
    public void Weapon_onClick()
    {
        show.TypeItem = 0;
        show.LoadItem();
    }
    public void Armor_onClick()
    {
        show.TypeItem = 1;
        show.LoadItem();
    }
    public void Pedal_onClick()
    {
        show.TypeItem = 2;
        show.LoadItem();
    }
}
