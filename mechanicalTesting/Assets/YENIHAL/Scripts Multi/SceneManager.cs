using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;

public class SceneManager : NetworkBehaviour
{

    [SyncVar] public int LobbyCount;

    public Collider player;


    public PlayerListManager playerListManager;

    private void Update()
    {
        playerListManager.playerCount = NetworkManager.singleton.numPlayers;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            LobbyCount += 1;
            if (LobbyCount == playerListManager.playerCount)
            {

                Debug.LogError("Diðer Sahneye Geçiþ Yapýlabilir.");
                print(LobbyCount);
                print(playerListManager.playerCount);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            LobbyCount -= 1;
            print(LobbyCount);

        }
    }
}
