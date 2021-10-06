using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransScene : MonoBehaviour
{
    public int totalSeconds;    // 视频一共多少秒
    IEnumerator Countdown()
    {
        while(totalSeconds > 0)
        {
            yield return new WaitForSecondsRealtime(1f);
            totalSeconds--;
        }
        if(totalSeconds == 0)
        {
            SceneManager.LoadScene("MainStage");
        }
    }
    void Awake()
    {
        StartCoroutine(Countdown());
    }
}
