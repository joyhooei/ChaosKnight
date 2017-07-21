using UnityEngine;
using System.Collections;

[System.Serializable]
public class QuestDb
{
	public int IdQuest;
	public string NameQuest;
	public int ItemReward;
	[Range(0,10)]
	public int RewardCount;
	[Range(0,100)]
	public int QuestCount;
	[Tooltip("Id MonsterDb")]
	public int MonsterTarget;
	[Range(0,1000000)]
	[Tooltip("Min Score of Player")]
	public int ScoreTarget;
	[Tooltip("0 : none\n>1 : Id Item")]
	public int UpgradeTarget;
	[TextArea()]
	public string Description;
	[Range(0,1)]
	[Tooltip("0 : Có\n1 : Không")]
	public int QuestLoop;
	[Range(0,1000000)]
	public int LoopCountUp;
	[Range(0,1000000)]
	public int RewardCountUp;
    [Range(0,1)]
    public int Rewarded;
}

