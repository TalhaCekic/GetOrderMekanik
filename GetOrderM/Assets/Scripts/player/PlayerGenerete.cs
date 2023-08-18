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
        if (!isLocalPlayer) return;
        CmdHudGenerete();
        

    }



    [Command]
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
        hud.gameObject.SetActive(true);
    }

}
