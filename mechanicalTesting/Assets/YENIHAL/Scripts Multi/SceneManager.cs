using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;

public class SceneManager : NetworkBehaviour
{

    [SyncVar] public int LobbyCount;

    public Collider player;


    public PlayerListManager playerListManager;


    public void PlayerCount()
    {
 
    }

    [Command]
    public void CmdPlayerCount()
    {
        if (!isLocalPlayer) return;
        //LobbyCount += 1;
        print(LobbyCount);

        if (LobbyCount == playerListManager.playerCount)
        {
            Debug.LogError("Diðer Sahneye Geçiþ Yapýlabilir.");
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CmdPlayerCount();
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            LobbyCount -= 1;
            print(LobbyCount);

        }
    }
}
