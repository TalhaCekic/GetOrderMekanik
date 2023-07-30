using Mirror;
using TMPro;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerListManager : NetworkBehaviour
{
    [SyncVar]
    private string playerName;

    
    public TMP_Text[] LobbyNameText;

    [SyncVar]
    public int playerCount = 0;

  
  public  PlayerControler playerControler;

    
    public GameObject[] playerClone;

    private void Update()
    {
        playerCount = NetworkServer.connections.Count;
       if(playerCount == 0) return;
        PlayerNames(SteamUser.GetSteamID());
    }


    public void PlayerNames(CSteamID steamId)
    {
        playerClone = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < playerCount; i++)
        {

            playerClone[i].GetComponent<PlayerControler>().CmdSetSteamId(steamId);
            playerName = playerClone[i].GetComponent<PlayerControler>().steamName;
            LobbyNameText[i].gameObject.SetActive(true);
            LobbyNameText[i].text = playerName;

        }
    }

}