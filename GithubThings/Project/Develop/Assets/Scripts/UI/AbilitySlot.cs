using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySlot : MonoBehaviour
{
    public GameObject[] abilityIcons;
    public PlayerState playerState;

    void Start()
    {
        playerState = GameObject.FindWithTag("Player").GetComponent<PlayerState>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 5;i++)
        {
            if(playerState.AbilityPool[i])
            {
                abilityIcons[i].SetActive(true);
            }
        }
    }
}
