using UnityEngine;
using Steamworks;
using TMPro;
using UnityEngine.UI;

public class PlayerListItem : MonoBehaviour
{
    public string PlayerName;
    public int ConnectionID;
    public ulong PlayerSteamID;
    private bool AvatarReceived;

    public TMP_Text PlayerNameText;
    public RawImage PlayerIcon;
    public TMP_Text PlayerReadyText;
    public bool Ready;

    protected Callback<AvatarImageLoaded_t> ImageLoaded;

    public void ChangeReadyStatues()
    {
        if(Ready) 
        {
            PlayerReadyText.text = "Ready";
            PlayerReadyText.color = Color.green;
        }
        else
        {
            PlayerReadyText.text = "Unready";
            PlayerReadyText.color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
     //   ImageLoaded = Callback<AvatarImageLoaded_t>.Create(OnImageLoaded);
    }

    public void SetPlayerValuse()
    {
        PlayerNameText.text = PlayerName;
        ChangeReadyStatues();
        if(!AvatarReceived) { GetPlayerIcon(); }
    }
    void GetPlayerIcon()
    {

    }
    void OnImageLoaded()
    {

    }
}
