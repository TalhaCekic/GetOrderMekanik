using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager1 : NetworkBehaviour
{

    [SyncVar]
    private float timeCounter = 0f;

    [SyncVar] public int LobbyCount;

    public GameObject[] playerClone;

    [SyncVar] private bool isReady = false;

    public PlayerListManager playerListManager;

    //UI
    public Slider stayGround;

    public TextMeshProUGUI lobbyCountText;

    private void Start()
    {
        timeCounter = 0;
    }
    private void Update()
    {
        if (isServer)
        {   
            CmdReady();
            LobbyCountText(lobbyCountText);
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdReady()
    {       
        RpcReady();
    }
    [ClientRpc]
    public void RpcReady()
    {
        //lobbyCountText.text = LobbyCount + "/ " + playerListManager.CmdPlayerCount(LobbyCount);
        if (isReady)
        {
            timeCounter += Time.deltaTime;
            stayGround.value = timeCounter;
            OnSliderValueChanged(timeCounter, stayGround.value);
            if (timeCounter >= 5f)
            {
                SceneManager.LoadScene(3);
                NetworkManager.singleton.ServerChangeScene("PcScene");
            }
        }
    }
    public void OpenFriendsList()
    {
        if (!SteamManager.Initialized) { return; }

        // Steam overlay'ini kullanarak arkadaş listesini açar
        SteamFriends.ActivateGameOverlay("Friends");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LobbyCount += 1;

            if (LobbyCount == playerListManager.playerCount && isServer)
            {
                isReady = true;
                CmdSetisReady(isReady);

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isReady = false;
            CmdSetisReady(isReady);
            LobbyCount -= 1;
            timeCounter = 0;
        }
    }
    private void OnSliderValueChanged(float oldValue, float newValue)
    {
        stayGround.value = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetisReady(bool newValue)
    {
        isReady = newValue;
    }

    [Server]
    public void LobbyCountText(TextMeshProUGUI text)
    {
        text.text = LobbyCount + "/ " + playerListManager.playerCount;
    }
}
