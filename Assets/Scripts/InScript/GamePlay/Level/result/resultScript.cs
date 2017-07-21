using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class resultScript : MonoBehaviour {
    AudioSource audioPlay;
	public Text txtGold, txtScore, txtBonus, txtVideo, txtHearth, txtIAP;
	void Start () {
        audioPlay = GetComponent<AudioSource>();
        audioPlay.volume = PublicClass.sound;
        txtGold.text = resultStatic.Gold.ToString();
        txtBonus.text = resultStatic.Bonus.ToString();
        txtHearth.text = resultStatic.Health.ToString();
        txtIAP.text = resultStatic.IAP.ToString();
        txtScore.text = resultStatic.Coin.ToString();
        txtVideo.text = resultStatic.Video.ToString();

        if (resultStatic.win)
        {
            GameObject gamein = new GameObject();
            var e = SceneManager.GetSceneByName("PlayCampaing").GetRootGameObjects();
            foreach (var item in e)
            {
                if (item.gameObject.tag == "Player")
                {
                    gamein = item;
                    break;
                }
            }
            if (gamein.GetComponent<player>().Player.LvCamping < PublicClass.level)
            {
                gamein.GetComponent<player>().Player.LvCamping += 1;
            }
            gamein.GetComponent<player>().Save();
            AudioClip audios = Resources.Load("Sounds/UI/Result_complete") as AudioClip;
            audioPlay.PlayOneShot(audios);
        }
        else
        {
            AudioClip audios = Resources.Load("Sounds/UI/Result_defeat") as AudioClip;
            audioPlay.PlayOneShot(audios);
        }
	}

	public void ReplayClick(int Type){
		switch(Type){
            case 1:
                SceneManager.LoadScene ("LoadScreenCampaing");
                break;
            case 2:
                SceneManager.LoadScene ("LoadSceneCampaing");
                break;
            case 3:
                SceneManager.LoadScene("LoadSceneCampaing");
                break;
        }
	}

    public void SelectLevel()
    {
        SceneManager.LoadScene("ChooseLevelCampaing");
    }
    public void SelectMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void PlayVideo()
    {
        SceneManager.LoadScene("");
    }


    public static bool win { get; set; }
}
