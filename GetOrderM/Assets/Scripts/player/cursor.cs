using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : NetworkBehaviour
{
    public pickUp PickUp;
    //public EscMenu escMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        PickUp = GetComponent<pickUp>();
        PickUp.pcInteract.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isLocalPlayer) return;
        ////mousenin kitlenip kitlenmeme durumunu kontrol eder.
        //if (PickUp.pcInteract.activeSelf == false && escMenu.panel.activeSelf ==false)
        //{
        //    Cursor.visible = false;
        //    Cursor.lockState = CursorLockMode.Locked;
        //}
        //else
        //{ 
        //    Cursor.visible = true;
        //    Cursor.lockState = CursorLockMode.None;
        //}
    }
}
