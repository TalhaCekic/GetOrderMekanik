using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem.HID;

public class PlayerGenerete : NetworkBehaviour
{
    [SerializeField] public Image hud;
    [SyncVar(hook = nameof(OnPlayerColorChanged))]
    public Color playerColor = Color.white;
    [SerializeField]private DataManager dataManager;
    [SerializeField] private UILobbyName uILobbyName;
    [SyncVar] public int playerCount;

  
    private void Start()
    {
      // uILobbyName = FindObjectOfType<UILobbyName>();
        dataManager = FindObjectOfType<DataManager>();
        hud.gameObject.SetActive(true);

        Color randomColor = Random.ColorHSV();

        CmdChangeColor(randomColor);
        //if (dataManager != null)
        //{
        //    dataManager.AddPlayerColor(randomColor);
        //    dataManager.InitializePlayerColors(connectionToServer);
        //}
    }
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        //if (dataManager != null)
        //{
        //    dataManager.InitializePlayerColors(connectionToServer);
        //}
    }
    private void Update()
    {
        
        playerCount = NetworkManager.singleton.numPlayers;
        Debug.Log(playerCount);
        
            //CmdUIColor(playerCount);
        
      
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
