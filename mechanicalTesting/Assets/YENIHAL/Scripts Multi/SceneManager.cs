using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;
using UnityEngine.SceneManagement;

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
                if (isServer)  // Kontrol etmek �nemlidir, ��nk� bu yaln�zca sunucuda �al��mal�d�r.
                {
                    NetworkManager.singleton.ServerChangeScene("bo�");
                }
                Debug.Log("Di�er Sahneye Ge�i� Yap�labilir.");
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
