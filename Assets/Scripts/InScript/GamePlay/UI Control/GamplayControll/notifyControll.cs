using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class notifyControll : MonoBehaviour {

    public Text Content;
    public Button BtnYes, BtnNo;
    Notify notication;
    
    void Start()
    {
        notication = GameObject.Find("NotiFyLoad").GetComponent<Notify>();
        foreach (var item in notication.Notication.NotifyDbList)
        {
            if (PublicClass.indexNotify == item.Id)
            {
                if (item.Type == 1)
                {
                    BtnYes.GetComponentInChildren<Text>().text = "Yes";
                    BtnNo.GetComponentInChildren<Text>().text = "No";
                    BtnYes.onClick.AddListener(Yes);
                    BtnNo.onClick.AddListener(No);
                }
                if (item.Type == 2)
                {
                    BtnNo.GetComponentInChildren<Text>().text = "Ok";
                    Destroy(BtnYes.gameObject);
                    BtnNo.onClick.AddListener(No);
                } 
                if (item.Type == 3)
                {
                    BtnNo.GetComponentInChildren<Text>().text = "Ok";
                    Destroy(BtnYes.gameObject);
                    BtnNo.onClick.AddListener(Ok);
                } if (item.Type == 4)
                {
                    BtnNo.GetComponentInChildren<Text>().text = "Ok";
                    Destroy(BtnYes.gameObject);
                    BtnNo.onClick.AddListener(Ok);
                    Content.text = PublicClass.stringNotify;
                    break;
                }
                Content.text = item.Content;
                break;
            }
        }
    }

    private void Ok()
    {
        SceneManager.UnloadSceneAsync("notifyBox");
    }
    public void Yes()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(PublicClass.SceneNext);
    }

    public void No()
    {
        SceneManager.LoadSceneAsync(PublicClass.ScenePrev, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("notifyBox");
    }
}
