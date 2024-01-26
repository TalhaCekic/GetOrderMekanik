using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Mirror;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : NetworkBehaviour
{
    private PlayerInput playerInput;
    public bool isDayReady; 
    public bool change;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        change = false;
       // if(!isLocalPlayer)return;
        playerInput.currentActionMap["ReadyDay"].Enable();
        playerInput.currentActionMap["ReadyDay"].performed += isReady;
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (isLocalPlayer && isServer && currentScene.name == "PcScene")
        {
            changeInput();
        }
    }

    public void isReady(InputAction.CallbackContext context)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (!DayManager.instance.dayOn && currentScene.name == "PcScene")
        {
            isDayReady = !isDayReady;
            change = true;
        }
    }
    [Command(requiresAuthority = false)]
    public void changeInput()
    {
        RpcChangeInput();
    }

    [ClientRpc]
    public void RpcChangeInput()
    {
        if (change && DayManager.instance.daySelect.activeSelf == false)
        {
            if (isDayReady == true)
            {
                DayManager.instance.readyPlayer(1);
            }

            if (isDayReady == false)
            {
                DayManager.instance.notReadyPlayer(1);
            }

            change = false;
        }

        if (DayManager.instance.dayOn)
        {
            isDayReady = false;
        }
    }
    
}