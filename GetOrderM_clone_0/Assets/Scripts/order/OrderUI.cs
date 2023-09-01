using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class OrderUI : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    [SerializeField] private OrderManager orderManager;

    [SyncVar] public int orderID = 0;
    void Start()
    {
        orderManager = FindAnyObjectByType<OrderManager>();
    }
    void Update()
    {
        if (isServer)
        {
            RpcýdCheck();
        }
        else
        {
            cmdýdCheck();
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
        if (objectNumber == 0) // boþ
        {
            orderID = 0;
            for (int i = 0; i < OrderObject.Count; i++)
            {
                OrderObject[i].SetActive(false);
            }
        }
        if (objectNumber == 12)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            orderID = 12;
        }
        if (objectNumber == 123)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            orderID = 123;
        }
        if (objectNumber == 124)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[4].SetActive(true);
            orderID = 124;
        }
        if (objectNumber == 125)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[5].SetActive(true);
            orderID = 125;
        }
        if (objectNumber == 1234)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[4].SetActive(true);
            orderID = 1234;
        }
        if (objectNumber == 1235)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[5].SetActive(true);
            orderID = 1235;
        }
        if (objectNumber == 1245)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[4].SetActive(true);
            OrderObject[5].SetActive(true);
            orderID = 1245;
        }
        if (objectNumber == 12345)
        {
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[4].SetActive(true);
            OrderObject[5].SetActive(true);
            orderID = 12345;
        }
    }
    void cmdýdCheck()
    {
        ýdCheck();
    }
    [ClientRpc]
    void RpcýdCheck()
    {
        ýdCheck();
    }
    public void ýdCheck()
    {
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
        if (orderID == 123)
        {
            if (isServer)
            {
                RpcinteractID(123);
            }
            else
            {
                CmdinteractID(123);
            }
        }
        if (orderID == 124)
        {
            if (isServer)
            {
                RpcinteractID(124);
            }
            else
            {
                CmdinteractID(124);
            }
        }
        if (orderID == 125)
        {
            if (isServer)
            {
                RpcinteractID(125);
            }
            else
            {
                CmdinteractID(125);
            }
        }
        if (orderID == 1234)
        {
            if (isServer)
            {
                RpcinteractID(1234);
            }
            else
            {
                CmdinteractID(1234);
            }
        }
        if (orderID == 1245)
        {
            if (isServer)
            {
                RpcinteractID(1245);
            }
            else
            {
                CmdinteractID(1245);
            }
        }
        if (orderID == 1235)
        {
            if (isServer)
            {
                RpcinteractID(1235);
            }
            else
            {
                CmdinteractID(1235);
            }
        }
        if (orderID == 12345)
        {
            if (isServer)
            {
                RpcinteractID(12345);
            }
            else
            {
                CmdinteractID(12345);
            }
        }
    }
}
