/*
* 由于我实在不会算四元数，就直接用这个来LookAt鼠标，返回他的四元数就行了
* Update: 我是傻逼
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHelper : MonoBehaviour
{
    private Transform playerTransform;
    private Vector3 mousePosition;
    private static float retRotation;
    private static Vector2 retVector2;
    private static Vector3 retVector3;
    void Start()
    {
        playerTransform = this.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        // 指向鼠标的向量
        Vector3 tempVetcor = mousePosition - this.transform.position;
        retVector2 = new Vector2(tempVetcor.x, tempVetcor.y);
        retVector3  = mousePosition;
        retRotation = Vector2.Angle(tempVetcor, Vector2.up);
        if(mousePosition.x > this.transform.position.x)
        {
            retRotation = -retRotation;
        }
    }

    public static float GetRotation()
    {
        return retRotation;
    }

    public static Vector2 GetVector2()
    {
        return retVector2;
    }

    public static Vector3 GetVector3()
    {
        return retVector3;
    }
}
