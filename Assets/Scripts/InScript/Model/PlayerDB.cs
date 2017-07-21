using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerDb {
	[Range(0,1000000)]
	public int Coin;
    [Range(0,100)]
    public int Energy;
    [Range(0, 500000)]
    public int IAP;
    [Range(1,4)]
    public int MC;
    [Range(0,100)]
    public int LvCamping, LvBloodCastle, LvDefenseBase;
    public int Score;
}
