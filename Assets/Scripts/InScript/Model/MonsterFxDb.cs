using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterFxDb {
	public int IdMonsterFX;
	public int IdMonster;
	public string NameFX;
	[Range(0,100)]
	public float Cooldown;
	[Range(0,10)]
	public float Duration;
	[Range(0,1)][Tooltip("0 : Không\n1 : Có")]
	public int Stun;
	public int Knock;
	public float MoveSlow;
	[Range(0,1)][Tooltip("0 : Không\n1 : Có")]
	public int  Blind;
	[Range(0,10)]
	public int Ranger;
	[Range(0,1)][Tooltip("Đơn vị : %")]
	public float HpStealth;
	[Range(0,1)][Tooltip("Đơn vị : %")]
	public float MpStealth;
}
