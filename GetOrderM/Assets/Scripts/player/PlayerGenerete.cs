using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class PlayerGenerete : NetworkBehaviour
{
    [SyncVar(hook = nameof(OnPlayerColorChanged))]
    public Color playerColor = Color.white;

    [SerializeField] private Image hud;
    private void Start()
    {
        hud.gameObject.SetActive(true);
        if (isLocalPlayer)
        {
            CmdSetPlayerColor(Random.ColorHSV());
        }

    }

    [Command]
    private void CmdSetPlayerColor(Color color)
    {
        playerColor = color;
        RpcSetPlayerColor(color);
    }

    [ClientRpc]
    private void RpcSetPlayerColor(Color color)
    {
        playerColor = color;
    }

    private void OnPlayerColorChanged(Color oldColor, Color newColor)
    {
        // Renk deðiþtiðinde yapýlacak iþlemler
        hud.color = newColor;
    }


}
