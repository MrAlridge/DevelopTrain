using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] destnations;                 // 要经过的目标点
    public float moveSpeed;                         // 平台移动速度
    private int maxIndex;                               // 数组上限
    private int index = 0;                              // 迭代数
    private Vector3 destPosition;                    //目标位置
    void Start()
    {
        maxIndex = destnations.Length;
        destPosition = destnations[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        IndexDetect();
        Vector2 movePos = new Vector2(Mathf.Lerp(transform.position.x, destPosition.x, /*moveSpeed * */Time.deltaTime), Mathf.Lerp(transform.position.y, destPosition.y, /*moveSpeed * */Time.deltaTime));
        this.GetComponent<Rigidbody2D>().MovePosition(movePos);
        //Debug.Log(destPosition.ToString());
    }

    void IndexDetect()
    {
        if(Vector3.Distance(transform.position, destPosition) <= 0.5)
        {
            if(index == maxIndex)
            {
                index = 0;
            }else{
                index++;
            }
            destPosition = destnations[index].position;
        }
    }

}
