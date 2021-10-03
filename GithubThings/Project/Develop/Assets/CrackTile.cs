using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackTile : MonoBehaviour
{
    public int crackCountdownNum;               // 碎裂所需时间
    private int countDown;
    IEnumerator CrackCountdown()
    {
        while(countDown > 0)
        {
            yield return new WaitForSeconds(1f);
            countDown--;
        }
        if(countDown == 0)
        {
            Crack();
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            StartCoroutine(CrackCountdown());
        }
    }

    void Crack()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        // 这里做消失时的效果
        this.GetComponent<Collider2D>().enabled = false;
    }
}
