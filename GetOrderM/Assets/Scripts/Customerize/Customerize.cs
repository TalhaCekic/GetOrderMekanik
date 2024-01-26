using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Customerize : NetworkBehaviour
{
    // Karakter objeleri

    [Header("Customize")]
    [SerializeField] private GameObject[] tshirtChange;

    [SerializeField] private GameObject[] pantsChange;

    [SerializeField] private GameObject[] bodyChange;

    [SerializeField] private GameObject[] earsChange;

    [SerializeField] private GameObject[] hairChange;

    [SerializeField] private GameObject[] shoesChange;

    [SerializeField] private GameObject[] CosmeticChange;

    [SerializeField] private GameObject[] hudChange;


    [Header("Canvaslar")]
    // Canvaslar
    [SerializeField] private GameObject tshirtCanvas;
    [SerializeField] private GameObject pantsCanvas;
    [SerializeField] private GameObject shoeseCanvas;
    [SerializeField] private GameObject hairCanvas;
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject cosmeticCanvas;



    //[Command]
    //public void CmdRpcChange(int index)
    //{
    //    // Sunucu tarafýndan çaðrýlan bu metot, sunucu üzerindeki deðiþiklikleri kontrol etmelidir.
    //     rpcChange(index);
    //    RpcUpdateTshirtColor(index); // Diðer oyunculara iletilmesi gereken RPC çaðrýsý
    //}

    //[ClientRpc]
    //public void RpcUpdateTshirtColor(int index)
    //{
    //    // Tüm istemcilere deðiþiklikleri iletmek için kullanýlýr
    //    CmdRpcChange(index);
    //}
    //public void button(int index)
    //{
    //    if(isServer)
    //    {
    //        rpcChange(index);
    //    }
    //    else
    //    {
    //        ChangeTshirtColor(index);
    //    }
    //}
    [Command(requiresAuthority = false)]
    public void ChangeTshirtColor(int index)
    {

        //  RpcUpdateTshirtColor(index);
        rpcChange(index);
    }
    [ClientRpc]
    public void rpcChange(int index)
    {
        //T-shirt Change
        if (index == 0)
        {

            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(true);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 1)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(true);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 2)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(true);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 3)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(true);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 4)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(true);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 5)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(true);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 6)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(true);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 7)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(true);
            tshirtChange[9].SetActive(false);
        }
        else if (index == 8)
        {
            tshirtChange[0].SetActive(false);
            tshirtChange[1].SetActive(false);
            tshirtChange[2].SetActive(false);
            tshirtChange[3].SetActive(false);
            tshirtChange[4].SetActive(false);
            tshirtChange[5].SetActive(false);
            tshirtChange[6].SetActive(false);
            tshirtChange[7].SetActive(false);
            tshirtChange[8].SetActive(false);
            tshirtChange[9].SetActive(true);
        }


    }




    [Command(requiresAuthority = false)]
    public void ChangePantsColor(int index)
    {

        RpcChangePants(index);

    }
    //[Command]
    //public void CMDChangePantsColor(int index)
    //{
    //    RpcChangePants(index);
    //    RpcChangePantsColor(index);
    //}
    //[ClientRpc]
    //public void RpcChangePantsColor(int index)
    //{
    //    CMDChangePantsColor(index);
    //}


    [ClientRpc]
    public void RpcChangePants(int index)
    {
        //Pants Change
        if (index == 0)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(true);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);

        }
        else if (index == 1)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(true);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 2)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(true);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 3)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(true);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 4)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(true);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 5)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(true);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 6)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(true);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(false);
        }
        else if (index == 7)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(true);
            pantsChange[9].SetActive(false);
        }
        else if (index == 8)
        {
            pantsChange[0].SetActive(false);
            pantsChange[1].SetActive(false);
            pantsChange[2].SetActive(false);
            pantsChange[3].SetActive(false);
            pantsChange[4].SetActive(false);
            pantsChange[5].SetActive(false);
            pantsChange[6].SetActive(false);
            pantsChange[7].SetActive(false);
            pantsChange[8].SetActive(false);
            pantsChange[9].SetActive(true);
        }


    }


    [Command(requiresAuthority = false)]
    public void ChangeBodyColor(int index)
    {
        RpcChangeBody(index);

    }
    //[Command]
    //public void CMDChangeBodyColor(int index)
    //{
    //    RpcChangeBody(index);
    //    RPCChangeBodyColor(index);
    //}
    //[ClientRpc]
    //public void RPCChangeBodyColor(int index)
    //{
    //    CMDChangeBodyColor(index);

    //}


    [ClientRpc]
    public void RpcChangeBody(int index)
    {
        //body Change
        if (index == 0)
        {
            bodyChange[0].SetActive(false);
            bodyChange[1].SetActive(true);
            bodyChange[2].SetActive(false);
            bodyChange[3].SetActive(false);

            //
            earsChange[0].SetActive(false);
            earsChange[1].SetActive(true);
            earsChange[2].SetActive(false);
            earsChange[3].SetActive(false);

        }
        else if (index == 1)
        {
            bodyChange[0].SetActive(false);
            bodyChange[1].SetActive(false);
            bodyChange[2].SetActive(true);
            bodyChange[3].SetActive(false);

            //
            earsChange[0].SetActive(false);
            earsChange[1].SetActive(false);
            earsChange[2].SetActive(true);
            earsChange[3].SetActive(false);

        }
        else if (index == 2)
        {
            bodyChange[0].SetActive(false);
            bodyChange[1].SetActive(false);
            bodyChange[2].SetActive(false);
            bodyChange[3].SetActive(true);

            //
            earsChange[0].SetActive(false);
            earsChange[1].SetActive(false);
            earsChange[2].SetActive(false);
            earsChange[3].SetActive(true);

        }



    }

    [Command(requiresAuthority = false)]
    public void ChangeShoesColor(int index)
    {
        RpcChangeShoes(index);


    }
    //[Command]
    //public void CMDChangeShoesColor(int index)
    //{
    //    RpcChangeShoes(index);
    //    RPCChangeShoesColor(index);

    //}
    //[ClientRpc]
    //public void RPCChangeShoesColor(int index)
    //{
    //    CMDChangeShoesColor(index);


    //}


    [ClientRpc]
    public void RpcChangeShoes(int index)
    {
        //Shoes Change
        if (index == 0)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(true);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);

        }
        else if (index == 1)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(true);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 2)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(true);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 3)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(true);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 4)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(true);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 5)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(true);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 6)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(true);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(false);
        }
        else if (index == 7)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(true);
            shoesChange[9].SetActive(false);
        }
        else if (index == 8)
        {
            shoesChange[0].SetActive(false);
            shoesChange[1].SetActive(false);
            shoesChange[2].SetActive(false);
            shoesChange[3].SetActive(false);
            shoesChange[4].SetActive(false);
            shoesChange[5].SetActive(false);
            shoesChange[6].SetActive(false);
            shoesChange[7].SetActive(false);
            shoesChange[8].SetActive(false);
            shoesChange[9].SetActive(true);
        }


    }


    [Command(requiresAuthority = false)]
    public void ChangeHairColor(int index)
    {
        RpcChangeHair(index);

    }

    //[Command]
    //public void CMDChangeHairColor(int index)
    //{
    //    RpcChangeHair(index);
    //    RpcChangeHairColor(index);
    //}
    //[ClientRpc]
    //public void RpcChangeHairColor(int index)
    //{
    //    CMDChangeHairColor(index);

    //}


    [ClientRpc]
    public void RpcChangeHair(int index)
    {
        //Hair Change
        if (index == 0)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(true);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);

        }
        else if (index == 1)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(true);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 2)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(true);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 3)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(true);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 4)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(true);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 5)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(true);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 6)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(true);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(false);
        }
        else if (index == 7)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(true);
            hairChange[9].SetActive(false);
        }
        else if (index == 8)
        {
            hairChange[0].SetActive(false);
            hairChange[1].SetActive(false);
            hairChange[2].SetActive(false);
            hairChange[3].SetActive(false);
            hairChange[4].SetActive(false);
            hairChange[5].SetActive(false);
            hairChange[6].SetActive(false);
            hairChange[7].SetActive(false);
            hairChange[8].SetActive(false);
            hairChange[9].SetActive(true);
        }


    }

    [Command(requiresAuthority = false)]
    public void ChangeHudColor(int index)
    {

        RpcChanceHud(index);
    }
    //[Command]
    //public void CMDChangeHudColor(int index)
    //{
    //    RpcChanceHud(index);
    //    //RPCChangeHudColor(index);
    //}
    //[ClientRpc]
    //public void RPCChangeHudColor(int index)
    //{
    //    CMDChangeHudColor(index);
    //}


    [ClientRpc]
    public void RpcChanceHud(int index)
    {
        if (index == 0)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(true);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);

        }
        else if (index == 1)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(true);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 2)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(true);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 3)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(true);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 4)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(true);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 5)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(true);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 6)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(true);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 7)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(true);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 8)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(true);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 9)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(true);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 10)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(true);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 11)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(true);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 12)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(true);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 13)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(true);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 14)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(true);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 15)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(true);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(false);
        }
        else if (index == 16)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(true);
            hudChange[18].SetActive(false);
        }
        else if (index == 17)
        {
            hudChange[0].SetActive(false);
            hudChange[1].SetActive(false);
            hudChange[2].SetActive(false);
            hudChange[3].SetActive(false);
            hudChange[4].SetActive(false);
            hudChange[5].SetActive(false);
            hudChange[6].SetActive(false);
            hudChange[7].SetActive(false);
            hudChange[8].SetActive(false);
            hudChange[9].SetActive(false);
            hudChange[10].SetActive(false);
            hudChange[11].SetActive(false);
            hudChange[12].SetActive(false);
            hudChange[13].SetActive(false);
            hudChange[14].SetActive(false);
            hudChange[15].SetActive(false);
            hudChange[16].SetActive(false);
            hudChange[17].SetActive(false);
            hudChange[18].SetActive(true);
        }


    }

    [Command(requiresAuthority = false)]
    public void ChangeCosmeticColor(int index)
    {

        //  RpcUpdateTshirtColor(index);
        RpcChangeCosmetic(index);



    }
    [ClientRpc]
    public void RpcChangeCosmetic(int index)
    {
        if (index == 0)
        {
            CosmeticChange[0].SetActive(true);
            CosmeticChange[1].SetActive(false);
            CosmeticChange[2].SetActive(false);
            CosmeticChange[3].SetActive(false);
        }
        else if (index == 1)
        {
            CosmeticChange[0].SetActive(false);
            CosmeticChange[1].SetActive(true);
            CosmeticChange[2].SetActive(false);
            CosmeticChange[3].SetActive(false);

        }
        else if (index == 2)
        {
            CosmeticChange[0].SetActive(false);
            CosmeticChange[1].SetActive(false);
            CosmeticChange[2].SetActive(true);
            CosmeticChange[3].SetActive(false);

        }
        else if (index == 3)
        {
            CosmeticChange[0].SetActive(false);
            CosmeticChange[1].SetActive(false);
            CosmeticChange[2].SetActive(false);
            CosmeticChange[3].SetActive(true);

        }

    }

    public void TshirtCanvasOpen()
    {
        tshirtCanvas.SetActive(true);
        pantsCanvas.SetActive(false);
        shoeseCanvas.SetActive(false);
        hairCanvas.SetActive(false);
        hudCanvas.SetActive(false);
        cosmeticCanvas.SetActive(false);
    }

    public void PantsCanvasOpen()
    {
        tshirtCanvas.SetActive(false);
        pantsCanvas.SetActive(true);
        shoeseCanvas.SetActive(false);
        hairCanvas.SetActive(false);
        hudCanvas.SetActive(false);
        cosmeticCanvas.SetActive(false);
    }

    public void ShoeseCanvasOpen()
    {
        tshirtCanvas.SetActive(false);
        pantsCanvas.SetActive(false);
        shoeseCanvas.SetActive(true);
        hairCanvas.SetActive(false);
        hudCanvas.SetActive(false);
        cosmeticCanvas.SetActive(false);
    }

    public void HairCanvasOpen()
    {
        tshirtCanvas.SetActive(false);
        pantsCanvas.SetActive(false);
        shoeseCanvas.SetActive(false);
        hairCanvas.SetActive(true);
        hudCanvas.SetActive(false);
        cosmeticCanvas.SetActive(false);

    }

    public void HudCanvasOpen()
    {
        tshirtCanvas.SetActive(false);
        pantsCanvas.SetActive(false);
        shoeseCanvas.SetActive(false);
        hairCanvas.SetActive(false);
        hudCanvas.SetActive(true);
        cosmeticCanvas.SetActive(false);

    }

    public void CosmeticCanvasOpen()
    {
        tshirtCanvas.SetActive(false);
        pantsCanvas.SetActive(false);
        shoeseCanvas.SetActive(false);
        hairCanvas.SetActive(false);
        hudCanvas.SetActive(false);
        cosmeticCanvas.SetActive(true);

    }





}
