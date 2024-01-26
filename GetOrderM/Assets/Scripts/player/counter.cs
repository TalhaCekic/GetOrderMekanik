using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class counter : NetworkBehaviour
{
    [SyncVar] public bool kasaDolu = false;
    [SyncVar] public bool anaKartDolu = false;
    [SyncVar] public bool cpuDolu = false;
    [SyncVar] public bool ekranKartiDolu = false;
    [SyncVar] public bool ramDolu = false;

    [SyncVar] public bool BoxKasaDolu = false;
    [SyncVar] public bool BoxAnakartDolu = false;
    [SyncVar] public bool BoxCpuDolu = false;
    [SyncVar] public bool BoxEkranKartiDolu = false;
    [SyncVar] public bool BoxRamDolu = false;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> BoxObject = new List<GameObject>();
    public List<GameObject> idleObject = new List<GameObject>();

    [SyncVar] public float submidID = 0;

    public bool notCombine;

    public ParticleSystem smoke;
    public ParticleSystem affix;

    [SyncVar] public bool isControlledCounter = false;

    public void Start()
    {
        CmdAffixEffectStop();
        CmdSmokeEffectStop();
    }
    private void Update()
    {
        if (isServer)
        {
            cmdidCheck();
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
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
            for (int i = 0; i < BoxObject.Count; i++)
            {
                BoxObject[i].SetActive(false);
            }
            for (int i = 0; i < BoxObject.Count; i++)
            {
                idleObject[i].SetActive(false);
            }
            //  isControlledCounter = false;
        }
        if (submidID >= 12)
        {
            for (int i = 0; i < BoxObject.Count; i++)
            {
                idleObject[i].SetActive(false);
            }
        }
        if (objectNumber == 1.1f) // kasa kutu
        {
            BoxObject[1].SetActive(true);
            submidID = 1.1f;
        }
        if (objectNumber == 1) // kasa
        {
            submidID = 1;
            idleObject[1].SetActive(true);
            childObject[1].SetActive(false);
        }
        if (objectNumber == 2.1f) // anakar kutu
        {
            BoxObject[2].SetActive(true);
            submidID = 2.1f;
        }
        if (objectNumber == 2) // anakar
        {
            idleObject[2].SetActive(true);
            childObject[2].SetActive(false);
            submidID = 2;
        }
        if (objectNumber == 3.1f) // CPU kutu
        {
            BoxObject[3].SetActive(true);
            submidID = 3.1f;
        }
        if (objectNumber == 3) // CPU
        {
            idleObject[3].SetActive(true);
            childObject[3].SetActive(false);
            submidID = 3;
        }
        if (objectNumber == 4) // ekran kartı
        {
            idleObject[4].SetActive(true);
            childObject[4].SetActive(false);
            submidID = 4;
        }
        if (objectNumber == 4.1f) // ekran kartı
        {
            BoxObject[4].SetActive(true);
            submidID = 4.1f;
        }
        if (objectNumber == 5.1f) // ram
        {
            BoxObject[5].SetActive(true);
            submidID = 5.1f;
        }
        if (objectNumber == 5) // ram
        {
            idleObject[5].SetActive(true);
            childObject[5].SetActive(false);
            submidID = 5;
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

        // kontrol sonrası ID
        if (objectNumber == 1.2f) // kasa
        {
            submidID = 1.2f;
            idleObject[1].SetActive(true);
            childObject[1].SetActive(false);
        }
        if (objectNumber == 2.2f) // anakar
        {
            idleObject[2].SetActive(true);
            childObject[2].SetActive(false);
            submidID = 2.2f;
        }
        if (objectNumber == 3.2f) // CPU
        {
            idleObject[3].SetActive(true);
            childObject[3].SetActive(false);
            submidID = 3.2f;
        }
        if (objectNumber == 4.2f) // ekran kartı
        {
            idleObject[4].SetActive(true);
            childObject[4].SetActive(false);
            submidID = 4.2f;
        }
        if (objectNumber == 5.2f) // ram
        {
            idleObject[5].SetActive(true);
            childObject[5].SetActive(false);
            submidID = 5.2f;
        }
        if (objectNumber == 12.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            submidID = 12.2f;
        }
        if (objectNumber == 123.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            submidID = 123.2f;
        }
        if (objectNumber == 124.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 124.2f;
        }
        if (objectNumber == 125.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 125.2f;
        }
        if (objectNumber == 1234.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 1234.2f;
        }
        if (objectNumber == 1235.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 1235.2f;
        }
        if (objectNumber == 1245.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 1245.2f;
        }
        if (objectNumber == 12345.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 12345.2f;
        }
        if (objectNumber == 23)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            submidID = 23;
        }
        if (objectNumber == 24)
        {
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 24;
        }
        if (objectNumber == 25)
        {
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 25;
        }
        if (objectNumber == 234)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            submidID = 234;
        }
        if (objectNumber == 235)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 235;
        }
        if (objectNumber == 245)
        {
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 245;
        }
        if (objectNumber == 2345)
        {
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            submidID = 2345;
        }
    }
    [Command(requiresAuthority = false)]
    void cmdidCheck()
    {
        idCheck();
    }
    public void idCheck()
    {
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekranKartiDolu && !ramDolu || !BoxKasaDolu || !BoxAnakartDolu || !BoxCpuDolu || !BoxEkranKartiDolu || !BoxRamDolu)
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
        if (!isControlledCounter)
        {
            if (kasaDolu || anaKartDolu || cpuDolu || ekranKartiDolu || ramDolu)
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
                if (ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && ekranKartiDolu && ramDolu)
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
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu)
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
            if (kasaDolu || anaKartDolu || cpuDolu || ekranKartiDolu || ramDolu)
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
                if (ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && ekranKartiDolu && ramDolu)
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
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu)
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
                if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu)
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
        //kasasız kombinasyonlar
        if (anaKartDolu && cpuDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(23);
            }
            else
            {
                CmdinteractID(23);
            }
            submidID = 23;
        }
        if (anaKartDolu && ekranKartiDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(24);
            }
            else
            {
                CmdinteractID(24);
            }
            submidID = 24;
        }
        if (anaKartDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(25);
            }
            else
            {
                CmdinteractID(25);
            }
            submidID = 25;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(234);
            }
            else
            {
                CmdinteractID(234);
            }
            submidID = 234;
        }
        if (anaKartDolu && cpuDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(235);
            }
            else
            {
                CmdinteractID(235);
            }
            submidID = 235;
        }
        if (anaKartDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(245);
            }
            else
            {
                CmdinteractID(245);
            }
            submidID = 245;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
            if (isServer)
            {
                RpcinteractID(2345);
            }
            else
            {
                CmdinteractID(2345);
            }
            submidID = 2345;
        }
        if (BoxKasaDolu || BoxAnakartDolu || BoxCpuDolu || BoxEkranKartiDolu || BoxRamDolu)
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
                    RpcinteractID(2.1f);
                }
                else
                {
                    CmdinteractID(2.1f);
                }
            }
            if (BoxCpuDolu)
            {
                if (isServer)
                {
                    RpcinteractID(3.1f);
                }
                else
                {
                    CmdinteractID(3.1f);
                }
            }
            if (BoxEkranKartiDolu)
            {
                if (isServer)
                {
                    RpcinteractID(4.1f);
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
                    RpcinteractID(5.1f);
                }
                else
                {
                    CmdinteractID(5.1f);
                }
            }
        }
    }


    // effecetler parıltı
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
        ekranKartiDolu = newValue;
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
    public void CmdSetBoxEkranKartiDolu(bool newValue)
    {
        BoxEkranKartiDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetBoxRamDolu(bool newValue)
    {
        BoxRamDolu = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetisControlled(bool newValue)
    {
        isControlledCounter = newValue;
    }
    [Command(requiresAuthority = false)]
    public void CmdSetSubmidID(float newValue)
    {
        submidID = newValue;
    }
    
}
