using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grass
{
    public GameObject GrassObject;
    [Range(0f,1f)]
    public float TyLe;
    [Range(0f, 3f)]
    public float MinScale, MaxScale;
}

public class GrassBorn : MonoBehaviour {
    public GameObject LandGround;
    public Grass[] GrassObjects;
    [Range(0, 1)]
    public float Density;
	void Start () {
        float MapSize = LandGround.GetComponent<BoxCollider2D>().size.x *LandGround.transform.localScale.x;
        float maxGlass = MapSize*Density;
        for (int i = 0; i < maxGlass; i++)
        {
            float range = Random.Range(0f, 1f);
            float Tyle = 0;
            foreach (var item in GrassObjects)
            {
                Tyle += item.TyLe;
                if (range <= Tyle)
                {
                    var e = Instantiate(item.GrassObject, transform);
                    var Scale = Random.Range(item.MinScale, item.MaxScale);
                    e.transform.position += new Vector3(i * MapSize / maxGlass,Scale / 30f, -20f);
                    e.transform.localScale = new Vector3(Scale, Scale, Scale);
                    break;
                }
            }
        }
	}
}
