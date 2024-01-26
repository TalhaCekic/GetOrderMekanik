using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem.HID;
using Steamworks;
using UnityEngine.InputSystem;

public class PlayerGenerete : NetworkBehaviour
{
    [SerializeField] public Image hud;
    //[SyncVar(hook = nameof(OnPlayerColorChanged))]
    //public Color playerColor = Color.white;
    [SerializeField] private DataManager dataManager;
    [SerializeField] private UILobbyName uILobbyName;
    [SyncVar] public int playerCount;
    [SyncVar] public string steamName;
    [SerializeField] private RenderTexture renderTexture;
    [SerializeField] private RawImage[] rawImages;


    // Customize
    public Camera cam;  
    private void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        
        ////Color randomColor = Random.ColorHSV();
        ////CmdChangeColor(randomColor);

        if (isLocalPlayer)
        {
            hud.gameObject.SetActive(true);
            hud.color = Color.blue;
            cam.cullingMask = this.gameObject.layer;
            cam.targetTexture = Instantiate(renderTexture, this.transform.parent);
            for (int i = 0; i < rawImages.Length; i++)
            {
                rawImages[i].texture = cam.targetTexture;
            }
        }
    }
    public override void OnStartLocalPlayer()
    {
        if (SteamManager.Initialized)
        {
            steamName = SteamFriends.GetPersonaName();
            CmdSetPlayerName(steamName);
        }
    }
    [Command]
    private void CmdSetPlayerName(string name)
    {
        steamName = name;
    }
    private void Update()
    {
        playerCount = NetworkManager.singleton.numPlayers;
        
    }
    //[Command(requiresAuthority = false)]
    //private void CmdChangeColor(Color newColor)
    //{
    //    playerColor = newColor;
    //}
    //private void OnPlayerColorChanged(Color oldColor, Color newColor)
    //{
    //    // Renk deðiþtiðinde yapýlacak iþlemler
    //    hud.color = newColor;
    //}

  
}
