using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NameChange : NetworkBehaviour
{
    public GameObject nameObject;
    private string stringName = "a,b,c,d,e,f,g,h,j,k,l,p,1,2,3,4,0";
    private void Start()
    {

       

        for (int i = 0; i < stringName.Length; i++)
        {
            nameObject.name +=  Random.Range(0,stringName.Length);
        }
        
    }
}
