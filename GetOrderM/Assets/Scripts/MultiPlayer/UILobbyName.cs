using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UILobbyName : NetworkBehaviour
{
    // BÝRÝNCÝ YÖNTEM
    [SerializeField] private PlayerGenerete[] playerGenerete;

    [SerializeField] private RawImage[] rawImage;

    [SerializeField] private Color[] color;

    [SyncVar]
    private int playerCount;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rawImage = GetComponentsInChildren<RawImage>();
        for (int i = 0; i < rawImage.Length; i++)
        {
            rawImage[i].gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        playerCount = NetworkManager.singleton.numPlayers;
        //if (isLocalPlayer)
        //{
        //    CmdUILobbyNames(color);
        //}
        //if (!isLocalPlayer)
        //{
        //    RpcUILobbyNames(color);
        //}

        if (isServer)
        {
            RpcUILobbyNames(color);
        }
        else
        {
            CmdUILobbyNames(color);
        }

    }
    [Command(requiresAuthority = false)]
    public void CmdUILobbyNames(Color[] color1)
    {
        RpcUILobbyNames(color1);

        //for (int i = 0; i < playerCount; i++)
        //{
        //    playerGenerete[i] = FindObjectOfType<PlayerGenerete>();
        //    color1[i] = playerGenerete[i].playerColor;
        //    rawImage[i].color = color1[i];
        //    rawImage[i].gameObject.SetActive(true);
        //}


    }

    [ClientRpc]
    public void RpcUILobbyNames(Color[] color1)
    {
        //CmdUILobbyNames(color1);
        //print("test");
        for (int i = 0; i < playerCount; i++)
        {
            playerGenerete[i] = FindObjectOfType<PlayerGenerete>();
         //   color1[i] = playerGenerete[i].playerColor;
            rawImage[i].color = color1[i];
            rawImage[i].gameObject.SetActive(true);
        }
    }
    //public void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {

    //        for (int i = 0; i < playerCount; i++)
    //        {
    //            playerGenerete[i] = FindObjectOfType<PlayerGenerete>();
    //            color[i] = playerGenerete[i].playerColor;
    //            rawImage[i].color = color[i];
    //            rawImage[i].gameObject.SetActive(true);
    //        }

    //    }
    //}


}
