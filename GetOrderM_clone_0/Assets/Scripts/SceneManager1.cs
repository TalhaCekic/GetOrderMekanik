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

    private bool test = false;

    public PlayerListManager playerListManager;

    //UI
    public Slider stayGround;

    private void Start()
    {
        timeCounter = 0;
    }
    private void Update()
    {
        if (test)
        {
            timeCounter += Time.deltaTime;
            stayGround.value = timeCounter;
            if (timeCounter >= 1)
            {
                SceneManager.LoadScene(2);
                NetworkManager.singleton.ServerChangeScene("PcScene");

            }
        }



     //   Debug.Log(timeCounter);
    }

    public void OpenFriendsList()
    {
        if (!SteamManager.Initialized) { return; }

        // Steam overlay'ini kullanarak arkadaþ listesini açar
        SteamFriends.ActivateGameOverlay("Friends");

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            LobbyCount += 1;

            if (LobbyCount == playerListManager.playerCount && isServer)
            {
                test = true;
             
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            LobbyCount -= 1;
            test = false;
            timeCounter = 0;
            //  if(LobbyCount!=playerListManager.playerCount)
            //StopCoroutine(TimeCounter());

        }
    }
}
