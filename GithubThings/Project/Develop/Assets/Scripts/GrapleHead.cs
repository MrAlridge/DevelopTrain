using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapleHead : MonoBehaviour
{
    public Rigidbody2D rigid;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rigid.velocity.x * rigid.velocity.y > 20f)
        {
            rigid.velocity = new Vector2(1f,1f) * (rigid.velocity.x/rigid.velocity.y);
        }
    }
}
