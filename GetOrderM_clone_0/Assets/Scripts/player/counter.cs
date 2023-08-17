using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class counter : NetworkBehaviour
{
    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekranKartýDolu = false;
    [SyncVar] public bool ramDolu = false;

    [SyncVar] public bool BoxKasaDolu = false;
    [SyncVar] public bool BoxAnakartDolu = false;
    [SyncVar] public bool BoxCpuDolu = false;
    [SyncVar] public bool BoxEkranKartýDolu = false;
    [SyncVar] public bool BoxRamDolu = false;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> BoxObject = new List<GameObject>();

    [SyncVar] public float counterID = 0;

    public bool notCombine;

    public ParticleSystem smoke;
    public ParticleSystem affix;

    public void Start()
    {
        CmdAffixEffectStop();
        CmdSmokeEffectStop();
    }
    private void Update()
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
            counterID = 0;
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
            for (int i = 0; i < BoxObject.Count; i++)
            {
                BoxObject[i].SetActive(false);
            }
        }
        if (objectNumber == 1.1f) // kasa kutu
        {
            BoxObject[1].SetActive(true);
            counterID = 1.1f;
        }
        if (objectNumber == 1) // kasa
        {
            counterID = 1;
            childObject[1].SetActive(true);
        }
        if (objectNumber == 2.2f) // anakar kutu
        {
            BoxObject[2].SetActive(true);
            counterID = 2.2f;
        }
        if (objectNumber == 2) // anakar
        {
            childObject[2].SetActive(true);
            counterID = 2;
            //  smoke.Play();
        }
        if (objectNumber == 3.3f) // CPU kutu
        {
            BoxObject[3].SetActive(true);
            counterID = 3.3f;
        }
        if (objectNumber == 3) // CPU
        {
            childObject[3].SetActive(true);
            counterID = 3;
            //  smoke.Play();
        }
        if (objectNumber == 4) // ekran kartý
        {
            childObject[4].SetActive(true);
            counterID = 4;
            //  smoke.Play();
        }
        if (objectNumber == 4.4f) // ekran kartý
        {
            BoxObject[4].SetActive(true);
            counterID = 4.4f;
            //  smoke.Play();
        }
        if (objectNumber == 5.5f) // ram
        {
            BoxObject[5].SetActive(true);
            counterID = 5.5f;
        }
        if (objectNumber == 5) // ram
        {
            childObject[5].SetActive(true);
            counterID = 5;
            //  smoke.Play();
        }
        if (objectNumber == 12)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            counterID = 12;
            //  smoke.Play();
        }
        if (objectNumber == 123)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            counterID = 123;
            // smoke.Play();
        }
        if (objectNumber == 124)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            counterID = 124;
        }
        if (objectNumber == 125)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            counterID = 125;
        }
        if (objectNumber == 1234)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            counterID = 1234;
        }
        if (objectNumber == 1235)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            counterID = 1235;
        }
        if (objectNumber == 1245)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            counterID = 1245;
        }
        if (objectNumber == 12345)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            counterID = 12345;
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
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekranKartýDolu && !ramDolu || !BoxKasaDolu || !BoxAnakartDolu || !BoxCpuDolu || !BoxEkranKartýDolu || !BoxRamDolu)
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
                counterID = 3;
            }
            if (ekranKartýDolu)
            {
                if (isServer)
                {
                    RpcinteractID(4);
                }
                else
                {
                    CmdinteractID(4);
                }
                counterID = 4;
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
                counterID = 5;
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
                counterID = 12;
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
                counterID = 123;
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
                counterID = 124;
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
                counterID = 125;
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
                counterID = 1245;
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
                counterID = 1234;
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
                counterID = 1235;
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
                counterID = 12345;
            }
        }
        if (BoxKasaDolu || BoxAnakartDolu || BoxCpuDolu || BoxEkranKartýDolu || BoxRamDolu)
        {
            if (BoxKasaDolu)
            {
                if (isServer)
                {
                    RpcinteractID(1.1f);
                }
                else
                {
                    CmdinteractID(1.1f);
                }
            }
            if (BoxAnakartDolu)
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
            if (BoxCpuDolu)
            {
                if (isServer)
                {
                    RpcinteractID(3.3f);
                }
                else
                {
                    CmdinteractID(3.3f);
                }
            }
            if (BoxEkranKartýDolu)
            {
                if (isServer)
                {
                    RpcinteractID(4.4f);
                }
                else
                {
                    CmdinteractID(4.4f);
                }
            }
            if (BoxRamDolu)
            {
                if (isServer)
                {
                    RpcinteractID(5.5f);
                }
                else
                {
                    CmdinteractID(5.5f);
                }
            }
        }
    }


    // effecetler parýltý
    [Command(requiresAuthority = false)] //play
    public void CmdAffixEffectPLAY()
    {
        RpcAffixEffectPlay();
    }
    [ClientRpc]
    public void RpcAffixEffectPlay()
    {
        affix.Play();
    }   
    [Command(requiresAuthority = false)] //stop
    public void CmdAffixEffectStop()
    {
        RpcAffixEffectStop();
    }
    [ClientRpc]
    public void RpcAffixEffectStop()
    {
        affix.Stop();
    } 

    // effecetler smoke
    [Command(requiresAuthority = false)] //play
    public void CmdSmokeEffectPLAY()
    {
        RpcSmokeEffectPlay();
    }
    [ClientRpc]
    public void RpcSmokeEffectPlay()
    {
        smoke.Play();
    }   
    [Command(requiresAuthority = false)] //stop
    public void CmdSmokeEffectStop()
    {
        RpcSmokeEffectStop();
    }
    [ClientRpc]
    public void RpcSmokeEffectStop()
    {
        smoke.Stop();
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
    [Command(requiresAuthority = false)]
    public void CmdSetBoxKasaDolu(bool newValue)
    {
        BoxKasaDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetBoxAnakartDolu(bool newValue)
    {
        BoxAnakartDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetBoxCpuDolu(bool newValue)
    {
        BoxCpuDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetBoxEkranKartýDolu(bool newValue)
    {
        BoxEkranKartýDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetBoxRamDolu(bool newValue)
    {
        BoxRamDolu = newValue;
    }

}
