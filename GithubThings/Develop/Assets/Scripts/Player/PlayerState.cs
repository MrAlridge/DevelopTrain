/*
* 描述玩家状态的脚本
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static int coinNum;                             // 收集到的硬币数
    public bool[] AbilityPool;
    void Start()
    {
        coinNum = 0;
        AbilityPool = new bool[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
