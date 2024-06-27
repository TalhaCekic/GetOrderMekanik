
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Mirror;


public class Menu : NetworkBehaviour
{
    public TextMeshProUGUI friendListText; // UI'de listeyi gösterecek Text elementi
    public ScrollRect scrollRect;
    public Button inviteButtonPrefab;
    public GameObject buttonContainer;
    public Button button;
    public GameObject popOut1;
    public GameObject multiplayer;
    //public GameObject friendsCavnas;
    //public Transform friendsContainer;


    public GameObject manager;
    public List<CSteamID> friends;

    
    public string DiscordUrl = "";
    public string SocialyLividyUrl = "";
    public string GetOrderSteamyUrl = "";
 
    private void Start()
    {
        if (SteamManager.Initialized)
        {
            PopulateFriendListUI();
            ButtonAtama();
            manager = GameObject.FindGameObjectWithTag("NetworkManager");
            //  manager = GameObject.FindAnyObjectByType<SteamLobby>();
        }
        else
        {
            Debug.LogError("Steam API başlatılamadı.");
        }
    }

    private void PopulateFriendListUI()
    {
        if (SteamManager.Initialized)
        {
            int friendCount = SteamFriends.GetFriendCount(EFriendFlags.k_EFriendFlagAll);
            for (int i = 0; i < friendCount; i++)
            {
                CSteamID friendSteamID = SteamFriends.GetFriendByIndex(i, EFriendFlags.k_EFriendFlagAll);
                if (SteamFriends.GetFriendPersonaState(friendSteamID) == EPersonaState.k_EPersonaStateOnline)
                {
                    string friendName = SteamFriends.GetFriendPersonaName(friendSteamID);
                    //friendListText.text += friendName + "\n"; 
                    CreateFriendButton(friendSteamID, friendName);
                }
            }
            // ScrollView'i güncelle
            Canvas.ForceUpdateCanvases();
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }

    private void CreateFriendButton(CSteamID friendSteamID, string friendName)
    {
        Button button = Instantiate(inviteButtonPrefab, buttonContainer.transform); // Düğme örneğini oluştur
        button.GetComponentInChildren<TextMeshProUGUI>().text = friendName; // Düğme metnini ayarla

        // Her düğmeye tıklanabilirlik ekle
        button.onClick.AddListener(() => CheckAndJoinGame(friendSteamID, friendName));
    }


    private void CheckAndJoinGame(CSteamID friendSteamID, string friendName)
    {
        if (SteamManager.Initialized)
        {
            FriendGameInfo_t friendGameInfo;
            if (SteamFriends.GetFriendGamePlayed(friendSteamID, out friendGameInfo))
            {
                if (friendGameInfo.m_gameID.AppID() == SteamUtils.GetAppID())
                {
                    // Arkadaşın oynadığı oyun bu oyun ise, doğrudan oyununa katılma
                    SteamMatchmaking.JoinLobby(friendSteamID);

                    //   manager.networkAddress = SteamMatchmaking.GetLobbyData(friendSteamID,);
                    //manager.StartClient();
                }
                else
                {
                    Debug.Log(friendName + " şu anda farklı bir oyunda.");
                }
            }
            else
            {
                Debug.Log(friendName + " şu anda herhangi bir oyunda değil.");
            }
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void TutorialSceneButton()
    {
        SceneManager.LoadScene(2);
        NetworkManager.singleton.ServerChangeScene("PcTutorial");
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePrivate, 1);
    }

    public void LoadDiscordUrl()
    {
        Application.OpenURL(DiscordUrl);
    }
    public void LoadSocialUrl()
    {
        Application.OpenURL(SocialyLividyUrl);
    }
    public void LoadSteamlUrl()
    {
        Application.OpenURL(GetOrderSteamyUrl);
    }

    private void ButtonAtama()
    {
        button.onClick.AddListener(() => manager.gameObject.GetComponent<SteamLobby>().Host());
    }

    public void OpenPopOut1()
    {
        popOut1.gameObject.SetActive(true);
    }

    public void PopOutClose()
    {
        popOut1.gameObject.SetActive(false);
    }

    public void Multiplayer()
    {
        multiplayer.gameObject.SetActive(true);
    }

    public void MultiplayerClose()
    {
        multiplayer.gameObject.SetActive(false);
    }

    public void SteamJoinFriends()
    {
        SteamFriends.ActivateGameOverlay("Friends");
    }
}


