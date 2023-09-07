using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : NetworkBehaviour
{
 //   public  SyncList<Color> playerColors = new SyncList<Color>();

    // UI'da göstermek için kullanýlacak Image bileþenleri
    public RawImage[] colorImages;
    [SyncVar] public int playerCount;
    
    [SerializeField] private UILobbyName uILobbyName;

    //   public Color[] colorArray;


    public GameObject[] players;
    private void Start()
    {
        uILobbyName = GetComponent<UILobbyName>();
       
        
        DontDestroyOnLoad(gameObject);
    }
    //public override void OnStartServer()
    //{
    //    playerColors.Callback += OnPlayerColorsUpdated;
    //    //   cmdShowFirstThreeColors();
    //}
    //private void OnPlayerColorsUpdated(SyncList<Color>.Operation op, int itemIndex, Color oldColor, Color newColor)
    //{
    //    for (int i = 0; i < NetworkServer.connections.Count; i++)
    //    {
    //        NetworkConnection conn = NetworkServer.connections[i];
    //        RpcUpdatePlayerColor(newColor); // Tüm oyunculara renk güncellemesini ileten RPC
    //    }
    //}

    private void Update()
    {
        playerCount = NetworkManager.singleton.numPlayers;
        // colorArray = new Color[playerColors.Count];

        for (int i = 0; i < playerCount+1; i++)
        {
            // colorArray[i] = playerColors[i];
            players = GameObject.FindGameObjectsWithTag("Player");
          

        }

        CmdUIColor(playerCount);

    }
    //public void AddPlayerColor(Color color)
    //{
    //    playerColors.Add(color);
    //}
    //public void InitializePlayerColors(NetworkConnection conn)
    //{
    //    foreach (Color color in playerColors)
    //    {
    //        RpcUpdatePlayerColor( color);
    //    }
    //}
    [ClientRpc]
    private void RpcUpdatePlayerColor( Color color)
    {
        // Renk güncelleme iþlemleri

    }
    //[Command(requiresAuthority = false)] public void cmdShowFirstThreeColors()
    //{
    //    ShowFirstThreeColors();
    //}
    //// UI'da ilk 3 rengi göstermek için kullanýlacak fonksiyon
    //[ClientRpc]
    //public void ShowFirstThreeColors()
    //{
    //    for (int i = 0; i < Mathf.Min(colorImages.Length, 3); i++)
    //    {
    //        if (i < playerColors.Count)
    //        {
    //            colorImages[i].color = playerColors[i];
    //            colorImages[i].gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            //colorImages[i].gameObject.SetActive(false);
    //        }
    //    }
    //}

    [Command(requiresAuthority = false)]
    public void CmdUIColor(int count)
    {
        RpcUIColor(count);
    }

    [ClientRpc]
    public void RpcUIColor(int count)
    {

        if (count == 1 && players[0] !=null)
        {
            Debug.Log("girdi");
         //   uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;

        }
         if (count == 2 && players[1] != null && players[0] !=null )
        {
          // uILobbyName.rawImage[0].gameObject.SetActive(true);
          uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
          //  uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
        }
        if (count == 3 && players.Length == 3  && players[0] != null && players[1] != null && players[2] != null)
        {
            // uILobbyName.rawImage[0].gameObject.SetActive(true);
              uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            //  uILobbyName.rawImage[1].gameObject.SetActive(true);
             uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            //  uILobbyName.rawImage[2].gameObject.SetActive(true);
            uILobbyName.rawImage[2].color = players[2].GetComponent<PlayerGenerete>().hud.color;
        }
        //else if (count == 4)
        //{
        //    uILobbyName.rawImage[0].gameObject.SetActive(true);
        //    uILobbyName.rawImage[0].color = playerColors[0];
        //    uILobbyName.rawImage[1].gameObject.SetActive(true);
        //    uILobbyName.rawImage[1].color = playerColors[1];
        //    uILobbyName.rawImage[2].gameObject.SetActive(true);
        //    uILobbyName.rawImage[2].color = playerColors[2];
        //    uILobbyName.rawImage[3].gameObject.SetActive(true);
        //    uILobbyName.rawImage[3].color = playerColors[3];
        //}
    }
}
