using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Parallax : MonoBehaviour
{
    Camera main;
    [Range(0, 10)]
    public float speed;
    Vector3 oldV;
    Rigidbody2D rg;
    int indexspeed;
    bool update = false;

    void Start()
    {
        main = Camera.main;
        rg = GetComponent<Rigidbody2D>();
        Invoke("OnUpdate", 1f);
    }

    void OnUpdate()
    {
        update = true;
    }
    void Update()
    {
        if (!update)
            return;
        
        float Direction = oldV.x - main.transform.position.x;
        if (Direction > 0) indexspeed = -1;
        else indexspeed = 1;
        move = Direction != 0;
        if (move)
            rg.velocity = new Vector2(speed* indexspeed, rg.velocity.y);
        else
            rg.velocity = Vector2.zero;
    }

    bool move = false;

    void LateUpdate()
    {
        oldV = main.transform.position;
    }
}
