using Mirror;
using TMPro;
using UnityEngine;
using Steamworks;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerListManager : NetworkBehaviour
{

    // public TMP_Text[] playerNameText;
    public List<TMP_Text> childObject = new List<TMP_Text>();

    public GameObject playerNamePrefabs;
    public ulong CurrentLobbyID;

    [SyncVar]
    public int playerCount;

    //public Transform playerNamePrefabsTransform;
    public CustomNetworkManager manager;


    protected Callback<LobbyEnter_t> LobbyEntered;

    public PlayerControler playerControler;

    public CSteamID[] steamId;
    public string[] playerName;


    private void Update()
    {
        playerCount = NetworkServer.connections.Count;
        Debug.Log(playerCount);
        if (playerCount == 0) { return; }
       
        PlayerNames();
    }

    private void PlayerNames()
    {
        for (int i = 0; i < playerCount; i++)
        {
            steamId[i] = SteamUser.GetSteamID();
            playerName[i] = SteamFriends.GetFriendPersonaName(steamId[i]);
            childObject[i].gameObject.SetActive(true);
            childObject[i].text = playerName[i];
        }
    }

    //public void OnLobbyEntered(LobbyEnter_t pCallback)
    //{
    //    Debug.Log("asss");

    //    CSteamID steamId = SteamUser.GetSteamID();
    //    // Oyuncunun ad�n� �ek
    //    playerName = SteamFriends.GetFriendPersonaName(steamId).ToString();
    //    //  Debug.Log(playerName);
    //    //playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_ulSteamIDLobby);
    //    Debug.Log("Player joined. Current players in lobby: " + playerCount);
    //    playerCount = NetworkManager.singleton.numPlayers;

    //    for (int i = 0; i < 2; i++)
    //    {


    //        Debug.Log(playerName);
    //        childObject[i].gameObject.SetActive(true);
    //        childObject[i].text = playerName;
    //    }

    //    manager.StartClient();
    //}
    //public void OnLobbyExited(GameLobbyJoinRequested_t pCallback)
    //{
    //    playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)GameLobbyJoinRequested_t.k_iCallback);
    //    Debug.Log("Player left. Current players in lobby: " + playerCount);
    //}

}