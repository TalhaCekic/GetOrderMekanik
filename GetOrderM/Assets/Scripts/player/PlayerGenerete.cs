using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem.HID;

public class PlayerGenerete : NetworkBehaviour
{
    [SerializeField] private Image hud;
    [SyncVar(hook = nameof(OnPlayerColorChanged))]
    public Color playerColor = Color.white;
    private DataManager dataManager;
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        hud.gameObject.SetActive(true);

        Color randomColor = Random.ColorHSV();

        CmdChangeColor(randomColor);
        if (dataManager != null)
        {
            dataManager.AddPlayerColor(randomColor);
            dataManager.InitializePlayerColors(connectionToServer);
        }
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (dataManager != null)
        {
            dataManager.InitializePlayerColors(connectionToServer);
        }
    }
    [Command(requiresAuthority = false)]
    private void CmdChangeColor(Color newColor)
    {
        playerColor = newColor;
    }
    private void OnPlayerColorChanged(Color oldColor, Color newColor)
    {
        // Renk deðiþtiðinde yapýlacak iþlemler
        hud.color = newColor;
    }
}
