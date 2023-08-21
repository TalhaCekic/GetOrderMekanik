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
    //public readonly SyncList<PlayerGenerete> playerGenerete = new SyncList<PlayerGenerete>();

    [SerializeField] private RawImage[] rawImage;
    //public readonly SyncList<RawImage> rawImage = new SyncList<RawImage>();

    [SerializeField] private Color[] color;
    //public readonly SyncList<Color> color = new SyncList<Color>();

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

        //if (isServer)
        //{
        //    RpcUILobbyNames();
        //}
        //else
        //{
        //    CmdUILobbyNames();
        //}

    }
    [Command(requiresAuthority = false)]
    public void CmdUILobbyNames()
    {
        RpcUILobbyNames();
    }

    [ClientRpc]
    public void RpcUILobbyNames()
    {
        print("test");
        for (int i = 0; i < playerCount; i++)
        {
            color[i] = playerGenerete[i].playerColor;
            rawImage[i].color = color[i];
            rawImage[i].gameObject.SetActive(true);
        }
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
           
            for (int i = 0; i < playerCount; i++)
            {
                playerGenerete[i] = FindObjectOfType<PlayerGenerete>();
                color[i] = playerGenerete[i].playerColor;
                rawImage[i].color = color[i];
                rawImage[i].gameObject.SetActive(true);
            }

        }
    }


}
