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
    public static Vector2 lastSavePoint;               // 上个存档点
    public bool isGround;                      // 是否着地
    private static bool isMoveable = true;             // 是否可动
    public float playerSpeed;                   // 初始移动速度
    public float playerJumpForce;               // 初始跳跃力度
    public static float playerStaticJumpForce;
    public float playerJumpCount;              // 玩家已跳跃次数
    public float swingForce;
    private static Vector2 playerPositition;     // 玩家位置
    private static Vector3 player3DPosition;    // 玩家三维坐标
    private static Vector2 mousePosition;       // 鼠标位置
    public static bool isHurt;                  // 是否受伤
    // -----Input Data-----
    public static float horizontalInputValue;         // 水平轴输入
    public static float verticalInputValue;           // 垂直轴输入
    public static bool jumpInputState;                // 跳跃键输入
    // -----Other Thing-----
    // Ray2D groundDetectRay = new Ray2D(new Vector2(0,0), new Vector2(0, -1));                      // 触地检测射线
    public LayerMask ground;
    RaycastHit2D[] castResults;           // 返回的结果
    public float playerSpeedLimit;              // 速度上限
    public int hurtCounter;                 // 受伤无敌倒计时

    IEnumerator HurtCountdown()
    {
        if(hurtCounter > 0)
        {
            yield return new WaitForSeconds(1);
            hurtCounter--;
        }
        if(hurtCounter == 0)
        {
            isHurt = false;
        }
    }
    void Start()
    {
        playerStaticJumpForce = playerJumpForce;
        
    }

    void Update()
    {
        playerPositition = new Vector2(this.transform.position.x, this.transform.position.y);
        GroundScan();
        SpeedLimit();
        UpdateRender();
        HurtScan();
        GetInput();
        if(isMoveable)                          // 移动操作逻辑
        {
            if(horizontalInputValue != 0)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x + horizontalInputValue * playerSpeed, playerRigidbody.velocity.y);
            }
            if(isGround /*|| playerJumpCount < 1*/)
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
            if(playerRigidbody.velocity.x < -0.25)
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

    void HurtScan()
    {
        if(isHurt)
        {
            SetPlayerMove(false);
            playerAnimator.SetBool("HurtBool", true);
            StartCoroutine(HurtCountdown());
            // 这里应该添加点效果
            playerRigidbody.MovePosition(lastSavePoint);
        }else{
            hurtCounter = 2;            // !!!特别注意我在这里手动锁死这个无敌时间！！！
        }
    }

    void OnCollisionEnter2D(Collision2D coll)          // 封装检测
    {
        if(coll.gameObject.tag == "Spike")
        {
            if(!isHurt)
            {
                isHurt = true;
            }
        }
        if(coll.gameObject.tag == "Tile" && coll.transform.position.x != transform.position.x && Mathf.Abs(transform.position.y - coll.transform.position.y) < 0.25)
        {
            playerRigidbody.velocity = new Vector2(0f, playerRigidbody.velocity.y);
        }
    }

    // -----封装方法-----
    public static void SetPlayerSavePoint(Vector2 pos)
    {
        lastSavePoint = pos;
    }
    public static Vector2 GetPlayerSavePoint()
    {
        return lastSavePoint;
    }
    public static float GetPlayerJumpForce()
    {
        return playerStaticJumpForce;
    }
    public static void SetPlayerMove(bool input)
    {
        if(isMoveable != input)
        {
            isMoveable = input;
        }
    }

    public static Vector2 GetPlayerPosition()
    {
        return playerPositition;
    }

    public static Vector2 GetMousePosition()
    {
        return mousePosition;
    }

    public static Vector3 GetPlayer3DPosition()
    {
        return player3DPosition;
    }
}
