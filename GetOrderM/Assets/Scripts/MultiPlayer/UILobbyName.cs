using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class UILobbyName : NetworkBehaviour
{

    [SerializeField] private PlayerGenerete[] playerGenerete;

    [SerializeField] private RawImage[] rawImage;


    [SerializeField] private Color[] color;

    [SyncVar]
    private int playerCount;
    private void Start()
    {
        rawImage = GetComponentsInChildren<RawImage>();
        for (int i = 0; i < rawImage.Length; i++)
        {
            rawImage[i].gameObject.SetActive(false);
        }


    }
    private void Update()
    {
       playerCount = NetworkManager.singleton.numPlayers;


        if (isServer)
        {

            CmdUILobbyNames();
        }




    }
    [Command(requiresAuthority = false)]
    public void CmdUILobbyNames()
    {
        RpcUILobbyNames();
    }

    [ClientRpc]
    public void RpcUILobbyNames()
    {
        //playerGenerete = FindObjectsOfType<PlayerGenerete>();
        //color = new Color[playerGenerete.Length];

        //foreach (var players in playerGenerete)
        //{
        //    color[playerGenerete.Length] = players.playerColor;
        //    rawImage[playerGenerete.Length].color = color[playerGenerete.Length];
        //    rawImage[playerGenerete.Length].gameObject.SetActive(true);
        //}

        for (int i = 0; i < playerCount; i++)
        {
            playerGenerete[i] = FindObjectOfType<PlayerGenerete>();

            color[i] = playerGenerete[i].playerColor;
            rawImage[i].color = color[i];
            rawImage[i].gameObject.SetActive(true);
        }
    }

   
}
