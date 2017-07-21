using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Talk{
	public string Avatar;
	public string Content;
	[Range(0,1)]
	[Tooltip("0 : Không\n1 : Có")]
	public int CamMC,CamBoss;
	[Range(0,5)]
	public int CamZoom;
}
/* 1. link hình ảnh avatar
 *  2. Nội dung hội thoại
 *  3. MC/Boss Camera hover
 *  4. Độ zoom của camera
 */

[System.Serializable]
public class CutsceneDb {
	public int IdCutscene;
	public int IdCampaing;
	public string NameCutscene;
	public int Phase;
	[Range(0,1)]
	[Tooltip("0 : Trước\n1 : Sau")]
	public int Poison;
	public Talk[] TalkList;
}

/* 1. id cut scene
 *  2. id campaing trong CampaingDb
 *  3. Tên Cutscene
 *  4. STT của phase trong level
 *  5. vị trí cutscene trước hay sau phase
 */
