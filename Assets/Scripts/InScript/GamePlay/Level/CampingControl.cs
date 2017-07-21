using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[System.Serializable]
public class bg{
	public int[] Ids;
	public string Name;
	public GameObject Content;
}

[RequireComponent(typeof(camping))]
[RequireComponent(typeof(player))]
public class CampingControl : MonoBehaviour {
	//public

	int IdLevel;
	public int inPhase = 0;
	camping yourCam;
	CampaingDb MyCamp;
	public bg[] Background;
	public GameObject[] MainCharacter;
	public GameObject[] Enemys;

	//end public
	//private

	Phase[] myPhase;
	bool update = false;
	bg InBackground;
	float Next;
	BoxCollider2D box;
	//end private
	double TimeEnd;
    Text txtTimeEnd;

    void Awake()
    {
        SceneManager.LoadSceneAsync("CustomShop", LoadSceneMode.Additive);
    }

    void Start()
    {
        GetComponent<camping>().Load();
        GetComponent<player>().Load();
        yourCam = GetComponent<camping>();
        //
        IdLevel = PublicClass.level;
        box = GetComponent<BoxCollider2D>();
        foreach (var i in Background)
        {
            foreach(var j in i.Ids){
                if (j == IdLevel)
                {
                    InBackground = i;
                    break;
                }
            }
        }
        Next = InBackground.Content.GetComponentInChildren<BoxCollider2D>().size.x * InBackground.Content.GetComponentInChildren<BoxCollider2D>().gameObject.transform.localScale.x;
        Vector3 ins = new Vector3(-3, transform.position.y, 20);
        Instantiate(InBackground.Content, ins, Quaternion.identity, transform);
        foreach (var item in yourCam.Player.CampaingDbList)
        {
            if (item.IdCampaing == IdLevel)
            {
                MyCamp = item;
                TimeEnd = MyCamp.TimeEnd;
                break;
            }
        }
        Text t = GameObject.Find("txtLevel").GetComponent<Text>();
        t.text = MyCamp.NameCampaing;
        myPhase = MyCamp.PhaseList;
        txtTimeEnd = GameObject.Find("txtTimeEnd").GetComponent<Text>();
        TimeSpan time = TimeSpan.FromSeconds(TimeEnd);
        txtTimeEnd.text = "TIME: " + time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");
        var max = myPhase[myPhase.Length - 1].EndLockMap / 1980 / 2;
        box.size = new Vector2(myPhase[myPhase.Length - 1].EndLockMap * PublicClass.dpp + 20, box.size.y);
        box.offset = new Vector2(box.size.x / 2, box.offset.y);
        for (int i = 1; i <= max + 1; i++)
        {
            Vector3 insx = new Vector3(i * Next - 3, transform.position.y, 20);
            Instantiate(InBackground.Content, insx, Quaternion.identity, transform);
        }

        foreach (var item in MainCharacter)
        {
            if (item.GetComponent<mainController>().MyID == GetComponent<player>().Player.MC)
            {
                var e = Instantiate(item, new Vector3(MyCamp.StartMC * PublicClass.dpp, 0, 0), Quaternion.identity);
                e.GetComponent<mainController>().myAnim.speed = 0;
                break;
            }
        }
        Invoke("OnStart", 2);
	}

    bool start = false;

    void OnStart()
    {
        start = true;
    }
	void OnUpdate(){
        GameObject main = GameObject.FindGameObjectWithTag("Player");
        main.GetComponent<Animator>().speed = 1;
		update = true;
	}

	Turn[] listEnemy;

	void Update () {
        if (!start) return;
        if (!update)
        {
            if (SceneManager.GetSceneByName("CustomShop").isLoaded == false)
            {
                OnUpdate();
            }
            else return;
        }
        if (endLevel) return;
		if (checkMain()) {
			//
			TimeEnd -= 1 * Time.deltaTime;
			TimeSpan time = TimeSpan.FromSeconds (TimeEnd);
			txtTimeEnd.text = "TIME: " + time.Minutes.ToString ("00") + ":" + time.Seconds.ToString ("00");
			//
            if (TimeEnd <= 100f)
            {
                txtTimeEnd.GetComponent<Animator>().enabled = true;
                txtTimeEnd.color = new Color(1, 0, 0);
            }
			if (TimeEnd <= 0) {
                resultStatic.win = false;
				endInLevel ();
			}
			//
			if (inPhase < myPhase.Length) {
				if (Camera.main.transform.position.x < myPhase [inPhase].StartPhase * PublicClass.dpp) {
					listEnemy = null;
					return;
				} else {
					GetComponent<soundCampaingControll> ().PlayAudioPhaseStart ();
					Camera.main.gameObject.GetComponent<CameraFollow> ().StartLook = myPhase [inPhase].StartLockMap*PublicClass.dpp;
					Camera.main.gameObject.GetComponent<CameraFollow> ().EndLook = myPhase [inPhase].EndLockMap*PublicClass.dpp;
					Camera.main.gameObject.GetComponent<CameraFollow> ().nextPhase = false;
					listEnemy = myPhase [inPhase].TurnList;
					StartCoroutine (OnBorn());
					inPhase++;
				}
			}
			if (inPhase >= myPhase.Length) {
				Camera.main.gameObject.GetComponent<CameraFollow> ().endLevel = true;
                if (countPhase == myPhase.Length && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0)
                {
                    endInLevel();
                }
			}
		}
	}
    void endInLevel()
    {
        endLevel = true;
        GetComponent<testBoss>().BornBoss(GameObject.FindGameObjectWithTag("Player").transform);
        Invoke("AfterEnd", 3f);
	}

    void AfterEnd()
    {
        SceneManager.LoadSceneAsync("Result", LoadSceneMode.Additive);
    }

	bool endLevel= false;
	[HideInInspector]
	public int countPhase = 0;
	IEnumerator OnBorn(){
		if (listEnemy != null) {
			foreach (var j in listEnemy) {
				foreach (var e in Enemys)
					if (j.IdMonster == e.GetComponent<EnemyControll> ().m_nID) {
						yield return new WaitForSeconds (j.Delay / 1000.0f);
						float PosZ = UnityEngine.Random.Range (-1f, 1f);
						Instantiate (e, new Vector3 (j.Xstay * PublicClass.dpp, j.Ystay * PublicClass.dpp, PosZ), Quaternion.identity);
						break;
					}
			}
		}
		countPhase++;
	}

	bool checkMain(){
		return GameObject.FindGameObjectWithTag ("Player");
	}

    void resetScore()
    {
        resultStatic.Bonus = 0;
        resultStatic.Coin = 0;
        resultStatic.Gold= 0;
        resultStatic.Health = 0;
        resultStatic.IAP = 0;
        resultStatic.Video = 0;
    }
}
