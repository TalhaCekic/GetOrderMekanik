using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;


public class PlayerGenerete : NetworkBehaviour
{
    [SyncVar]
    private Color playerColor = Color.white;

    [SerializeField] private Image hud;



    private void Start()
    {
        if (isServer)
        {
            RpcHudGenerete();
            hud.gameObject.SetActive(true);
        }
        else
        {
            CmdHudGenerete();
            hud.gameObject.SetActive(true);

        }

    }

    [Command(requiresAuthority = false)]
    public void CmdHudGenerete()
    {
        RpcHudGenerete();
    }


    [ClientRpc]
    public void RpcHudGenerete()
    {
        if (!isLocalPlayer) return;
        playerColor = new Color(Random.value, Random.value, Random.value);
        hud.color = playerColor;
       
    }

}
