using Mirror;
using TMPro;
using UnityEngine;
using Steamworks;
using System;

public class PlayerListManager : NetworkBehaviour
{
    [SyncVar]
    private string playerName;

    public TMP_Text[] playerNameText;

    public GameObject playerNamePrefabs;

    protected Callback<PersonaStateChange_t> m_PersonaStateChange;

    [SyncVar]
    public int playerCount;

    public Transform playerNamePrefabsTransform;

    protected Callback<LobbyEnter_t> m_lobbyEntered;
    protected Callback<GameLobbyJoinRequested_t> m_lobbyExited;


    private void Start()
    {
        m_lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
      m_lobbyExited = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyExited);

    }

    private void Update()
    {
        
    }
    public void OnLobbyEntered(LobbyEnter_t pCallback)
    {
        CSteamID steamId = SteamUser.GetSteamID();
        // Oyuncunun adýný çek
        playerName = SteamFriends.GetFriendPersonaName(steamId).ToString();
        //  Debug.Log(playerName);
        //playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_ulSteamIDLobby);
         Debug.Log("Player joined. Current players in lobby: " + playerCount);
        playerCount = NetworkManager.singleton.numPlayers;
        for (int i = 0; i < playerCount; i++)
        {
            Instantiate(playerNamePrefabs, playerNamePrefabsTransform);
            Debug.Log(playerName);
            playerNameText[i] = playerNamePrefabs.GetComponent<TMP_Text>();
            playerNameText[i].text = playerName;
        }
    }
    public void OnLobbyExited(GameLobbyJoinRequested_t pCallback)
    {
        playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)GameLobbyJoinRequested_t.k_iCallback);
        Debug.Log("Player left. Current players in lobby: " + playerCount);
    }

    // Oyuncu sunucuya baðlandýðýnda çalýþacak metod
    void OnPlayerConnected(NetworkConnection conn)
    {
        playerCount++;
        Debug.Log("Player Connected. Total players: " + playerCount);
        Instantiate(playerNamePrefabs);
    }

    // Oyuncu sunucudan ayrýldýðýnda çalýþacak metod
    void OnPlayerDisconnected(NetworkConnection conn)
    {
        playerCount--;
        Debug.Log("Player Disconnected. Total players: " + playerCount);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();

        // Sunucu durduðunda event'leri temizle
        NetworkServer.OnConnectedEvent -= OnPlayerConnected;
        NetworkServer.OnDisconnectedEvent -= OnPlayerDisconnected;
    }

    //public void LobbyEnter()
    //{
    //    RpcLobbyEnter = Callback<LobbyEnter_t>;
    //    m_lobbyExited = Callback<GameLobbyJoinRequested_t>.Create(GameLobbyJoinRequested_t.k_iCallback);
    //   // CmdLobbyEnter();
    //}

    //[Command]
    //public void CmdLobbyEnter()
    //{
    //    RpcLobbyEnter();
    //}

    //[ClientRpc]
    //public void RpcLobbyEnter()
    //{
    //    Debug.Log("Girdi");
    //    CSteamID steamId = SteamUser.GetSteamID();
    //    // Oyuncunun adýný çek
    //    playerName = SteamFriends.GetFriendPersonaName(steamId).ToString();
    //    Debug.Log(playerName);
    //    playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)LobbyEnter_t.k_iCallback);
    //    Debug.Log("Player joined. Current players in lobby: " + playerCount);
    //    for (int i = 0; i < playerCount; i++)
    //    {
    //        Instantiate(playerNamePrefabs, playerNamePrefabsTransform);
    //        Debug.Log(playerName);
    //        playerNameText[i] = playerNamePrefabs.GetComponent<TMP_Text>();
    //        playerNameText[i].text = playerName;
    //    }


    //}


}