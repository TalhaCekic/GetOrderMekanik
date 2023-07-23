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

    private void Start()
    {
       
    }
    private void Update()
    {


    }
    public void PlayerCount()
    {
        RpcPlayerCount();
    }

    [Command]
    public void CmdPlayerCount()
    {
        //LobbyCount += 1;
        print(LobbyCount);

        if (LobbyCount == playerListManager.playerCount)
        {
            Debug.LogError("Di�er Sahneye Ge�i� Yap�labilir.");
        }
    }

    [ClientRpc]
    public void RpcPlayerCount()
    {
        CmdPlayerCount();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerCount();
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
