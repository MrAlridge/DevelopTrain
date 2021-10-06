using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartShell : MonoBehaviour
{
    public Button btn1, btn2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickStart()
    {
        SceneManager.LoadScene("MidScene");
    }

    public void ClickEnd()
    {
        Application.Quit();
    }
}
