using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using UnityEngine.SceneManagement;
using Mirror;

public class EscMenü : NetworkBehaviour
{
    [SerializeField] public GameObject panel;

     public bool isMenuOpen = false;

    [SerializeField] private pickUp PickUp;

    void Start()
    {
        panel.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!PickUp.isPcInteract)
            {
                // Menü durumunu güncelle
                isMenuOpen = !isMenuOpen;
                panel.SetActive(isMenuOpen);
            }
        }
    }
    private void MenuClose()
    {
        panel.gameObject.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void MenüOpen()
    {
        panel.gameObject.SetActive(true);
    }

    public void RestartScene()
    {
        if (isServer)
        {
            MenuClose();
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
            NetworkManager.singleton.ServerChangeScene(currentScene.name);
        }


    }


}
