using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DeliveryOrder : NetworkBehaviour
{
    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekranKartýDolu = false;
    [SyncVar] public bool ramDolu = false;

    public List<GameObject> childObject = new List<GameObject>();

    [SerializeField] private OrderManager orderManager;
    [SyncVar] public int submidID;
    // Start is called before the first frame update
    void Start()
    {
        orderManager = FindAnyObjectByType<OrderManager>();
    }

    // Update is called once per frame
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
        orderManager.orderHistory.Remove(submidID);
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
            submidID = 0;
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
        }
        if (objectNumber == 12)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            submidID = 12;
        }
        if (objectNumber == 123)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            submidID = 123;
        }
        if (objectNumber == 124)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 124;
        }
        if (objectNumber == 125)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 125;
        }
        if (objectNumber == 1234)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 1234;
        }
        if (objectNumber == 1235)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 1235;
        }
        if (objectNumber == 1245)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 1245;
        }
        if (objectNumber == 12345)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 12345;
        }
    }
    [Command(requiresAuthority = false)]
    void cmdýdCheck()
    {
        ýdCheck();
        // RpcýdCheck();
    }
    [ClientRpc]
    void RpcýdCheck()
    {
        ýdCheck();
    }
    public void ýdCheck()
    {
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekranKartýDolu && !ramDolu)
        {
            if (isServer)
            {
                RpcinteractID(0);
            }
            else if (isClient)
            {
                CmdinteractID(0);
            }
        }
        if (kasaDolu || anaKartDolu || cpuDolu || ekranKartýDolu || ramDolu)
        {
            if (kasaDolu && anaKartDolu)
            {
                if (isServer)
                {
                    RpcinteractID(12);
                }
                else
                {
                    CmdinteractID(12);
                }
                submidID = 12;
            }
            if (kasaDolu && anaKartDolu && cpuDolu)
            {
                if (isServer)
                {
                    RpcinteractID(123);
                }
                else
                {
                    CmdinteractID(123);
                }
                submidID = 123;
            }
            if (kasaDolu && anaKartDolu && ekranKartýDolu)
            {
                if (isServer)
                {
                    RpcinteractID(124);
                }
                else
                {
                    CmdinteractID(124);
                }
                submidID = 124;
            }
            if (kasaDolu && anaKartDolu && ramDolu)
            {
                if (isServer)
                {
                    RpcinteractID(125);
                }
                else
                {
                    CmdinteractID(125);
                }
                submidID = 125;
            }
            if (kasaDolu && anaKartDolu && ekranKartýDolu && ramDolu)
            {
                if (isServer)
                {
                    RpcinteractID(1245);
                }
                else
                {
                    CmdinteractID(1245);
                }
                submidID = 1245;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ekranKartýDolu)
            {
                if (isServer)
                {
                    RpcinteractID(1234);
                }
                else
                {
                    CmdinteractID(1234);
                }
                submidID = 1234;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ramDolu)
            {
                if (isServer)
                {
                    RpcinteractID(1235);
                }
                else
                {
                    CmdinteractID(1235);
                }
                submidID = 1235;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ekranKartýDolu && ramDolu)
            {
                if (isServer)
                {
                    RpcinteractID(12345);
                }
                else
                {
                    CmdinteractID(12345);
                }
                submidID = 12345;
            }
        }
    }

    // bool deðiþkenlerin sunucuya gönderilmesi
    [Command(requiresAuthority = false)]
    public void CmdSetKasaDolu(bool newValue)
    {
        kasaDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetAnakartDolu(bool newValue)
    {
        anaKartDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetCpuDolu(bool newValue)
    {
        cpuDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetEkranKartýDolu(bool newValue)
    {
        ekranKartýDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetRamDolu(bool newValue)
    {
        ramDolu = newValue;
    }
}
