using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem.HID;
using Steamworks;

public class PlayerGenerete : NetworkBehaviour
{
    [SerializeField] public Image hud;
    [SyncVar(hook = nameof(OnPlayerColorChanged))]
    public Color playerColor = Color.white;
    [SerializeField]private DataManager dataManager;
    [SerializeField] private UILobbyName uILobbyName;
    [SyncVar] public int playerCount;
    public string steamName;


    private void Start()
    {

        steamName = SteamFriends.GetPersonaName();
        dataManager = FindObjectOfType<DataManager>();
        hud.gameObject.SetActive(true);
        Color randomColor = Random.ColorHSV();
        CmdChangeColor(randomColor);

    }
    public override void OnStartClient()
    {
        base.OnStartClient();
       

    }
    private void Update()
    { 
        playerCount = NetworkManager.singleton.numPlayers;
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
