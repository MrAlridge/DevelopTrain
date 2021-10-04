using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{
    public GameObject player;
    private Vector3 lastPosition;           // 平台上一帧的位置
    private Vector3 playerLastPosition;
    private Vector3 offset, playerOffset;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        lastPosition = transform.position;
    }

    void Update()
    {
        transform.position = this.GetComponentInParent<Transform>().position;
        offset = transform.position - lastPosition;
        playerOffset = transform.position - player.transform.position;
        lastPosition = transform.position;
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            player.transform.SetParent(this.transform);
            //playerLastPosition = player.transform.position;
        }
    }
    /*
    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player")
        {
            player.transform.position = transform.position - playerOffset;
            Debug.Log("Yes!");
        }

    }
    */
    void OnTriggerExit2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            player.transform.SetParent(null);
            //playerLastPosition = Vector3.zero;
        }
    }
}
