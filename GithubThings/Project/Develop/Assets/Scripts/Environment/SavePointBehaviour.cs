using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePointBehaviour : MonoBehaviour
{
    private Vector2 thisSavePointPosition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        thisSavePointPosition = new Vector2(transform.position.x, transform.position.y);
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            if(BasicPlayerBehaviour.GetPlayerSavePoint() != thisSavePointPosition)
            {
                BasicPlayerBehaviour.SetPlayerSavePoint(thisSavePointPosition);
                // 这里记得加点特效
                
            }
        }
    }
}
