using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSkillShow : MonoBehaviour {

    GameObject MainEvent;
    skill Items;
    //
    public player Player { get; set; }
    //
    [HideInInspector]
    public int IdSkill;
    public Image ImageSkill;
    public Text NameSkill,Process,CoinUpdate, Description;
    public Button UpdateButton;
	void Awake () {
        MainEvent = GameObject.Find("Event");
        UpdateButton.onClick.AddListener(Update_Onclick);
	}

    private void Update_Onclick()
    {
        Items = MainEvent.GetComponent<skill>();
        foreach (var item in Items.Skill.SkillList)
        {
            if (item.IdSkill == IdSkill)
            {
                Player.Player.Coin -= item.CoinUp;
                item.CoinUp += (int)(item.CoinUp * item.CoinupInCrease);
                item.AMUp += item.AMUp * item.AblityUp;
                item.CoolDown -= item.CoolDown * item.AblityUp;
                item.CristUp += item.CristUp * item.AblityUp;
                item.DameSkill += item.DameSkill * item.AblityUp;
                item.DameUp += item.DameUp * item.AblityUp;
                item.DodgeUp += item.DodgeUp * item.AblityUp;
                item.HpRegen += item.HpRegen * item.AblityUp;
                item.MRUp += item.MRUp * item.AblityUp;
                item.UpLevel += 1;
                Items.Save();
                Player.Save();
                MainEvent.GetComponent<ItemSkillLoad>().OnLoadItem();
                Load();
            }
        }
    }

    public void Load()
    {
        Player = MainEvent.GetComponent<player>();
        Items = MainEvent.GetComponent<skill>();
        foreach (var item in Items.Skill.SkillList)
        {
            if (item.IdSkill == IdSkill)
            {
                ImageSkill.sprite = Resources.Load<Sprite>("Skills/Icons/" + item.IdSkill.ToString());
                NameSkill.text = item.NameSkill;
                Process.text = "Level: +" + item.UpLevel.ToString() + "/" + item.UpMax.ToString();
                CoinUpdate.text = item.CoinUp.ToString() + " Coin to up level " + (item.UpLevel + 1).ToString();
                Description.text = OnLoadDescription(item);
                if (item.UpLevel >= item.UpMax)
                {
                    UpdateButton.GetComponentInChildren<Text>().text = "In Max Level Upgrade";
                    UpdateButton.interactable = false;
                    CoinUpdate.text = "In Max Level Upgrade";
                }
                else
                {
                    if (Player.Player.Coin < item.CoinUp)
                    {
                        UpdateButton.interactable = false;
                        UpdateButton.GetComponentInChildren<Text>().text = "Not enough coin";
                    }
                    else
                    {
                        UpdateButton.interactable = true;
                        UpdateButton.GetComponentInChildren<Text>().text = "Upgrade";
                    }
                }
            }
        }
    }


    string OnLoadDescription( SkillDb SkillDB)
    {
        string ret = "";
        switch (SkillDB.IdSkill)
        {
            case 1:
                ret = "War falls from the sky, dealing " + SkillDB.DameSkill.ToString("0.") + " damage and repelling everything around " + SkillDB.CoolDown.ToString("0.0") + " of cooldown.";
                break;
            case 2:
                ret = "Anything will be shackled by chains in " + SkillDB.Duration + " seconds. It takes " + SkillDB.CoolDown.ToString("0.0") + " seconds to cooldown.";
                break;
            case 3:
                ret = "War rushes forward to 400 distance and deals " + SkillDB.DameSkill.ToString("0.") + " damage to alltargets. Cooldown in " + SkillDB.CoolDown.ToString("0.0") + " seconds.";
                break;
            case 4:
                ret = "War invoking the destruction horse appears, war will be increased to " + SkillDB.CristUp*100 + "% in Cristical chance and " + SkillDB.DodgeUp*100 + "% in Dodge." + SkillDB.CoolDown.ToString("0.0") + "seconds of cooldown.";
                break;
            case 5:
                ret= "Deadly Light appears causing " + SkillDB.DameSkill.ToString("0.") + " damage to all targets and regen " + SkillDB.HpRegen*100 + "% Hit Points for Death. Need " + SkillDB.CoolDown.ToString("0.0") + " seconds to cooldown." ;
                break;
            case 6:
                ret = "Soul barrier causes all targets to be delayed by " + SkillDB.Slow*100 + "% speed in" + SkillDB.Duration + "second. It takes cooldown from" + SkillDB.CoolDown.ToString("0.0") + "seconds." ;
                break;
            case 7:
                ret ="Death's swing his Scythe while jumping forward and dealing" + SkillDB.DameSkill.ToString("0.") + "damage to all targets. Need" + SkillDB.CoolDown.ToString("0.0") + "seconds to cooldown.";
                break;
            case 8:
                ret = "Death is increased by" +SkillDB.DameUp*100 + "% of basic damage and" +SkillDB.SpeedUp +"% of speed move. Skill cooldown for" + SkillDB.CoolDown.ToString("0.0") + "seconds.";
                break;
            case 9:
                ret = "The Famile's whip makes" + SkillDB.DameSkill.ToString("0.") + "damage for 2 times for every target. Cooldown after" + SkillDB.CoolDown.ToString("0.0") + "seconds.";
                break;
            case 10:
                break;
            case 11:
                break;
            case 12:
                break;
            case 13:
                break;
            case 14:
                break;
            case 15:
                break;
            case 16:
                break;
        }
        return ret;
    }

}
