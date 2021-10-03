using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject playerTarget;
    public float offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(offset <= Vector3.Distance(playerTarget.transform.position, this.transform.position))
        {
            
        }
    }
}
