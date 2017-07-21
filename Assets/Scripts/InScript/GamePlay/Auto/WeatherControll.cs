using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weather
{
    public string weatherName;
    public GameObject Content;
}

public class WeatherControll : MonoBehaviour {

    public Weather[] weathers;
	void Start () {
        float i = Random.Range(0f, weathers.Length * 1.0f);
        for (int j = 0; j < weathers.Length; j++)
        {
            if (i>j && i < j + 1)
            {
                Instantiate(weathers[j].Content, Camera.main.transform);
                break;
            }
        }
	}
}
