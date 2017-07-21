using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Turn{
	[Range(0,10000)]
	public int Delay;
	public int IdMonster;
	[Range(0,2)]
	[Tooltip(@"0 : Point(xStay,yStay)
1 : Left
2 : Right")]
	public int SideStart;
	public int Xstay;
	public int Ystay;
}
