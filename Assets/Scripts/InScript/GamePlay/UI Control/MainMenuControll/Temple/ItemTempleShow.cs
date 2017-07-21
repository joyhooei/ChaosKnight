using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(item))]
public class ItemTempleShow : closeOnClick {
    item Items;
    public MCDb Player;
    public Text HPContent, AMContent, MRContent, DamageContent, CristContent, DodgeContent, VampContent;
    public Text Name, Description;
    public Image Viewer;
    public Image ContentView;
    //
    float hp, am, damage, mr, crist, vamp, dodge;
    Animator ViewerAnimator;
    SpriteRenderer ViewerSprite;
	void Start () {
        Player = PublicClass.Player;
        Items = GetComponent<item>();
        Items.Load();
        Name.text = Player.NameMC;
        Description.text = Player.Description;
        //
        ViewerAnimator = Viewer.GetComponent<Animator>();
        ViewerSprite = Viewer.GetComponent<SpriteRenderer>();
        ContentView.sprite = Resources.Load<Sprite>("UI/Items/Icons/ground");
        ViewerAnimator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Player/Animators/"+ Player.IdMC);
        // Load;
        ItemDb item = null;
        hp = Player.HpDefault;
        am = Player.AMDefault;
        mr = Player.MRDefault;
        dodge = Player.DodgeDefault;
        damage = crist = vamp = 0;
        for (int i = 0; i < Items.Player.ItemDbList.Length; i++)
        {
            item = Items.Player.ItemDbList[i];
            if (item.IdMC == Player.IdMC && item.TypeItem != 3 && item.TypeItem != 4)
            {
               hp += (hp * item.HpIncrease);
                am += (am * item.AMIncrease);
                mr += (mr * item.MRIncrease);
                damage += item.DameAtk;
                crist +=  item.CristUp;
                vamp +=  item.Vamp;
                dodge += item.DodgeIncrease;
            }
        }
        //
        HPContent.text = hp.ToString("0.") + " HP";
        AMContent .text = am.ToString("0.") +" AM";
        MRContent.text = mr.ToString("0.") +" MR";
        DamageContent.text = (damage).ToString("0.") + " Damage";
        CristContent.text = (crist*100).ToString("0.") +"% Crist";
        DodgeContent.text = (dodge*100).ToString("0.")+"% Dodge";
        VampContent.text = (vamp*100).ToString("0.")+"% Vamp";
	}

    void FixedUpdate()
    {
        Viewer.sprite = ViewerSprite.sprite;
    }
}
