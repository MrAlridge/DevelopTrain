/*
* 这个是控制玩家行为的脚本，基本包括了玩家本身的各种动作
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBehaviour : MonoBehaviour
{
    // -----Component Varieties-----
    public Collider2D playerCollider;
    public Rigidbody2D playerRigidbody;
    public Animator playerAnimator;
    public SpriteRenderer playerRender;
    // -----State Data-----
    public bool isGround;                      // 是否着地
    private bool isMoveable = true;             // 是否可动
    public float playerSpeed;                   // 初始移动速度
    public float playerJumpForce;               // 初始跳跃力度
    public float playerJumpCount;              // 玩家已跳跃次数
    private static Vector2 playerPostition;     // 玩家位置
    private static Vector2 mousePosition;       // 鼠标位置
    // -----Input Data-----
    private float horizontalInputValue;         // 水平轴输入
    private float verticalInputValue;           // 垂直轴输入
    private bool jumpInputState;                // 跳跃键输入
    // -----Other Thing-----
    // Ray2D groundDetectRay = new Ray2D(new Vector2(0,0), new Vector2(0, -1));                      // 触地检测射线
    public LayerMask ground;
    RaycastHit2D[] castResults;           // 返回的结果
    public float playerSpeedLimit;              // 速度上限

    void Start()
    {
        
    }

    void Update()
    {
        playerPostition = new Vector2(this.transform.position.x, this.transform.position.y);
        GroundScan();
        SpeedLimit();
        UpdateRender();
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
        playerJumpCount++;
    }

    void GetInput()                             // 输入数据更新
    {
        horizontalInputValue = Input.GetAxis("Horizontal");
        verticalInputValue = Input.GetAxis("Vertical");
        jumpInputState = Input.GetKeyDown(KeyCode.Space);
        mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    void UpdateRender()                         // 更新状态和渲染
    {
        if(Mathf.Abs(playerRigidbody.velocity.x) > 2)
        {
            playerAnimator.SetBool("isWalking", true);
        }else{
            playerAnimator.SetBool("isWalking", false);
        }
        if(playerRigidbody.velocity.x > 0.25)
        {
            if(playerRender.flipX != false)
            {
                playerRender.flipX = false;
            }
        }else{
            if(playerRigidbody.velocity.x < 0.25)
            {
                if(playerRender.flipX != true)
                {
                    playerRender.flipX = true;
                }
            }
        }
        if(playerRigidbody.velocity.y > 0)
        {
            playerAnimator.SetInteger("jumpDir", 1);
        }else{
            if(playerRigidbody.velocity.y < -0)
            {
                playerAnimator.SetInteger("jumpDir", -1);
            }else{
                playerAnimator.SetInteger("jumpDir", 0);
            }
        }
    }

    void SetIsGround(bool setBool)              // 封装变值方法
    {
        if(isGround != setBool)
            isGround = setBool;
    }

    void SpeedLimit()                           // 限制玩家最大速度
    {
        if (Mathf.Abs(playerRigidbody.velocity.x) > playerSpeedLimit)
        {
            if(playerRigidbody.velocity.x > 0)
            {
                playerRigidbody.velocity = new Vector2(playerSpeedLimit, playerRigidbody.velocity.y);
            }else{
                playerRigidbody.velocity = new Vector2(-playerSpeedLimit, playerRigidbody.velocity.y);
            }
        }
    }
    void GroundScan()                           // 触地检测
    {
        bool groundDetect = false;
        if (playerCollider.IsTouchingLayers(ground))
        {
            groundDetect = true;
            //Debug.Log("Touch!");
        }

        if (groundDetect)
        {
            SetIsGround(true);
            playerJumpCount = 0;
            playerAnimator.SetBool("isGround", true);
        }else{
            SetIsGround(false);
            playerAnimator.SetBool("isGround", false);
        }
    }

    // -----封装方法-----
    public static Vector2 GetPlayerPosition()
    {
        return playerPostition;
    }

    public static Vector2 GetMousePosition()
    {
        return mousePosition;
    }
}
