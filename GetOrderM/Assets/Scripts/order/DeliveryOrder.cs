using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DeliveryOrder : NetworkBehaviour
{
    public static DeliveryOrder instance;

    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekrankartýDolu = false;
    [SyncVar] public bool ramDolu = false;

    public List<GameObject> childObject = new List<GameObject>();

    [SerializeField] private ManagerOrder managerOrder;
    [SyncVar] public int submidID;

    public int currentSubmidID;

    [SyncVar] public bool orderCorrect;

    [SyncVar] float resetDelay = 0.5f;
    [SyncVar] float lastResetTime = -1f;

    int currentobjectnumber;

    [SerializeField] private Transform target, target2;

    [SerializeField] public SyncList<GameObject> orderUI = new SyncList<GameObject>();


   [SyncVar] private int currentID;
    void Start()
    {
        instance = this;
        // managerOrder = FindAnyObjectByType<ManagerOrder>();
    }
    // Update is called once per frame
    void Update()
    {
        //cmdIdCheck(currentobjectnumber);
        //cmd(currentobjectnumber);
        
        if (isServer)
        {
            IdCheck();
            cmd(currentobjectnumber);
        }
    }
    //private void LateUpdate()
    //{
    //    if (isServer)
    //    {
    //        server(currentobjectnumber);
    //    }
    //}
    
    [Server]
    public void AddObjectToList(GameObject obj)
    {
        if (!orderUI.Contains(obj))
        {
            orderUI.Add(obj);
        }
    }
    [Command(requiresAuthority = false)]
    public void CmdinteractID(float objectNumber)
    {
        interactID(objectNumber);
        //RpcinteractID(objectNumber);
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
            print("sýfýrlamaaa");
            submidID = 0;
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
        }
        if (objectNumber == 1) // kasa
        {
            submidID = 1;
            childObject[1].SetActive(true);
        }
        if (objectNumber == 2) // anakar
        {
            childObject[2].SetActive(true);
            submidID = 2;
        }
        if (objectNumber == 3) // CPU
        {
            childObject[3].SetActive(true);
            submidID = 3;
        }
        if (objectNumber == 4) // ekran kartï¿½
        {
            childObject[4].SetActive(true);
            submidID = 4;
        }
        if (objectNumber == 5) // ram
        {
            childObject[5].SetActive(true);
            submidID = 5;
        }
        if (objectNumber == 12)
        {
            submidID = 12;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    submidID = 0;

                });
            }
        }
        if (objectNumber == 123)
        {
            submidID = 123;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[3].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    cpuDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[3].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 124)
        {
            submidID = 124;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    ekrankartýDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[4].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 125)
        {
            submidID = 125;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    ramDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[5].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 1234)
        {
            submidID = 1234;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[3].gameObject.transform.DOMove(target.position, 2);
                childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    cpuDolu = false;
                    ekrankartýDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[3].transform.DOMove(target2.position, 1);
                    childObject[4].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 1235)
        {
            submidID = 1235;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[3].gameObject.transform.DOMove(target.position, 2);
                childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    cpuDolu = false;
                    ramDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[3].transform.DOMove(target2.position, 1);
                    childObject[5].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 1245)
        {
            submidID = 1245;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[4].gameObject.transform.DOMove(target.position, 2);
                childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    ekrankartýDolu = false;
                    ramDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[4].transform.DOMove(target2.position, 1);
                    childObject[5].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
        if (objectNumber == 12345)
        {
            submidID = 12345;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            if (orderCorrect)
            {
                childObject[1].gameObject.transform.DOMove(target.position, 2);
                childObject[2].gameObject.transform.DOMove(target.position, 2);
                childObject[3].gameObject.transform.DOMove(target.position, 2);
                childObject[4].gameObject.transform.DOMove(target.position, 2);
                childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
                {
                    kasaDolu = false;
                    anaKartDolu = false;
                    cpuDolu = false;
                    ekrankartýDolu = false;
                    ramDolu = false;
                    childObject[1].transform.DOMove(target2.position, 1);
                    childObject[2].transform.DOMove(target2.position, 1);
                    childObject[3].transform.DOMove(target2.position, 1);
                    childObject[4].transform.DOMove(target2.position, 1);
                    childObject[5].transform.DOMove(target2.position, 1);
                    submidID = 0;
                });
            }
        }
    }
    [Command(requiresAuthority = false)]
    void cmdIdCheck()
    {
        IdCheck();
        //RpcIdCheck(currentobjectnumber);
    }
    [Server]
    public void IdCheck()
    {
        //if (Time.time - lastResetTime > resetDelay)
        //{
        //    for (int i = 0; i < managerOrder.orderArray.Count; i++)
        //    {
        //        if (managerOrder.orderArray[i] == submidID)
        //        {
        //            managerOrder.orderArray[i] = 1;
        //            currentobjectnumber = 0;
        //        //    orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;
        //            lastResetTime = Time.time;
        //            orderCorrect = true;

        //            break;
        //        }
        //        else
        //        {
        //            orderCorrect = false;
        //        }
        //    }
        //}

        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekrankartýDolu && !ramDolu)
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
        if (kasaDolu || anaKartDolu || cpuDolu || ekrankartýDolu || ramDolu)
        {
            if (kasaDolu)
            {
                if (isServer)
                {
                    RpcinteractID(1);
                }
                else
                {
                    CmdinteractID(1);
                }
            }
            if (anaKartDolu)
            {
                if (isServer)
                {
                    RpcinteractID(2);
                }
                else
                {
                    CmdinteractID(2);
                }
            }
            if (cpuDolu)
            {
                if (isServer)
                {
                    RpcinteractID(3);
                }
                else
                {
                    CmdinteractID(3);
                }
            }
            if (ekrankartýDolu)
            {
                if (isServer)
                {
                    RpcinteractID(4);
                }
                else
                {
                    CmdinteractID(4);
                }
            }
            if (ramDolu)
            {
                if (isServer)
                {
                    RpcinteractID(5);
                }
                else
                {
                    CmdinteractID(5);
                }
            }
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
            }
            if (kasaDolu && anaKartDolu && ekrankartýDolu)
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
            }
            if (kasaDolu && anaKartDolu && ekrankartýDolu && ramDolu)
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
            if (kasaDolu && anaKartDolu && cpuDolu && ekrankartýDolu)
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
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ekrankartýDolu && ramDolu)
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

    [Command(requiresAuthority = false)]
    public void cmd(int currentobjectnumber)
    {
        server(currentobjectnumber);
    }
    [Server] 
    public void server(int currentobjectnumber) 
    {
        
        // rpc(currentobjectnumber);
        if (Time.time - lastResetTime > resetDelay)
        {
            
            for (int i = 0; i < managerOrder.orderArray.Count; i++)
            {
                if (managerOrder.orderArray[i] == submidID)
                {
                   
                    currentobjectnumber = 0;
                    orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;
                    orderUI.Remove(orderUI[i]);
                    lastResetTime = Time.time;
                    orderCorrect = true;
                    managerOrder.orderArray[i] = currentID;
                    break;
                }
                else
                {
                    orderCorrect = false;
                }

            }   
                currentID = 1;
            
        }
    }
    [ClientRpc]
    public void rpc(int currentobjectnumber)
    {
        //if (Time.time - lastResetTime > resetDelay)
        //{
        //    for (int i = 0; i < managerOrder.orderArray.Count; i++)
        //    {
        //        if (managerOrder.orderArray[i] == submidID)
        //        {
        //            managerOrder.orderArray[i] = 1;
        //            currentobjectnumber = 0;
        //            orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;
        //            lastResetTime = Time.time;
        //            orderCorrect = true;
        //            break;
        //        }
        //        else
        //        {
        //            orderCorrect = false;
        //        }
        //    }
        //}
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
        ekrankartýDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetRamDolu(bool newValue)
    {
        ramDolu = newValue;
    }
}
