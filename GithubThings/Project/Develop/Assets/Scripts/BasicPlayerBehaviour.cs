using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerBehaviour : MonoBehaviour
{
    // -----Component Varieties-----
    public Collider2D playerCollider;
    public Rigidbody2D playerRigidbody;
    //public Animator playerAnimator;
    public SpriteRenderer playerRender;
    // -----State Data-----
    public bool isGround;                      // 是否着地
    private bool isMoveable = true;             // 是否可动
    public float playerSpeed;                   // 初始移动速度
    public float playerJumpForce;               // 初始跳跃力度
    public float playerJumpCount;              // 玩家已跳跃次数
    public LayerMask ground;
    // -----Input Data-----
    private float horizontalInputValue;         // 水平轴输入
    private float verticalInputValue;           // 垂直轴输入
    private bool jumpInputState;                // 跳跃键输入
    // -----Other Thing-----

    void Start()
    {

    }

    void Update()
    {
        GroundScan();
        UpdateRender();
        GetInput();
        if (isMoveable)                          // 移动操作逻辑
        {
            if (horizontalInputValue != 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x + horizontalInputValue * playerSpeed, playerRigidbody.velocity.y);
            }
            if (isGround || playerJumpCount < 2)
            {
                if (jumpInputState)
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
    }

    void UpdateRender()
    {
        if (playerRigidbody.velocity.x > 0)
        {
            if (playerRender.flipX != false)
            {
                playerRender.flipX = false;
            }
        }
        else
        {
            if (playerRigidbody.velocity.x < 0)
            {
                if (playerRender.flipX != true)
                {
                    playerRender.flipX = true;
                }
            }
        }
    }

    void SetIsGround(bool setBool)              // 封装变值方法
    {
        if (isGround != setBool)
            isGround = setBool;
    }

    void GroundScan()                           // 触地检测
    {
        bool groundDetect = false;
        //Collider2D[] contacts = new Collider2D[2];
        //playerCollider.GetContacts(contacts);
        //foreach (Collider2D item in contacts)
        //{
        //    if (item.gameObject.CompareTag("Tile"))
        //    {
        //        groundDetect = true;
        //    }
        //}
        if (playerCollider.IsTouchingLayers(ground))
        {
            groundDetect = true;
        }

        if (groundDetect)
        {
            SetIsGround(true);
            playerJumpCount = 0;
        }
    }
}
