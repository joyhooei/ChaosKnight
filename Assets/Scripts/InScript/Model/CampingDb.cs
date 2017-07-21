using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CampaingDb {
	public int IdCampaing;
	public string NameCampaing;
	[Range(0,10000)]
	public int EnergyPay;
	[Range(0,10000)]
	public int TimeEnd;
	[Range(0,10000)]
	public int StartMC;
	public Phase[] PhaseList;

	/* 1. id của campaing
	 *  2. Tên campaing
	 * 	3. Tiền trả để chơi level
	 *  4.	Vị trí của quái xuất hiện so với O(0,0)
	 * 	5. Danh sách modeBlood Castle
	 */
}
