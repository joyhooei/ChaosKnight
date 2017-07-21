using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Phase{
	[Range(0,10000)]
	public int StartPhase;
	[Range(0,10000)]
	public int StartLockMap;
	[Range(0,10000)]
	public int EndLockMap;
	public Turn[] TurnList;
}
