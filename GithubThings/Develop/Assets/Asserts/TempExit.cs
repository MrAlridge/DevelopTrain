using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TempExit : MonoBehaviour
{
    public Button button;
    public Image image;
    bool isOpen = false;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isOpen)
            {
                image.enabled = true;
                button.enabled = true;
                isOpen = true;
            }else{
                image.enabled = false;
                button.enabled = false;
                isOpen = false;
            }
        }
    }
    public void Click()
    {
        Application.Quit();
    }
}
