using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneManager1 : NetworkBehaviour
{

    [SyncVar] public int LobbyCount;

    public Collider player;


    public PlayerListManager playerListManager;


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
            Debug.Log(LobbyCount);
            Debug.Log(playerListManager.playerCount);
            if (LobbyCount == playerListManager.playerCount)
            {
                Debug.Log("Diðer Sahneye Geçiþ Yapýlabilir.");
                print(LobbyCount);
                print(playerListManager.playerCount);
                if (isServer)  // Kontrol etmek önemlidir, çünkü bu yalnýzca sunucuda çalýþmalýdýr.
                {
                    SceneManager.LoadScene(2);
                    NetworkManager.singleton.ServerChangeScene("oyun");
                }
             
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
