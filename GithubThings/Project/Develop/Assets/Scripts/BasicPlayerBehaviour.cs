using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBehaviour : MonoBehaviour
{
    // -----Component Varieties-----
    public Collider2D playerCollider;
    public Rigidbody2D playerRigidbody;
    // -----State Data-----
    private bool isGround;                      // 是否着地
    private bool isMoveable = true;             // 是否可动
    public float playerSpeed;                   // 初始移动速度
    public float playerJumpForce;               // 初始跳跃力度
    private float playerJumpCount;              // 玩家已跳跃次数
    // -----Input Data-----
    private float horizontalInputValue;         // 水平轴输入
    private float verticalInputValue;           // 垂直轴输入
    private bool jumpInputState;                // 跳跃键输入

    void Start()
    {
        
    }

    void Update()
    {
        GetInput();
        if(isMoveable)                          // 移动操作逻辑
        {
            if(horizontalInputValue != 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x + horizontalInputValue * playerSpeed, playerRigidbody.velocity.y);
            }
            if(isGround || playerJumpCount < 2)
            {
                if(jumpInputState)
                {
                    Jump();
                }
            }
        }
    }

    void Jump()                                 // 跳跃函数
    {
        playerRigidbody.AddForce(new Vector2(0, playerJumpForce));
    }

    void GetInput()                             // 输入数据更新
    {
        horizontalInputValue = Input.GetAxis("Horizontal");
        verticalInputValue = Input.GetAxis("Vertical");
        jumpInputState = Input.GetButtonDown("Jump");
    }

    void SetIsGround(bool setBool)              // 封装变值方法
    {
        if(isGround != setBool)
            isGround = setBool;
    }

}
