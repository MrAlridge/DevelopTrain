using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapleHead : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject abilitySystem;
    void Start()
    {
        abilitySystem = GameObject.FindWithTag("AbilitySystem");
    }

    // Update is called once per frame
    void Update()
    {
        // 速度限制以后再说
    }

    void OnDestroy()
    {
        abilitySystem.GetComponent<AbilityScript>().grapleAbility.currentGraple = null;
        abilitySystem.GetComponent<AbilityScript>().grapleAbility.ResetGraple();
    }
}
