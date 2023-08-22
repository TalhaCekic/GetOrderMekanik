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
       // CmdUILobbyNames(rawImage,playerGenerete,color);
        //if (isServer)
        //{
        //    RpcUILobbyNames();
        //}
        //else
        //{
        //    CmdUILobbyNames();
        //}

    }
    //[Command(requiresAuthority = false)]
    //public void CmdUILobbyNames(RawImage[] raw, PlayerGenerete[] player, Color[] color1)
    //{
    //    RpcUILobbyNames(raw,player,color1);
    //}

    //[ClientRpc]
    //public void RpcUILobbyNames(RawImage[] raw, PlayerGenerete[] player, Color[] color1)
    //{
    //    print("test");
    //    for (int i = 0; i < playerCount; i++)
    //    {
    //        player[i] = FindObjectOfType<PlayerGenerete>();
    //        color1[i] = player[i].playerColor;
    //        raw[i].color = color1[i];
    //        raw[i].gameObject.SetActive(true);
    //    }
    //}
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
