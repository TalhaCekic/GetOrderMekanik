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
    protected Callback<LobbyEnter_t> LobbyEntered;
    public ulong CurrentLobbyID;

    private CustomNetworkManager manager;
    private const string HostAddressKey = "HostAddress";
    private void Start()
    {
        if (!SteamManager.Initialized) { return; }
        manager = GetComponent<CustomNetworkManager>();
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }

    //public void OnLobbyEntered(LobbyEnter_t pCallback)
    //{

    //    CSteamID steamId = SteamUser.GetSteamID();
    //    playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_ulSteamIDLobby);
    //    Debug.Log("Player joined. Current players in lobby: " + playerCount);
    //}
    private void OnTriggerStay(Collider other)
    {
        print(playerCount);
        //if (playerCount == 1)
        //{
        //    print("aaaaaa");
        //}
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        
        CurrentLobbyID = callback.m_ulSteamIDLobby;
        
        playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)callback.m_ulSteamIDLobby);

        if (NetworkServer.active) { return; }

        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);

        manager.StartClient();

    }
}
