using System.Collections;
using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using UnityEngine;
using Unity.Services.Relay.Models;
using Unity.Services.Relay;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using UnityEngine.UI;
using TMPro;
using System;

public class TestRelay : MonoBehaviour
{
    //OYUNCU DETAYLARI
    private int maxPlayer = 4;


    public TMP_Text Log;

    private string Code = "";

    // BUTTONS ve TEXTS
    [SerializeField] TMP_Text joinCode;
    [SerializeField] TMP_InputField joinCodeInput;
    [SerializeField] Button joinButton;
    [SerializeField] Button HostButton;


    public async void Start()
    {
        await UnityServices.InitializeAsync();

        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Singed in" + AuthenticationService.Instance.PlayerId);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();

        //
        HostButton.onClick.AddListener((CreateRelay));
        joinButton.onClick.AddListener(async () =>
        {
            try
            {
                Debug.Log("Joining Relay With  " + joinCodeInput.text);
                JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCodeInput.text);

                RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

                NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
                NetworkManager.Singleton.StartClient();
                Log.text = "Connected";
            }
            catch (RelayServiceException e)
            {
                Debug.LogError(e);
                Log.text = "Not Connected";
            }
            //Dictionary<string, string> playerData = new Dictionary<string, string>()
            //{

            //};

        });
    }
    private void Update()
    {
        joinCode.text = Code;
    }
    private async void CreateRelay()
    {
        try
        {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxPlayer);

            Code = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log("Host - Got join code: " + joinCode);

            RelayServerData relayServerData = new RelayServerData(allocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);

            
            
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
        }
    }
    public async void JoineRelay(string joinCode)
    {
        try
        {
            Debug.Log("Joining Relay With" + joinCode);
            JoinAllocation joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

            RelayServerData relayServerData = new RelayServerData(joinAllocation, "dtls");

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetRelayServerData(relayServerData);
            NetworkManager.Singleton.StartClient();
            Log.text = "Connected";
        }
        catch (RelayServiceException e)
        {
            Debug.LogError(e);
            Log.text = "Not Connected";
        }
    }

}
