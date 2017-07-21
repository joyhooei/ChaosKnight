using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SlotItem{
	public int IdItem;
	public int Count;
	public int RateSlot;
}

[System.Serializable]
public class ATK
{
    public int RankATK,RangerATK;
    public float Damage;
    [Range(0,1)]
    public float Rate;
    public int DamageInfu;
    public int Stun;
    public int Knock;
    [Range(0,1)]
    public float MoveSlow;
    public int Blind;
    [Range(0,1)]
    public float HpStealth,MpStealth,FXDuration,RateCrist;
}

[System.Serializable]
public class ESC
{
    [Range(0,1000)]
    public int RangerArlet, RangerESC, DelayESC;
}
[System.Serializable]
public class MonsterDb{
	public int IdMonster;
	public string NameMonster;
	[Range(0,1)]
	[Tooltip("0 : Quái thường\n1 : Trùm")]
	public int MonterType;
	public int Score;
	[Range(0,100)]
	[Tooltip("0 : Không chết\n>1: chết sau N giây")]
	public float TimeLife;
	[Range(0,10)]
	public float Ranger;
	[Range(0,10000)]
	public int HpDefault;
	[Range(0,10)]
	public int MoveSpeed;
	[Range(0,20)]
	public float delayAtk;
	[Range(0,1)]
	[Tooltip("0 : Vật lý\n1: Phép thuật")]
	public int DameType;

    public ATK[] ATKList;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float RateCrist, RateDodge, RateDrop, MCRegen;
    public ESC ESC;
	public SlotItem[] Droplist;
    public int DameATK1, DameATK2, DameATK3;
    public float RateATK1,RateATK2,RateATK3;

	internal MonsterDb(){
	}

	internal MonsterDb(MonsterDb monster){
		this.DameType = monster.DameType;
		this.delayAtk = monster.delayAtk;
		this.Droplist = monster.Droplist;
		this.HpDefault = monster.HpDefault;
		this.IdMonster = monster.IdMonster;
		this.MCRegen = monster.MCRegen;
		this.MonterType = monster.MonterType;
		this.MoveSpeed = monster.MoveSpeed;
		this.NameMonster = monster.NameMonster;
		this.Ranger = monster.Ranger;
		this.RateCrist = monster.RateCrist;
		this.RateDodge = monster.RateDodge;
		this.RateDrop = monster.RateDrop;
		this.Score = monster.Score;
		this.TimeLife = monster.TimeLife;
        this.ESC = monster.ESC;
        this.ATKList = monster.ATKList;
        DameATK1 = monster.DameATK1;
        DameATK2 = monster.DameATK2;
        DameATK3 = monster.DameATK3;
        RateATK1 = monster.RateATK1;
        RateATK2 = monster.RateATK2;
        RateATK3 = monster.RateATK3;
	}
}
/*
+ IdMonster: id phân biệt của các loại Quái có trong table.
+ NameMonster: đoạn text tên của loại Quái.
+ MonsterType: là 1 giá trị để phân biệt kiểu của Quái là loại thường hay là trùm.
+ Score: 1 số điểm sẽ đem về cho player sau khi con quái bị tiêu diệt.
+ TimeLife: thời gian sống của loại Quái này. Sau 1 khoảng thời gian nhất định, được tính bằng giây, nếu không bị player tiêu diệt, con quái tự động trừ Hpdefault của mình về 0 và thực hiện animation Die.
+ Ranger: chỉ số là tầm để thực hiện đòn tấn công của con Quái.
+ Hpdefault: tổng lượng Hp cơ bản lúc đầu mà con Quái có.
+ MoveSpeed: tốc độ di chuyển.
+ DelayAtk: khoảng thời gian tính bằng giây nghỉ giữa 2 lần con Quái thực hiện animation tung đòn tấn công.
+ Dametype: loại hình gây sát thương là vật lý hay phép. 
+ DameAtk1: sát thương của đòn tấn công khi thực hiện animation Atk1. Đơn vị số nguyên. Tối thiểu = 0.
+ DameAtk2: sát thương của đòn tấn công khi thực hiện animation Atk2(nếu có).
+ DameAtk3: sát thương của đòn tấn công khi thực hiện animation Atk3(nếu có)
+ RateAtk1: tỷ lệ sẽ thực hiện animation Atk1 trong 100% tổng tỷ lệ của Atk1 + Atk2 + Atk3.
+ RateAtk2: tỷ lệ sẽ thực hiện animation Atk1 trong 100% tổng tỷ lệ của Atk1 + Atk2 + Atk3.
+ RateAtk3: tỷ lệ sẽ thực hiện animation Atk1 trong 100% tổng tỷ lệ của Atk1 + Atk2 + Atk3.
+ RateCrist: tỷ lệ sẽ xuất hiện sát thương chí mạng. Sát thương chí mạng là sát thương của đòn tấn công sẽ được tăng x2.
+ RateDodge: một tỷ lệ / 100% sẽ thực hiện animation Dodge(nếu có) – hành vi né đòn mỗi khi bị MC tấn công trúng.
+ RateDrop: tỷ lệ sẽ rơi ra item sau khi chết hay không / 100%.
+ MCRegen
*/
