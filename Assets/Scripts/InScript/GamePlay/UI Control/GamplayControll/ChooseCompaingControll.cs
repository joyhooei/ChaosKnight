using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseCompaingControll : MonoBehaviour {

    public void Back_OnClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    public void BuyEnergy_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        SceneManager.LoadSceneAsync("BuyEnergy", LoadSceneMode.Additive);
    }
}
