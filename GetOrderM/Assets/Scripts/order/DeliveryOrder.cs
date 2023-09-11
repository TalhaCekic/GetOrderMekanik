using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using DG.Tweening;


public class DeliveryOrder : NetworkBehaviour
{
    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekranKartýDolu = false;
    [SyncVar] public bool ramDolu = false;

    public List<GameObject> childObject = new List<GameObject>();

    [SerializeField] private ManagerOrder managerOrder;
    [SyncVar] public int submidID;

    public int currentSubmidID;

    float resetDelay = 0.5f;
    float lastResetTime = -1f;

    int currentobjectnumber;

    [SerializeField] private Transform target, target2;

    [SerializeField] private GameObject[] orderUI;

    void Start()
    {
        managerOrder = FindAnyObjectByType<ManagerOrder>();

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
      
        //objectNumber = currentobjectnumber;
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
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);

            });
           
            // submidID = 12;
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
       // orderUI = GameObject.FindGameObjectsWithTag("orderUI");
        if (Time.time - lastResetTime > resetDelay)
        {
            for (int i = 0; i < managerOrder.orderArray.Length; i++)
            {
                
                if (managerOrder.orderArray[i] == submidID)
                {
                    managerOrder.orderArray[i] = 1;
                    currentobjectnumber = 0;                 
                    lastResetTime = Time.time;
                    break;
                }
            }
        }


        // if (submidID == managerOrder.orderArray[0])
        // {
        //     // Debug.Log(string.Format("Dizin {0} deðeri {1}'e eþittir.", currentSubmidID));

        //     managerOrder.orderArray[0] = 0;

        // }

        // else if (managerOrder.orderArray[1] == submidID)
        // {
        //     // Debug.Log(string.Format("Dizin {0} deðeri {1}'e eþittir.", currentSubmidID));
        //     managerOrder.orderArray[1] = 0;

        // }
        // else if (managerOrder.orderArray[2] == submidID)
        // {
        //     //Debug.Log(string.Format("Dizin {0} deðeri {1}'e eþittir.", currentSubmidID));
        //     managerOrder.orderArray[2] = 0;

        // }
        // else if (managerOrder.orderArray[3] == submidID)
        // {
        //     // Debug.Log(string.Format("Dizin {0} deðeri {1}'e eþittir.", currentSubmidID));
        //     managerOrder.orderArray[3] = 0;

        // }
        // else if (managerOrder.orderArray[4] == submidID)
        // {
        //     // Debug.Log(string.Format("Dizin {0} deðeri {1}'e eþittir.", currentSubmidID));
        //     managerOrder.orderArray[4] = 0;

        //}







        if (currentobjectnumber==0)
        {
            if (isServer)
            {
               
                RpcinteractID(0);
            }
            else if (isClient)
            {
                CmdinteractID(0);
            }
            submidID = 0;
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
