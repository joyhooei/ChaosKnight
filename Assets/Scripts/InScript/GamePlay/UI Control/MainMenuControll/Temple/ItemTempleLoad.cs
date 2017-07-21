using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(mc))]
public class ItemTempleLoad : closeOnClick {
    [HideInInspector]
    public mc Mc;
    //
    public GameObject ItemTemple;
    public Transform ContentView;
    //
	void Start () {
        Load();
	}

    internal void Load()
    {
        GetComponent<mc>().Load();
        Mc = GetComponent<mc>();
        //
        for (int j = 0; j < ContentView.childCount; j++)
        {
            Destroy(ContentView.GetChild(j).gameObject);
        }
        int i = 0;
        foreach (var item in Mc.Player.MC)
        {
            var e = Instantiate(ItemTemple, ContentView);
            e.gameObject.GetComponent<ItemTempleControll>().MCDB = item;
            e.gameObject.GetComponent<ItemTempleControll>().index = i;
            i += 1;
        }
    }
}
