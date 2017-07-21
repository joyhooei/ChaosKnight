using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuControll : MonoBehaviour {

    void Start()
    {
        Time.timeScale = 0;
    }

    public void Continue_OnClick()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("pauseMenu");
    }

    public void Restart_OnClick()
    {
        PublicClass.ScenePrev = "pauseMenu";
        PublicClass.SceneNext = "LoadScreenCampaing";
        PublicClass.indexNotify = 1;
        SceneManager.UnloadSceneAsync("pauseMenu");
        SceneManager.LoadSceneAsync("notifyBox", LoadSceneMode.Additive);
    }

    public void OtherLevel_OnClick()
    {
        PublicClass.ScenePrev = "pauseMenu";
        PublicClass.SceneNext = "ChooseLevelCampaing";
        PublicClass.indexNotify = 2;
        SceneManager.UnloadSceneAsync("pauseMenu");
        SceneManager.LoadSceneAsync("notifyBox", LoadSceneMode.Additive);
    }

    public void Setting_OnClick()
    {
        PublicClass.ScenePrev = "pauseMenu";
        SceneManager.UnloadSceneAsync("pauseMenu");
        SceneManager.LoadSceneAsync("settingMenu",LoadSceneMode.Additive);
    }

    public void Main_Onclick()
    {
        PublicClass.ScenePrev = "pauseMenu";
        PublicClass.SceneNext = "mainMenu";
        PublicClass.indexNotify = 4;
        SceneManager.UnloadSceneAsync("pauseMenu");
        SceneManager.LoadSceneAsync("notifyBox", LoadSceneMode.Additive);
    }
}
