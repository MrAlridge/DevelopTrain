using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapleHead : MonoBehaviour
{
    // -----Component Area-----
    public DistanceJoint2D playerJoint;                         // 玩家的关节
    public Collider2D thisCollider;
    public Rigidbody2D rigid;
    public GameObject abilitySystem;
    public float swingForce;                                    // 荡绳的力度
    // -----State Area-----
    public bool isHit = false;                          // 是否抓到锚点
    public int flyingTime;                              // 抓钩的飞行时间
    public Vector2 lockPosition;                        // 锁住的锚点的位置

    IEnumerator SelfDestory()
    {
        // code
        while(flyingTime > 0)
        {
            yield return new WaitForSeconds(1);
            flyingTime--;
        }
        if(flyingTime == 0 && !isHit)
        {
            Destroy(this.gameObject, 0.1f);
        }
    }

    void Start()
    {
        playerJoint = GameObject.FindWithTag("Player").GetComponent<DistanceJoint2D>();
        abilitySystem = GameObject.FindWithTag("AbilitySystem");
        
    }

    // Update is called once per frame
    void Update()
    {
        // 速度限制以后再说
        if(isHit)
        {
            if(thisCollider.enabled)
            {
                thisCollider.enabled = false;
            }
            this.rigid.MovePosition(lockPosition);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.tag == "GraplePoint")
        {
            isHit = true;
            lockPosition = new Vector2(coll.transform.position.x, coll.transform.position.y);
            if(!playerJoint.enabled)
            {
                playerJoint.enabled = true;
            }
            rigid.bodyType = RigidbodyType2D.Kinematic;
            playerJoint.connectedBody = rigid;              // 绑定到抓钩
            playerJoint.distance = AbilityScript.grapleDistance;        //限定距离
            BasicPlayerBehaviour.SetPlayerMove(false);
        }
    }

    public void Launch(float grapleForce, Vector2 targetPosition)                    // 发射抓钩
    {
        this.transform.Rotate(new Vector3(0f, 0f,CursorHelper.GetRotation()));
        this.rigid.AddForce(CursorHelper.GetVector2() * grapleForce * 0.01f, ForceMode2D.Impulse);
        // this.rigid.velocity = new Vector2(grapleForce, 0f);
        StartCoroutine(SelfDestory());
    }

    public void Swing(float dir)
    {
        //先求垂直向量
        playerJoint.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(dir * swingForce, 0f));
    }

    void OnDestroy()
    {
        abilitySystem.GetComponent<AbilityScript>().grapleAbility.currentGraple = null;
        abilitySystem.GetComponent<AbilityScript>().grapleAbility.ResetGraple();
        BasicPlayerBehaviour.SetPlayerMove(true);
        playerJoint.enabled = false;
    }
}
