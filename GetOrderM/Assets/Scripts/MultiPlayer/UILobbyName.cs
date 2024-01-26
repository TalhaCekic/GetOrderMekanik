using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class UILobbyName : NetworkBehaviour
{
    [SerializeField] public RawImage[] rawImage;

    [SerializeField] private Color[] color;

    [SerializeField] private DataManager dataManager;

    [SerializeField] public TextMeshProUGUI[] textMeshPros;

    [SyncVar]
    public int playerCount;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        rawImage = GetComponentsInChildren<RawImage>();
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        dataManager = GetComponent<DataManager>();
        for (int i = 0; i < rawImage.Length; i++)
        {
            rawImage[i].gameObject.SetActive(false);
        }

    }
    private void Update()
    {
     //   playerCount = NetworkManager.singleton.numPlayers;


    }
    //[Command(requiresAuthority = false)]
    //public void CmdUILobbyNames()
    //{
    //    RpcUILobbyNames();

    //}

    //[ClientRpc]
    //public void RpcUILobbyNames()
    //{
    //    for (int i = 0; i < playerCount; i++)
    //    {
       
    //      color[i] = dataManager.playerColors[i];
    //        rawImage[i].color = color[i];
    //        rawImage[i].gameObject.SetActive(true);
    //    }
    //}
    ////public void OnTriggerStay(Collider other)
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
