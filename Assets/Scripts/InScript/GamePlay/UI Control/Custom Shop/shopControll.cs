using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class shopControll : MonoBehaviour {

    public GameObject ItemView, Top,Content;
    item Items;
    mainController main;
    float TimeToClose = 10;

	void Start () {
        GameObject[] g = SceneManager.GetSceneByName("PlayCampaing").GetRootGameObjects();
        int i = 0;
        foreach (var item in g)
        {
            if(i>=2) break;
            if (item.name == "database")
            {
                Items = item.GetComponent<item>();
                i+=1;
                continue;
            }
            if(item.tag.Contains("Player")){
                main = item.GetComponent<mainController>();
                i+=1;
                continue;
            }
        }

        foreach (var item in Items.Player.ItemDbList)
        {
            if (item.TypeItem == 4)
            {
                var e = Instantiate(ItemView, Content.transform);
                e.GetComponent<itemShopControll>().myItem = new ItemDb(item);
            }
        }
        main.MyPlayer.Load();
        Top.GetComponentInChildren<Text>().text = main.MyPlayer.Player.Coin.ToString() + " Gold";
	}


    void Update()
    {
        TimeToClose -= 1 * Time.deltaTime;
        Top.GetComponentsInChildren<Text>()[1].text ="Close in: "+ ((int)TimeToClose).ToString();
        if (TimeToClose <= 0) CloseOnClick();
    }

    public void CloseOnClick()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("CustomShop");
    }
}
