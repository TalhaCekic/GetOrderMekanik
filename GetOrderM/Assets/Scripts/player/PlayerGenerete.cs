using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class PlayerGenerete : NetworkBehaviour
{
    [SyncVar(hook = nameof(HandleColorChanged))]
    private Color playerColor = Color.white;

    [SerializeField] private Image  hud;

   

    private void Start()
    {
        hud.gameObject.SetActive(false);
        if (!isLocalPlayer) return;
        playerColor = new Color(Random.value, Random.value, Random.value);
        HandleColorChanged(playerColor, playerColor);
        hud.gameObject.SetActive(true);
    }

    private void HandleColorChanged(Color oldColor, Color newColor)
    {
        hud.color = newColor;
    }
}
