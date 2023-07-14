using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button startServerButton;
    [SerializeField]
    private Button startHostButton;
    [SerializeField]
    private Button startClientButton;
    [SerializeField]
    private TextMeshProUGUI playersInGameText;

    private void Awake()
    {
        Cursor.visible = true;
    }
    void Start()
    {
        startHostButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartHost();
            //if (NetworkManager.Singleton.StartHost())
            //{
            //    playersInGameText.text = ("Host started...");
                
            //}
            //else
            //{
            //    playersInGameText.text = ("Host coud not be started...");
            //}
        });  
        startServerButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartServer();
            //if (NetworkManager.Singleton.StartServer())
            //{
            //    playersInGameText.text = ("Server started...");
            //}
            //else
            //{
            //    playersInGameText.text = ("Server coud not be started...");
            //}
        });      
        startClientButton.onClick.AddListener(() =>
        {
            NetworkManager.Singleton.StartClient();
            //if (NetworkManager.Singleton.StartClient())
            //{
            //    playersInGameText.text = ("Client started...");
            //}
            //else
            //{
            //    playersInGameText.text = ("Client coud not be started...");
            //}
        });
    }
}
