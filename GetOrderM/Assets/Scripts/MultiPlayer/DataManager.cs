using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : NetworkBehaviour
{
    // UI'da göstermek için kullanýlacak Image bileþenleri
    public RawImage[] colorImages;
    [SyncVar] public int playerCount;

    [SerializeField] private UILobbyName uILobbyName;

    public GameObject[] players;

    public int[] layerIndex;

    private void Start()
    {
        uILobbyName = GetComponent<UILobbyName>();
        DontDestroyOnLoad(gameObject);
        layerIndex[0] = LayerMask.NameToLayer("Player1");
        layerIndex[1] = LayerMask.NameToLayer("Player2");
        layerIndex[2] = LayerMask.NameToLayer("Player3");
        layerIndex[3] = LayerMask.NameToLayer("Player4");

    }
    private void Update()
    {
        playerCount = NetworkManager.singleton.numPlayers;
        // colorArray = new Color[playerColors.Count];

        for (int i = 0; i < playerCount + 1; i++)
        {
            // colorArray[i] = playerColors[i];
            players = GameObject.FindGameObjectsWithTag("Player");
        }
        if (isServer)
        {
            RpcUIColor(playerCount);
        }
        else
        {
            CmdUIColor(playerCount);
        }

    }
    [Command(requiresAuthority = false)]
    public void CmdUIColor(int count)
    {
        RpcUIColor(count);
    }
    [ClientRpc]
    public void RpcUIColor(int count)
    {
        if (count == 1 && players[0] != null)
        {
            // players[0].layer = layerIndex[0];
            players[0].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[0];
            ChangeLayerRecursively(players[0].transform, "Player1");
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[1].gameObject.SetActive(false);
            uILobbyName.rawImage[2].gameObject.SetActive(false);
            uILobbyName.rawImage[3].gameObject.SetActive(false);

        }
        if (count == 2 && players[1] != null && players[0] != null)
        {
            players[0].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[0];
            ChangeLayerRecursively(players[0].transform, "Player1");
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            players[1].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[1];
            ChangeLayerRecursively(players[1].transform, "Player2");
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[2].gameObject.SetActive(false);
            uILobbyName.rawImage[3].gameObject.SetActive(false);

        }
        if (count == 3 && players.Length == 3 && players[0] != null && players[1] != null && players[2] != null)
        {
            players[0].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[0];
            ChangeLayerRecursively(players[0].transform, "Player1");
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            players[1].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[1];
            ChangeLayerRecursively(players[1].transform, "Player2");
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
            players[2].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[2];
            ChangeLayerRecursively(players[2].transform, "Player3");
            uILobbyName.rawImage[2].gameObject.SetActive(true);
            uILobbyName.rawImage[2].color = players[2].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[2].text = players[2].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[3].gameObject.SetActive(false);
        }
        if (count == 4 && players.Length == 4 && players[0] != null && players[1] != null && players[2] != null && players[3] != null)
        {
            players[0].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[0];
            ChangeLayerRecursively(players[0].transform, "Player1");
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            players[1].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[1];
            ChangeLayerRecursively(players[1].transform, "Player2");
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
            players[2].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[2];
            ChangeLayerRecursively(players[2].transform, "Player3");
            uILobbyName.rawImage[2].gameObject.SetActive(true);
            uILobbyName.rawImage[2].color = players[2].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[2].text = players[2].GetComponent<PlayerGenerete>().steamName.ToString();
            players[3].GetComponentInChildren<Camera>().cullingMask = 1 << layerIndex[3];
            ChangeLayerRecursively(players[3].transform, "Player4");
            uILobbyName.rawImage[3].gameObject.SetActive(true);
            uILobbyName.rawImage[3].color = players[3].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[3].text = players[3].GetComponent<PlayerGenerete>().steamName.ToString();
        }
    }




    void ChangeLayerRecursively(Transform trans, string newLayer)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(newLayer);

        foreach (Transform child in trans)
        {
            ChangeLayerRecursively(child, newLayer);

            //   child.GetComponent<Camera>().cullingMask = 11;
        }
    }

}

