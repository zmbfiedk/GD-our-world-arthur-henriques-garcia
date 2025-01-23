using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationstatecontroller : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(anim);
    }

    void Update()
    {
        bool isrunning = anim.GetBool("Isrun");
        bool iswalking = anim.GetBool("Iswalking");
        bool forwardpressed = Input.GetKey("w");
        bool runpressed = Input.GetKeyDown(KeyCode.LeftShift);
        
        
        if (!iswalking && forwardpressed) //start
        {
            anim.SetBool("Iswalking", true);
        }

        if (iswalking && !forwardpressed) //stop
        {
            anim.SetBool("Iswalking", false);
        }

        if (isrunning && (forwardpressed && runpressed))
        {
            anim.SetBool ("Isrun", true);
            Debug.Log("run");
        }

        if( isrunning &&(!forwardpressed || !runpressed))
        {
            anim.SetBool("Isrun", true);

        }
    }
}
