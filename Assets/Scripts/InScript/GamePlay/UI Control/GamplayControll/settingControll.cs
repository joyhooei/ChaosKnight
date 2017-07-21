using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class settingControll : MonoBehaviour {

    public Slider Music, soundFx;

    void Start()
    {
        Music.value = PublicClass.music;
        soundFx.value = PublicClass.sound;
    }

    public void MusicOnChange()
    {
        PublicClass.music = Music.value;
    }

    public void SoundFxOnChange()
    {
        PublicClass.sound = soundFx.value;
    }

    public void Close()
    {
        if(PublicClass.indexNotify != -1)
            SceneManager.LoadSceneAsync(PublicClass.ScenePrev, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("settingMenu");
    }
}
