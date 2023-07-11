using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooranim1 : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetBool("open",true);
        }
       
    }
}
