using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButtonOnClick : MonoBehaviour {
    public void Setting_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.indexNotify = -1;
        PublicClass.SceneNext = "settingMenu";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void More_OnClick()
    {
        Application.OpenURL(@"https://play.google.com/store/apps/dev?id=8988959007894361415");
    }
    public void Energy_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "BuyEnergy";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void Coin_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "BuyCoin";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void IAP_OnClick()
    {

    }
    public void FreeCoin_OnClick()
    {

    }
    public void DailyGift_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "Daily";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void Quest_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "Quest";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void PlayCampaing_OnClick()
    {
        PublicClass.SceneNext = "ChooseLevelCampaing";
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.typeLoading = 1;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void BaseDefense_OnClick()
    {

    }
    public void BloodCastle_OnClick()
    {

    }
    public void Rank_OnClick()
    {

    }
    public void Skill_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "Skill";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void Inventory_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "Inventory";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void Temple_OnClick()
    {
        PublicClass.ScenePrev = SceneManager.GetActiveScene().name;
        PublicClass.SceneNext = "Temple";
        PublicClass.typeLoading = 0;
        SceneManager.LoadScene("LoadPopUp", LoadSceneMode.Additive);
    }
    public void ExitGame_OnClick(){

    }
}
