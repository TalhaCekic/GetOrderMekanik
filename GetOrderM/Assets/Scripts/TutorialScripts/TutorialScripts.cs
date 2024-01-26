using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using Steamworks;

public class TutorialScripts : NetworkBehaviour
{
    private int currentStep = 0;
    private string[] tutorialStepsTr = { "'E' Tu?una basarak bilgisayar kasas?n? al", "'E' Tu?una basarak bilgisayar kasas?n? masaya b?rak", "'Q' tu?una basarak bilgisayar kasas?n? kutusunu a�", "'E' Tu?una basarak anakart? al",
        "'E' Tu?una basarak anakart? masaya b?rak", "'Q' Tu?una basarak anakart kutusunu a�", "'E' Tu?una basarak anakart? al", "'E' Tu?una basarak anakart? kasan?n i�ine koy", "'E' Tu?una basarak kasay? al",
        "'E' Tu?una basarak sipari?i teslim et", "Tebrikler art?k oyunu oynamaya haz?rs?n. 'p' Tu?una basarak ana men�ye d�n." };
    private string[] tutorialStepsEng = { "Press 'E' to pick up the computer case", "Press 'E' to place the computer case on the table", "Press 'Q' to open the computer case", "Press 'E' to pick up the motherboard",
        "Press 'E' to place the motherboard on the table", "Press 'Q' to open the motherboard box", "Press 'E' to pick up the motherboard", "Press 'E' to place the motherboard into the case", "Press 'E' to pick up the case",
        "Press 'E' to deliver the order", "Congratulations, you are now ready to play the game. Press 'p' to return to the main menu." };


    private string[] requiredInputs = { "e", "e", "q", "e", "e", "q", "e", "e", "e", "e","e" };
    private bool[] stepCompleted;
    public TMP_Text text;
    public RawImage[] tutorialImages;
    public bool useE = false;


    private void Start()
    {
        text.text = tutorialStepsEng[currentStep];
        SetActiveTutorialImage(currentStep);
    }

    private void Update()
    {
        if (currentStep < tutorialStepsEng.Length)
        {
            CheckInput();
        }
        else
        {
            //Debug.Log("Tutorial tamamlandi. ?yi i?!");
        }

        MenuyeDon();
    }

    private void MenuyeDon()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
            NetworkManager.singleton.ServerChangeScene("PcMen�");
            ulong asd = SteamLobby.instance.currentLobbyID;    
            CSteamID lobbySteamId = (CSteamID)asd;
            SteamMatchmaking.LeaveLobby(lobbySteamId);
            SteamLobby.instance.currentLobbyID = 0;
            NetworkManager.singleton.StopHost(); // Host modunda �al���yorsa sunucuyu durdurun
            NetworkManager.singleton.StopClient(); // �stemci modunda �al���yorsa istemciyi durdurun
            NetworkManager.singleton.StopServer(); // Sunucu modunda �al���yorsa sunucuyu durdurun

            // NetworkManager'� yeniden ba�lat�n (�rne�in, �nceki ayarlar� koruyarak)
            NetworkManager.singleton.Start();

        }
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(requiredInputs[currentStep]))
        {
            //Debug.Log("Tebrikler! Ad�m " + (currentStep + 1) + " tamamland�.");
            SetInactiveTutorialImage(currentStep);
            currentStep++;
            if (currentStep < tutorialStepsEng.Length)
            {
                text.text = tutorialStepsEng[currentStep];
                SetActiveTutorialImage(currentStep);
            }
        }
    }

    private void SetActiveTutorialImage(int index)
    {
        if (index < tutorialImages.Length)
        {
            tutorialImages[index].gameObject.SetActive(true);
        }
    }

    private void SetInactiveTutorialImage(int index)
    {
        if (index < tutorialImages.Length)
        {
            tutorialImages[index].gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "counter")
        {
            useE = true;
        }
        else if(other.gameObject.tag == "Pickup")
        {
            useE=true;
        }
    }
}
