using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(player))]
public class BuyEnergy : MonoBehaviour {
    public Text TextView;
    public Button Buy, Close;
    //public Button Add, Minus,
    public int minEnergy;
    int inEnergy, hasEnergy;
    int maxIAP;
    player playerDb;

    void Start()
    {
        playerDb = GetComponent<player>();
        //Add.onClick.AddListener(Add_Onclick);
        //Minus.onClick.AddListener(Minus_Onclick);
        Buy.onClick.AddListener(Buy_OnClick);
        Close.onClick.AddListener(Close_OnClick);
        Invoke("OnStart",0.1f);
    }

    private void Close_OnClick()
    {
           SceneManager.UnloadSceneAsync("BuyEnergy");
    }

    void OnStart()
    {
        maxIAP = playerDb.Player.IAP;
        hasEnergy = playerDb.Player.Energy;
        inEnergy = minEnergy;
    }

    private void Buy_OnClick()
    {
        playerDb.Player.Energy += inEnergy;
        playerDb.Player.IAP -= inEnergy;
        playerDb.Save();
        OnStart();
        PublicClass.indexNotify = 6;
        PublicClass.ScenePrev = "BuyEnergy";
        SceneManager.LoadScene("notifyBox", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("BuyEnergy");
    }

    private void Minus_Onclick()
    {
        inEnergy -= minEnergy;
    }

    private void Add_Onclick()
    {
        inEnergy += minEnergy;
    }

    void Update()
    {
        TextView.text = inEnergy.ToString();
        /*
        if ( inEnergy >= maxIAP )
        {
            Add.interactable = false;
        }
        else
        {
            Add.interactable = true;
        }
        if (inEnergy <= minEnergy || maxIAP < minEnergy)
        {
            Minus.interactable = false;
        }
        else
        {
            Minus.interactable = true;
        }
         * */
        if (inEnergy < minEnergy || maxIAP < minEnergy )
        {
            Buy.interactable = false;
        }
        else
        {
            Buy.interactable = true;
        }
    }
}
