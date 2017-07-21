using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadPopUpControll : MonoBehaviour {

    void Start()
    {
        StartCoroutine(LoadNewScene());
    }
    IEnumerator LoadNewScene()
    {
        AsyncOperation async = null;
        switch(PublicClass.typeLoading){
            case 0:
                async = SceneManager.LoadSceneAsync(PublicClass.SceneNext, LoadSceneMode.Additive);
                break;
            case 1:
                async = SceneManager.LoadSceneAsync(PublicClass.SceneNext);
                break;

        }
        while (!async.isDone)
        {
            yield return new WaitForFixedUpdate ();
        }
        SceneManager.UnloadSceneAsync("LoadPopUp");
    }
}
