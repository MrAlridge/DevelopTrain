using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            if(Input.GetAxis("Vertical") < 0)
            {
                this.GetComponent<Collider2D>().isTrigger = true;
                Invoke("ResetThis", 1f);
            }
        }
    }

    void ResetThis()
    {
        this.GetComponent<Collider2D>().isTrigger = false;
    }
}
