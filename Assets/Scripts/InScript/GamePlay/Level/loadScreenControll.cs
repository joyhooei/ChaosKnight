using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadScreenControll : MonoBehaviour {
    public Text Status;
    public string status;
	float index = 0;
	Image ProcessBar;
	void Start () {
		ProcessBar = GameObject.Find ("ProcessBar").GetComponent<Image>();
		StartCoroutine(LoadNewScene());
	}

    void FixedUpdate()
    {
        Status.text = status;
    }

	IEnumerator LoadNewScene() {
        var async = SceneManager.LoadSceneAsync("PlayCampaing");
        while (!async.isDone)
        {
            status = "Loading ....";
            ProcessBar.fillAmount = async.progress;
            yield return null;
        }
	}

}
