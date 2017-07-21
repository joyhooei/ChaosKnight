using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoThunder : MonoBehaviour {

    [Range(0,1)]
    public float Value;
    public GameObject EffectThunder;
    float NextTime, TimeDelay = 1f;

    SpriteRenderer mySprite;
    float min = 0.1f, max = 0.5f, a;
    int i = 0;
    Animator CameraSkaing;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        CameraSkaing = GameObject.FindGameObjectWithTag("CameraShaking").GetComponent<Animator>();
    }

    void Update()
    {

        if (i == 0)
        {
            a += max * Time.deltaTime;
            if (a >= max) i = 1;
        }
        else
        {
            a -= min * Time.deltaTime; 
            if (a <= min) i = 0;
        }


        mySprite.color = new Color(1, 1, 1, a);
        if (Time.time > NextTime)
        {
            NextTime = Time.time + TimeDelay;
            Born();
        }
	}

    void Born()
    {
        float a = Random.Range(0f, 1f);
        if (a < Value)
        {
            var e = Instantiate(EffectThunder, transform.position, Quaternion.identity);
            e.transform.position -= new Vector3(Random.Range(-5f, 5f), Random.Range(-1f, 1f),0);
            ShakingEnable();
            Invoke("ShakingDisable", 0.33f);
        }
    }
    public void ShakingEnable()
    {
        CameraSkaing.SetBool("shake3", true);
    }
    public void ShakingDisable()
    {
        CameraSkaing.SetBool("shake3", false);
    }

}
