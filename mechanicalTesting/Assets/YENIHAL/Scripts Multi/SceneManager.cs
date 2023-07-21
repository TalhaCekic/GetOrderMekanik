using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;

public class SceneManager : NetworkBehaviour
{

    [SyncVar] public int playerCount;

    public Collider player;


    private void Update()
    {


    }
    public void PlayerCount()
    {
        CmdPlayerCount();
    }

    [Command]
    public void CmdPlayerCount()
    {
        RpcPlayerCount();
    }

    [ClientRpc]
    public void RpcPlayerCount()
    {
        OnTriggerEnter(player);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player")
        {
            playerCount += 1;
            print(playerCount);
            if (playerCount == 4)
            {
                Debug.Log("Diðer Sahneye Geçiþ Yapýlabilir.");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            playerCount -= 1;
            print(playerCount);

        }
    }
}
