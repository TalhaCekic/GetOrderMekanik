using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;

public class SceneManager : NetworkBehaviour
{
    [SyncVar]
    public int playerCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    public void OnLobbyEntered(LobbyEnter_t pCallback)
    {

        CSteamID steamId = SteamUser.GetSteamID();
        playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_ulSteamIDLobby);
        Debug.Log("Player joined. Current players in lobby: " + playerCount);
    }
    private void OnTriggerStay(Collider other)
    {
        print("bbbbbbbbb");
        if (playerCount == 1)
        {
            print("aaaaaa");
        }
    }
}
