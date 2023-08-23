using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order12 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    [SerializeField] private OrderManager orderManager;

    [SyncVar] public int orderID = 0;
    [SyncVar] public float couldown = 35;

    [SerializeField] private Slider sliderCouldown;

    void Start()
    {
        orderManager = FindAnyObjectByType<OrderManager>();
        //CmdinteractID(0);
        //orderID = 12;
    }
    void Update()
    {
        CmdinteractID(12);
        orderID = 12;
        if (isServer)
        {
            RpcinteractID(12);

        }
        else
        {
             CmdinteractID(12);
        }
        //if (isServer)
        //{
        //    RpcýdCheck();
        //}
        //else
        //{
        //    cmdýdCheck();
        //}
    }
    [Command(requiresAuthority = false)]
    public void CmdinteractID(float objectNumber)
    {
        interactID(objectNumber);
        RpcinteractID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        interactID(objectNumber);
    }
    public void interactID(float objectNumber)
    {
        if (orderID == 0)
        {
            for (int i = 0; i < OrderObject.Count; i++)
            {
                OrderObject[i].SetActive(false);
            }
        }
        if (objectNumber == 12)
        {
            print(" ÇALIÞTIRSANA  ");
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(false);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            couldown -= Time.deltaTime;
            sliderCouldown.value = couldown;
            if (couldown < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    [Command(requiresAuthority = false)]
    void cmdýdCheck()
    {
        RpcýdCheck();
    }
    [ClientRpc]
    void RpcýdCheck()
    {
        ýdCheck();
    }
    public void ýdCheck()
    {
        //if(orderID == 0)
        //{
        //    if (isServer)
        //    {
        //        RpcinteractID(0);
        //    }
        //    else
        //    {
        //        CmdinteractID(0);
        //    }
        //}
        if (orderID == 12)
        {
            if (isServer)
            {
                RpcinteractID(12);
            }
            else
            {
                CmdinteractID(12);
            }
        }
    }
}
