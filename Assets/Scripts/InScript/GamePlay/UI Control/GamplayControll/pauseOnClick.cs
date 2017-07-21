using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class pauseOnClick : MonoBehaviour {

    Button button;
	void Start () {
        button = GetComponent<Button>();
        button.onClick.AddListener(Pause_OnClick);
	}

    void Pause_OnClick()
    {
        var e = SceneManager.LoadSceneAsync("pauseMenu", LoadSceneMode.Additive);
    }
}
