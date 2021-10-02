using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class AbilityScript : MonoBehaviour
{
    public Graple grapleAbility;
    public int abilityIndex;
    public GameObject playerReferObject;
    public float grapleDistanceValue;
    public GameObject grapleReferObject;
    public float grapleForceValue;
    // [SerializeField]
    public static int currentAbilityIndex;                         // 当前能力编号
    public static GameObject playerObject;                         // 玩家
    public static float grapleDistance;                    // 抓钩最大距离
    public static GameObject grapleObject;                  // 抓钩的头
    public static float grapleForce;                        // 抓钩发射力度

    public abstract class BaseAbility
    {
        public int index;           // 能力编号
        public abstract void ActiveAbility();
    }

    public class Graple : BaseAbility
    {
        public int index = 1;
        public bool grapleOut = false;              // 抓钩是否飞出
        public GameObject currentGraple;            // 生成的抓钩
        public override void ActiveAbility()
        {
            // 抓钩发射
            if (!grapleOut)
            {
                //Debug.Log("Parameters:" + thisInstance.grapleObject.name + ',' + thisInstance.playerObject.name + ',' + thisInstance.grapleForce.ToString());
                currentGraple = Instantiate(grapleObject, playerObject.transform.position, CursorHelper.GetRotation());
                // currentGraple.transform.LookAt(CursorHelper.GetVector3());
                // 万恶之源应该就是这个施加力的语句了，这个GetVector2返回的向量绝壁有问题
                currentGraple.GetComponent<Rigidbody2D>().AddForce(grapleForce * CursorHelper.GetVector2());    // 这个力度是不是有点大
                Destroy(currentGraple, 4f);
                grapleOut = true;
            }else{
                Debug.Log("NOT NULL!");
            }
        }

        public void ResetGraple()
        {
            grapleOut = false;
            Debug.Log("Yes!");
        }
    }

    public AbilityScript()
    {
        
    }

    void Start()
    {
        currentAbilityIndex = abilityIndex;
        playerObject = playerReferObject;
        grapleDistance = grapleDistanceValue;
        grapleObject = grapleReferObject;
        grapleForce = grapleForceValue;
        grapleAbility = new Graple();
    }

    void Update()
    {
        currentAbilityIndex = AbilityCursor.cursorEvent;
        if(Input.GetMouseButtonDown(0))
        {
            switch (currentAbilityIndex)
            {
                case 1:
                {
                    grapleAbility.ActiveAbility();
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        // Debug.Log(grapleAbility.grapleOut.ToString());
    }
}
