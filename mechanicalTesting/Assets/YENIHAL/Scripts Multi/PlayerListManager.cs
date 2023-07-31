using Mirror;
using TMPro;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerListManager : NetworkBehaviour
{

    [SyncVar] private string playerName;


    public TMP_Text[] LobbyNameText;

    [SyncVar]
    public int playerCount = 0;


    // public  PlayerControler playerControler;


    public GameObject[] playerClone;


    private void Update()
    {
        rpcName(SteamUser.GetSteamID());
        //DontDestroyOnLoad(gameObject);
        playerCount = NetworkServer.connections.Count;
        if (playerCount == 0) return;
        //   PlayerNames(SteamUser.GetSteamID());

    }

    //[Command]
    //void cmdNames(CSteamID steamId)
    //{
    //    PlayerNames(SteamUser.GetSteamID());
    //    rpcName(SteamUser.GetSteamID());
    //}

    [ClientRpc]
    void rpcName(CSteamID steamId)
    {
        PlayerNames(SteamUser.GetSteamID());
    }
    public void PlayerNames(CSteamID steamId)
    {

        for (int i = 0; i < playerCount; i++)
        {
            playerClone = GameObject.FindGameObjectsWithTag("Player");
            playerClone[i].GetComponent<PlayerControler>().CmdSetSteamId(steamId);
            playerName = playerClone[i].GetComponent<PlayerControler>().steamName;
            Debug.Log(playerName);
            LobbyNameText[i].gameObject.SetActive(true);
            LobbyNameText[i].text = playerName;

        }
    }

}