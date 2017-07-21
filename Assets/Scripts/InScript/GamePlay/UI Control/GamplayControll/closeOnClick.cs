using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class closeOnClick : MonoBehaviour {

    public void OnClick(string NameScene)
    {
        SceneManager.UnloadSceneAsync(NameScene);
    }
}
