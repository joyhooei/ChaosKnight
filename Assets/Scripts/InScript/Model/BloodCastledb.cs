using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BloodCastleDb {
	[Range(0,1000000)]
	public int EnergyPay;
	[Range(0,999999)]
	public int TimeEnd;
	[Range(0,1)]
	[Tooltip("Đơn vị : %")]
	public float HpDefaultUp,DameAtk1Up,DameAtk2Up,DameAtk3Up;
	public float StartMC;
	public Phase[] PhaseList;

	/*	1.	số enegy phải trả để chơi level
	 * 	2. Thồiời gian tồn tại của level, tính bằng giây
	 * 	3. % máu tăng, dame tăng của quái trong level
	 * 	4.	Vị trí của quái xuất hiện so với O(0,0)
	 * 	5. Danh sách modeBlood Castle
	 */ 
}
