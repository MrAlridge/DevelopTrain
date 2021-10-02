using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilityCursor : MonoBehaviour
{
    public Image cursor;       // 能力游标

    public static int cursorEvent = 0;         // 代表按下了什么
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        detectKeyboard();
        switch (cursorEvent)
        {
            case 1:
            {
                cursor.rectTransform.localPosition = new Vector3(-40f, 0f, 0f);
                break;
            }
            case 2:
            {
                cursor.rectTransform.localPosition = new Vector3(-20f, 0f, 0f);
                break;
            }
            case 3:
            {
                cursor.rectTransform.localPosition = new Vector3(0f, 0f, 0f);
                break;
            }
            case 4:
            {
                cursor.rectTransform.localPosition = new Vector3(20f, 0f, 0f);
                break;
            }
            case 5:
            {
                cursor.rectTransform.localPosition = new Vector3(40f, 0f, 0f);
                break;
            }
            default:
                break;
        }
        
    }

    void detectKeyboard()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            cursorEvent = 1;
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            cursorEvent = 2;
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            cursorEvent = 3;
        }
        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            cursorEvent = 4;
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            cursorEvent = 5;
        }
    }
}
