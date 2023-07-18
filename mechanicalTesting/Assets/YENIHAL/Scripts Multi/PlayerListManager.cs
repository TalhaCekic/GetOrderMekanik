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

   int playerCount;

    public Transform playerNamePrefabsTransform;



    protected Callback<LobbyEnter_t> m_lobbyEntered;
    protected Callback<GameLobbyJoinRequested_t> m_lobbyExited;

    private void Start()
    {
        if (!SteamManager.Initialized) return;

        OnStartClient();
        m_lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        m_lobbyExited = Callback<GameLobbyJoinRequested_t>.Create(OnLobbyExited);
    }


    public override void OnStartClient()
    {
        base.OnStartClient();
            // Oyuncunun Steam ID'sini al
            CSteamID steamId = SteamUser.GetSteamID();

        // Oyuncunun adýný çek

        playerName = SteamFriends.GetFriendPersonaName(steamId).ToString();
        Debug.Log(playerName);
    }

    public void OnLobbyEntered(LobbyEnter_t pCallback)
    {
         playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_ulSteamIDLobby);
        Debug.Log("Player joined. Current players in lobby: " + playerCount);
        for (int i = 0; i < playerCount; i++)
        {
            Instantiate(playerNamePrefabs, playerNamePrefabsTransform);

            playerNameText[i].text = playerNamePrefabs.GetComponent<TMPro.TextMeshPro>().text;
            playerNameText[i].text = playerName;
        }

    }
    public void Update()
    {
        Debug.Log(playerCount);
       
    }
    public void OnLobbyExited(GameLobbyJoinRequested_t pCallback)
    {
        int playerCount = SteamMatchmaking.GetNumLobbyMembers((CSteamID)pCallback.m_steamIDLobby);
        Debug.Log("Player left. Current players in lobby: " + playerCount);
    }

    //// Oyuncu sunucuya baðlandýðýnda çalýþacak metod
    //void OnPlayerConnected(NetworkConnection conn)
    //{
    //    playerCount++;
    //    Debug.Log("Player Connected. Total players: " + playerCount);
    //    Instantiate(playerNamePrefabs,playerNameText.transform);
    //}

    //// Oyuncu sunucudan ayrýldýðýnda çalýþacak metod
    //void OnPlayerDisconnected(NetworkConnection conn)
    //{
    //    playerCount--;
    //    Debug.Log("Player Disconnected. Total players: " + playerCount);
    //}

    //public override void OnStopServer()
    //{
    //    base.OnStopServer();

    //    // Sunucu durduðunda event'leri temizle
    //    NetworkServer.OnConnectedEvent -= OnPlayerConnected;
    //    NetworkServer.OnDisconnectedEvent -= OnPlayerDisconnected;
    //}


}