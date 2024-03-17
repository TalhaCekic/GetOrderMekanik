using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror.FizzySteam;
using Unity.VisualScripting;

public class SteamLobby : NetworkBehaviour
{
    public static SteamLobby instance;

    protected Callback<LobbyCreated_t> LobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> JoinRequest;
    protected Callback<LobbyEnter_t> LobbyEntered;

    

    public ulong currentLobbyID;
    private const string HostAdressKey = "HostAddress";
    private CustomNetworkManager manager;

    public GameObject HostButton;
    //public Text LobbyNameText;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
       
        CheckSteamConnection();
        if (!SteamManager.Initialized) { return; }
        manager = GetComponent<CustomNetworkManager>();
        instance = this;
        LobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        JoinRequest = Callback<GameLobbyJoinRequested_t>.Create(onJoinRequest);
        LobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
    }
    private void CheckSteamConnection()
    {
        if (!SteamManager.Initialized)
        {
            // Check if the SteamManager component is already attached to the game object
            SteamManager steamManager = gameObject.GetComponent<SteamManager>();
            steamManager.enabled = true;
          
       

           // FizzySteamworks.active.ClientConnected();  
            print(" yazdýr çalýþtýr ");
        }

    }
    public void Host()
    {

        SceneManager.LoadScene(1);
        NetworkManager.singleton.ServerChangeScene("PcLobby");
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, manager.maxConnections);
       


    }
    public void Tutorial()
    {
        SceneManager.LoadScene(2);
        NetworkManager.singleton.ServerChangeScene("PcTutorial");
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, 1);
       

    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if (callback.m_eResult != EResult.k_EResultOK) { return; }
        manager.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAdressKey, SteamUser.GetSteamID().ToString());
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), "name", SteamFriends.GetPersonaName().ToString());
    }
    private void onJoinRequest(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        //everyone
        currentLobbyID = callback.m_ulSteamIDLobby;

        //clients
        if (NetworkServer.active) { return; }
        manager.networkAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAdressKey);
        manager.StartClient();
    }

}
