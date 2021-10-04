using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBehaviour : MonoBehaviour
{
    public GameObject target;           // 操作目标
    public Sprite secSprite;            // 使用后的样子
    public bool isUse = false;          // 是否被使用
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D coll)
    {
        if(coll.tag == "Player" && Input.GetButtonDown("Interact") && !isUse)
        {
            target.GetComponent<Collider2D>().isTrigger = true;
            this.GetComponent<SpriteRenderer>().sprite = secSprite;
            isUse = true;
        }
    }
}
