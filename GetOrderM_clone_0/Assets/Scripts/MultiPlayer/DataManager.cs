using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : NetworkBehaviour
{
    public readonly SyncList<Color> playerColors = new SyncList<Color>();

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public override void OnStartServer()
    {
        if (isLocalPlayer)
        {
            playerColors.Callback += OnPlayerColorsUpdated;
        }

    }
    private void OnPlayerColorsUpdated(SyncList<Color>.Operation op, int itemIndex, Color oldColor, Color newColor)
    {
        for (int i = 0; i < NetworkServer.connections.Count; i++)
        {
            NetworkConnection conn = NetworkServer.connections[i];
            RpcUpdatePlayerColor(itemIndex, newColor); // Tüm oyunculara renk güncellemesini ileten RPC
        }
    }
    public void AddPlayerColor(Color color)
    {
        playerColors.Add(color);
    }
    public void InitializePlayerColors(NetworkConnection conn)
    {
        foreach (Color color in playerColors)
        {
            RpcUpdatePlayerColor(playerColors.IndexOf(color), color);
        }
    }
    [ClientRpc]
    private void RpcUpdatePlayerColor(int index, Color color)
    {
        // Renk güncelleme iþlemleri
    }
    [Command]
    public void CmdChangeColor(Color newColor)
    {
        playerColors[connectionToClient.connectionId] = newColor;
    }
}

