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
    private string[] tutorialSteps = { "'E' Tuþuna basarak bilgisayar kasasýný al"," 'E' Tuþuna basarak bilgisayar kasasýný masaya býrak", "'Q' tuþuna basarak bilgisayar kasasýný kutusunu aç ", "'E' Tuþuna basarak anakartý  al",
    "'E' Tuþuna basarak anakartý masaya býrak","'Q' Tuþuna basarak anakart kutusunu aç","'E' Tuþuna basarak anakartý al", "'E' Tuþuna basarak anakartý kasanýn içine koy", "'E' Tuþuna basarak kasayý al",
        "'E' Tuþuna basarak sipariþi teslim et", "Tebrikler artýk oyunu oynamaya hazýrsýn. 'p' Tuþuna basarak ana menüye dön."  };
    private string[] requiredInputs = { "e", "e", "q", "e", "e", "q", "e", "e", "e", "e","e" };
    private bool[] stepCompleted;
    public TextMeshProUGUI text;
    public RawImage[] tutorialImages;
    public bool useE = false;


    private void Start()
    {
        text.text = tutorialSteps[currentStep];
        SetActiveTutorialImage(currentStep);
    }

    private void Update()
    {
        if (currentStep < tutorialSteps.Length)
        {
            CheckInput();
        }
        else
        {
            Debug.Log("Tutorial tamamlandi. ?yi i?!");
        }

        MenuyeDon();
    }

    private void MenuyeDon()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(0);
            NetworkManager.singleton.ServerChangeScene("PcMenü");
            ulong asd = SteamLobby.instance.currentLobbyID;    
            CSteamID lobbySteamId = (CSteamID)asd;
            SteamMatchmaking.LeaveLobby(lobbySteamId);
            SteamLobby.instance.currentLobbyID = 0;
            NetworkManager.singleton.StopHost(); // Host modunda çalýþýyorsa sunucuyu durdurun
            NetworkManager.singleton.StopClient(); // Ýstemci modunda çalýþýyorsa istemciyi durdurun
            NetworkManager.singleton.StopServer(); // Sunucu modunda çalýþýyorsa sunucuyu durdurun

            // NetworkManager'ý yeniden baþlatýn (örneðin, önceki ayarlarý koruyarak)
            NetworkManager.singleton.Start();

        }
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(requiredInputs[currentStep]))
        {
            Debug.Log("Tebrikler! Adým " + (currentStep + 1) + " tamamlandý.");
            SetInactiveTutorialImage(currentStep);
            currentStep++;
            if (currentStep < tutorialSteps.Length)
            {
                text.text = tutorialSteps[currentStep];
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
