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
    [SyncVar] public bool ekrankartiDolu = false;
    [SyncVar] public bool ramDolu = false;

    public List<GameObject> childObject = new List<GameObject>();
    
    [SyncVar] public float submidID;
    
    [SyncVar] public bool submid;

    [SyncVar] public bool orderCorrect;
    [SyncVar] public bool notOrderCorrect;

    [SyncVar] float resetDelay = 0.5f;
    [SyncVar] public float lastResetTime = -1f;

    [SyncVar] public int currentobjectnumber;

    [SerializeField] private Transform target, target2;

    [SyncVar] public bool isControlledCounter = false;

    void Start()
    {
        instance = this;
        submid = false;
        notOrderCorrect = false;
    }

    void Update()
    {
        if (isServer)
        {
            IdCheck();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdinteractID(float objectNumber)
    {
        interactID(objectNumber);
    }

    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        interactID(objectNumber);
    }

    public void interactID(float objectNumber)
    {
        if (objectNumber == 0) // boş
        {
            submidID = 0;
            notOrderCorrect = false;
            orderCorrect = false;
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
            
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            { 
                kasaDolu = false;
                anaKartDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
            });
        }

        if (objectNumber == 123)
        {
            submidID = 123;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
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
                
            });
        }

        if (objectNumber == 124)
        {
            submidID = 124;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                ekrankartiDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
               
            });
        }

        if (objectNumber == 125)
        {
            submidID = 125;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            
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
              
            });
        }

        if (objectNumber == 1234)
        {
            submidID = 1234;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);

            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[3].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                cpuDolu = false;
                ekrankartiDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[3].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                
            });
        }

        if (objectNumber == 1235)
        {
            submidID = 1235;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);

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
                
            });
        }

        if (objectNumber == 1245)
        {
            submidID = 1245;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
  
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2);
            childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                ekrankartiDolu = false;
                ramDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                childObject[5].transform.DOMove(target2.position, 1);
                
            });
        }

        if (objectNumber == 12345)
        {
            submidID = 12345;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
 
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[3].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2);
            childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                cpuDolu = false;
                ekrankartiDolu = false;
                ramDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[3].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                childObject[5].transform.DOMove(target2.position, 1);
                
            });
        }

        // kontrol sonrası
        if (objectNumber == 1.2f) // kasa
        {
            submidID = 1;
            childObject[1].SetActive(true);
        }

        if (objectNumber == 2.2f) // anakar
        {
            childObject[2].SetActive(true);
            submidID = 2;
        }

        if (objectNumber == 3.2f) // CPU
        {
            childObject[3].SetActive(true);
            submidID = 3;
        }

        if (objectNumber == 4.2f) // ekran kartï¿½
        {
            childObject[4].SetActive(true);
            submidID = 4;
        }

        if (objectNumber == 5.2f) // ram
        {
            childObject[5].SetActive(true);
            submidID = 5;
        }

        if (objectNumber == 12.2f)
        {
            submidID = 12;
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
        }

        if (objectNumber == 123.2f)
        {
            submidID = 123;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
     
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
                
            });
        }

        if (objectNumber == 124.2f)
        {
            submidID = 124;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
     
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                ekrankartiDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
               
            });
        }

        if (objectNumber == 125.2f)
        {
            submidID = 125;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
      
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
                
            });
        }

        if (objectNumber == 1234.2f)
        {
            submidID = 1234;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
     
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[3].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                cpuDolu = false;
                ekrankartiDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[3].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                
            });
        }

        if (objectNumber == 1235.2f)
        {
            submidID = 1235;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
          
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
                
            });
        }

        if (objectNumber == 1245.2f)
        {
            submidID = 1245;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
         
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2);
            childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                ekrankartiDolu = false;
                ramDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                childObject[5].transform.DOMove(target2.position, 1);
                
            });
        }

        if (objectNumber == 12345.2f)
        {
            submidID = 12345;
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
        
            childObject[1].gameObject.transform.DOMove(target.position, 2);
            childObject[2].gameObject.transform.DOMove(target.position, 2);
            childObject[3].gameObject.transform.DOMove(target.position, 2);
            childObject[4].gameObject.transform.DOMove(target.position, 2);
            childObject[5].gameObject.transform.DOMove(target.position, 2).OnComplete(() =>
            {
                kasaDolu = false;
                anaKartDolu = false;
                cpuDolu = false;
                ekrankartiDolu = false;
                ramDolu = false;
                childObject[1].transform.DOMove(target2.position, 1);
                childObject[2].transform.DOMove(target2.position, 1);
                childObject[3].transform.DOMove(target2.position, 1);
                childObject[4].transform.DOMove(target2.position, 1);
                childObject[5].transform.DOMove(target2.position, 1);
                
            });
            if (objectNumber >= 12 && notOrderCorrect)
            {
                notOrderCorrect = false;
                submidID = 0;
            }
        }
    }

    [Command(requiresAuthority = false)]
    void cmdIdCheck()
    {
        IdCheck();
    }

    [Server]
    public void IdCheck()
    {
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekrankartiDolu && !ramDolu)
        {
            isControlledCounter = false;
            if (isServer)
            {
                RpcinteractID(0);
            }
            else
            {
                CmdinteractID(0);
            }
        }

        if (!isControlledCounter)
        {
            if (kasaDolu || anaKartDolu || cpuDolu || ekrankartiDolu || ramDolu)
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

                    submidID = 3;
                }

                if (ekrankartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(4);
                    }
                    else
                    {
                        CmdinteractID(4);
                    }

                    submidID = 4;
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

                    submidID = 5;
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
                  //  submidID = 0;
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

                if (kasaDolu && anaKartDolu && ekrankartiDolu)
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

                if (kasaDolu && anaKartDolu && ekrankartiDolu && ramDolu)
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

                if (kasaDolu && anaKartDolu && cpuDolu && ekrankartiDolu)
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

                if (kasaDolu && anaKartDolu && cpuDolu && ekrankartiDolu && ramDolu)
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

        if (isControlledCounter)
        {
            if (kasaDolu || anaKartDolu || cpuDolu || ekrankartiDolu || ramDolu)
            {
                if (kasaDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1.2f);
                    }
                    else
                    {
                        CmdinteractID(1.2f);
                    }
                }

                if (anaKartDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(2.2f);
                    }
                    else
                    {
                        CmdinteractID(2.2f);
                    }
                }

                if (cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(3.2f);
                    }
                    else
                    {
                        CmdinteractID(3.2f);
                    }
                }

                if (ekrankartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(4.2f);
                    }
                    else
                    {
                        CmdinteractID(4.2f);
                    }
                }

                if (ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(5.2f);
                    }
                    else
                    {
                        CmdinteractID(5.2f);
                    }
                }

                if (kasaDolu && anaKartDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12.2f);
                    }
                    else
                    {
                        CmdinteractID(12.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && cpuDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(123.2f);
                    }
                    else
                    {
                        CmdinteractID(123.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && ekrankartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(124.2f);
                    }
                    else
                    {
                        CmdinteractID(124.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(125.2f);
                    }
                    else
                    {
                        CmdinteractID(125.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && ekrankartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1245.2f);
                    }
                    else
                    {
                        CmdinteractID(1245.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && cpuDolu && ekrankartiDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1234.2f);
                    }
                    else
                    {
                        CmdinteractID(1234.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && cpuDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(1235.2f);
                    }
                    else
                    {
                        CmdinteractID(1235.2f);
                    }
                }

                if (kasaDolu && anaKartDolu && cpuDolu && ekrankartiDolu && ramDolu)
                {
                    if (isServer)
                    {
                        RpcinteractID(12345.2f);
                    }
                    else
                    {
                        CmdinteractID(12345.2f);
                    }
                }
            }
        }
    }

    // bool değişkenlerin sunucuya gönderilmesi
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
    public void CmdSetEkranKartiDolu(bool newValue)
    {
        ekrankartiDolu = newValue;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetRamDolu(bool newValue)
    {
        ramDolu = newValue;
    }

    [Command(requiresAuthority = false)]
    public void CmdorderCorrect(bool newValue)
    {
        orderCorrect = newValue;
    }   
    [Command(requiresAuthority = false)]
    public void CmdnotOrderCorrect(bool newValue)
    {
        notOrderCorrect = newValue;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetisControlled(bool newValue)
    {
        isControlledCounter = newValue;
    } 
    [Command(requiresAuthority = false)]
    public void CmdSetSubmid(bool newValue)
    {
        submid = newValue;
    }
}