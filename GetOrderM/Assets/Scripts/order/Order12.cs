using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order12 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    [SerializeField] private OrderManager orderManager;

    [SyncVar] public int orderID = 0;
    void Start()
    {
        orderManager = FindAnyObjectByType<OrderManager>();
        CmdinteractID(0);
    }
    void Update()
    {
        if (isServer)
        {
            Rpc�dCheck();
        }
        else
        {
            cmd�dCheck();
        }
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
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
        }
    }
    void cmd�dCheck()
    {
        �dCheck();
    }
    [ClientRpc]
    void Rpc�dCheck()
    {
        �dCheck();
    }
    public void �dCheck()
    {
        if(orderID == 0)
        {
            if (isServer)
            {
                RpcinteractID(0);
            }
            else
            {
                CmdinteractID(0);
            }
        }
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
