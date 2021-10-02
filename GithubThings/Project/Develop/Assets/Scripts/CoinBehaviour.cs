/*
* 这个是实现金币的脚本
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private Collider2D coinCollider;

    void Start()
    {
        coinCollider = this.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D result)
    {
        if(result.gameObject.tag == "Player")
        {
            // 这里应该播放音效
            PlayerState.coinNum += 1;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
