using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(skill))]
[RequireComponent(typeof(player))]
public class ItemSkillLoad : closeOnClick{

    public Transform ContentView;
    public GameObject ItemSkill;
    //
    skill Skills;
    player Player;
    //
	void Start () {
        Load();
	}

    public void Load()
    {
        GetComponent<skill>().Load();
        GetComponent<player>().Load();
        //
        Skills = GetComponent<skill>();
        Player = GetComponent<player>();
        //

        for (int i = 0; i < ContentView.childCount; i++)
        {
            Destroy(ContentView.GetChild(i).gameObject);
        }
        foreach (var item in Skills.Skill.SkillList)
        {
            if (item.IdMC == Player.Player.MC)
            {
                var e = Instantiate(ItemSkill, ContentView);
                var controll = e.GetComponent<ItemSkillControll>();
                controll.item = item;
                controll.IdMC = item.IdMC;
                controll.Show_OnClick();
            }
        }
    }

    public void OnLoadItem()
    {
        GetComponent<skill>().Load();
        GetComponent<player>().Load();
        //
        Skills = GetComponent<skill>();
        Player = GetComponent<player>();
        //

        for (int i = 0; i < ContentView.childCount; i++)
        {
            Destroy(ContentView.GetChild(i).gameObject);
        }
        foreach (var item in Skills.Skill.SkillList)
        {
            if (item.IdMC == Player.Player.MC)
            {
                var e = Instantiate(ItemSkill, ContentView);
                var controll = e.GetComponent<ItemSkillControll>();
                controll.item = item;
                controll.IdMC = item.IdMC;
            }
        }
    }
}
