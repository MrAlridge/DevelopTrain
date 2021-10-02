using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraplePickup : MonoBehaviour
{
    public GameObject abilitySystem;
    public PlayerState playerState;
    public Collider2D thisCollider;                 //pickUp的碰撞体
    void Start()
    {
        abilitySystem = GameObject.FindWithTag("AbilitySystem");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D result)
    {
        if(result.tag == "Player")
        {
            playerState.AbilityPool[0] = true;
            Destroy(this.gameObject, 0.1f);
        }
    }
}
