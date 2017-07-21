using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListItemControll : MonoBehaviour {
    public GameObject[] Items;
    public void BornItem(int id, Vector3 Position)
    {
        foreach (var item in Items)
        {
            if (item.GetComponent<ItemControl>().IdItem == id)
            {
                Instantiate(item, Position, Quaternion.identity);
                break;
            }
        }
    }
}
