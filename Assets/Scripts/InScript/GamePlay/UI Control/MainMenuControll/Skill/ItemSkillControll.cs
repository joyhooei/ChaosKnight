using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkillControll : MonoBehaviour {

    [HideInInspector]
    public SkillDb item;
    [HideInInspector]
    public int IdMC;
    public Image ImageSkill;
    public Text NameSkill;
    public Image Process;
    //
	void Start () {
        Load();
        GetComponent<Button>().onClick.AddListener(Show_OnClick);
	}

    public void Show_OnClick()
    {
        var show = GameObject.FindGameObjectWithTag("ItemSkillShow").GetComponent<ItemSkillShow>();
        show.IdSkill = item.IdSkill;
        show.Load();
    }

    void Load()
    {
        ImageSkill.sprite = Resources.Load<Sprite>("Skills/Icons/" + item.IdSkill.ToString());
        NameSkill.text = item.NameSkill;
        Process.fillAmount = item.UpLevel * 1f / item.UpMax;
        Process.GetComponentInChildren<Text>().text = "+" + item.UpLevel.ToString() + "/" + item.UpMax.ToString();
    }

    public void ReLoad()
    {
        NameSkill.text = item.NameSkill;
        Process.fillAmount = item.UpLevel * 1f / item.UpMax;
        Process.GetComponentInChildren<Text>().text = "+" + item.UpLevel.ToString() + "/" + item.UpMax.ToString();
    }

}
