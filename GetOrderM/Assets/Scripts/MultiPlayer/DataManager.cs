using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    //[ClientRpc]
    //private void RpcUpdatePlayerColor( Color color)
    //{
    //    // Renk güncelleme iþlemleri

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
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();

        }
        if (count == 2 && players[1] != null && players[0] !=null )
        {
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
        }
        if (count == 3 && players.Length == 3  && players[0] != null && players[1] != null && players[2] != null)
        {
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[2].gameObject.SetActive(true);
            uILobbyName.rawImage[2].color = players[2].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[2].text = players[2].GetComponent<PlayerGenerete>().steamName.ToString();
        }
        if (count == 4 && players.Length ==4  && players[0] != null && players[1] != null && players[2] != null && players[3] !=null)
        {
            uILobbyName.rawImage[0].gameObject.SetActive(true);
            uILobbyName.rawImage[0].color = players[0].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[0].text = players[0].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[1].gameObject.SetActive(true);
            uILobbyName.rawImage[1].color = players[1].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[1].text = players[1].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[2].gameObject.SetActive(true);
            uILobbyName.rawImage[2].color = players[2].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[2].text = players[2].GetComponent<PlayerGenerete>().steamName.ToString();
            uILobbyName.rawImage[3].gameObject.SetActive(true);
            uILobbyName.rawImage[3].color = players[3].GetComponent<PlayerGenerete>().hud.color;
            uILobbyName.textMeshPros[3].text = players[3].GetComponent<PlayerGenerete>().steamName.ToString();
        }
    }
}
