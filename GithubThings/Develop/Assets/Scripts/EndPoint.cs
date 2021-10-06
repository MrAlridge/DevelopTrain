using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public VideoPlayer vp;
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D coll)
    {
        if(PlayerState.coinNum == 2)
        {
            vp.Play();
        }
    }
}
