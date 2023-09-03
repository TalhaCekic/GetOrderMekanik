using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : NetworkBehaviour
{
    public readonly SyncList<Color> playerColors = new SyncList<Color>();

    // UI'da göstermek için kullanýlacak Image bileþenleri
    public RawImage[] colorImages;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public override void OnStartServer()
    {
        playerColors.Callback += OnPlayerColorsUpdated;
        cmdShowFirstThreeColors();
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
    [Command(requiresAuthority = false)] public void cmdShowFirstThreeColors()
    {
        ShowFirstThreeColors();
    }
    // UI'da ilk 3 rengi göstermek için kullanýlacak fonksiyon
    [ClientRpc]
    public void ShowFirstThreeColors()
    {
        for (int i = 0; i < Mathf.Min(colorImages.Length, 3); i++)
        {
            if (i < playerColors.Count)
            {
                colorImages[i].color = playerColors[i];
                colorImages[i].gameObject.SetActive(true);
            }
            else
            {
                //colorImages[i].gameObject.SetActive(false);
            }
        }
    }
}
