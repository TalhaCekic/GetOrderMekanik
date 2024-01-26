
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Tutorialcounter : MonoBehaviour
{
     public bool kasaDolu = false;
    public bool anaKartDolu = false;
     public bool cpuDolu = false;
     public bool ekranKartiDolu = false;
     public bool ramDolu = false;

     public bool BoxKasaDolu = false;
     public bool BoxAnakartDolu = false;
     public bool BoxCpuDolu = false;
    public bool BoxEkranKartiDolu = false;
     public bool BoxRamDolu = false;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> BoxObject = new List<GameObject>();
    public List<GameObject> idleObject = new List<GameObject>();

    public float submidID = 0;

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
      
            RpcidCheck();
        
    }
    
    public void CmdinteractID(float objectNumber)
    {
       // interactID(objectNumber);
        RpcinteractID(objectNumber);
    }
   
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
            for (int i = 0; i < BoxObject.Count; i++)
            {
                BoxObject[i].SetActive(false);
            }
            for (int i = 0; i < BoxObject.Count; i++)
            {
                idleObject[i].SetActive(false);
            }
        }
        if(submidID >=12)
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
        if (objectNumber == 2.2f) // anakar kutu
        {
            BoxObject[2].SetActive(true);
            submidID = 2.2f;
        }
        if (objectNumber == 2) // anakar
        {
            idleObject[2].SetActive(true);
            childObject[2].SetActive(false);
            submidID = 2;
        }
        if (objectNumber == 3.3f) // CPU kutu
        {
            BoxObject[3].SetActive(true);
            submidID = 3.3f;
        }
        if (objectNumber == 3) // CPU
        {
            idleObject[3].SetActive(true) ;
            childObject[3].SetActive(false);
            submidID = 3;
        }
        if (objectNumber == 4) // ekran kartý
        {
            idleObject[4].SetActive(true);
            childObject[4].SetActive(false);
            submidID = 4;
        }
        if (objectNumber == 4.4f) // ekran kartý
        {
            BoxObject[4].SetActive(true);
            submidID = 4.4f;
        }
        if (objectNumber == 5.5f) // ram
        {
            BoxObject[5].SetActive(true);
            submidID = 5.5f;
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
   
    void cmdidCheck()
    {
        RpcidCheck();
    }
   
    void RpcidCheck()
    {
        idCheck();
    }
    public void idCheck()
    {
        if (!kasaDolu && !anaKartDolu && !cpuDolu && !ekranKartiDolu && !ramDolu || !BoxKasaDolu || !BoxAnakartDolu || !BoxCpuDolu || !BoxEkranKartiDolu || !BoxRamDolu)
        {
            
                RpcinteractID(0);
           
        }
        if (kasaDolu || anaKartDolu || cpuDolu || ekranKartiDolu || ramDolu)
        {
            if (kasaDolu)
            {
                
                
                    RpcinteractID(1);
               
            }
            if (anaKartDolu)
            {
               
                    RpcinteractID(2);
                
            }
            if (cpuDolu)
            {
               
                    RpcinteractID(3);
               
               
                submidID = 3;
            }
            if (ekranKartiDolu)
            {
               
                    RpcinteractID(4);
                
              
                submidID = 4;
            }
            if (ramDolu)
            {
               
                    RpcinteractID(5);
                
                submidID = 5;
            }
            if (kasaDolu && anaKartDolu)
            {
                
                    RpcinteractID(12);
                
                
                submidID = 12;
            }
            if (kasaDolu && anaKartDolu && cpuDolu)
            {
                
                    RpcinteractID(123);
                
                submidID = 123;
            }
            if (kasaDolu && anaKartDolu && ekranKartiDolu)
            {
               
                    RpcinteractID(124);
                
               
                submidID = 124;
            }
            if (kasaDolu && anaKartDolu && ramDolu)
            {
                
                    RpcinteractID(125);
              
                submidID = 125;
            }
            if (kasaDolu && anaKartDolu && ekranKartiDolu && ramDolu)
            {
                
                    RpcinteractID(1245);
               
                submidID = 1245;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu)
            {
               
                    RpcinteractID(1234);
                
                
                submidID = 1234;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ramDolu)
            {
                
                    RpcinteractID(1235);
               
                submidID = 1235;
            }
            if (kasaDolu && anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu)
            {
               
                    RpcinteractID(12345);
               
                submidID = 12345;
            }
        }
        //kasasýz kombinasyonlar
        if (anaKartDolu && cpuDolu && !kasaDolu)
        {
           
                RpcinteractID(23);
          
            submidID = 23;
        }
        if (anaKartDolu && ekranKartiDolu && !kasaDolu)
        {
            
                RpcinteractID(24);
            
            submidID = 24;
        }
        if (anaKartDolu && ramDolu && !kasaDolu)
        {
            
                RpcinteractID(25);
          
            submidID = 25;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && !kasaDolu)
        {
            
                RpcinteractID(234);
          
            submidID = 234;
        }
        if (anaKartDolu && cpuDolu && ramDolu && !kasaDolu)
        {
            
                RpcinteractID(235);
           
            submidID = 235;
        }
        if (anaKartDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
            
                RpcinteractID(245);
            
            submidID = 245;
        }
        if (anaKartDolu && cpuDolu && ekranKartiDolu && ramDolu && !kasaDolu)
        {
           
                RpcinteractID(2345);
            
           
            submidID = 2345;
        }
        if (BoxKasaDolu || BoxAnakartDolu || BoxCpuDolu || BoxEkranKartiDolu || BoxRamDolu)
        {
            if (BoxKasaDolu)
            {
                
                    RpcinteractID(1.1f);
                
            }
            if (BoxAnakartDolu)
            {
               
                    RpcinteractID(2.2f);
               
            }
            if (BoxCpuDolu)
            {
                
                    RpcinteractID(3.3f);
               
            }
            if (BoxEkranKartiDolu)
            {
               
                    RpcinteractID(4.4f);
                
            }
            if (BoxRamDolu)
            {
               
                    RpcinteractID(5.5f);
               
            }
        }
    }


    // effecetler parýltý
    //play
    public void CmdAffixEffectPLAY()
    {
        RpcAffixEffectPlay();
    }

    public void RpcAffixEffectPlay()
    {
        affix.Play();
    }   
     //stop
    public void CmdAffixEffectStop()
    {
        RpcAffixEffectStop();
    }

    public void RpcAffixEffectStop()
    {
        affix.Stop();
    } 

    // effecetler smoke
     //play
    public void CmdSmokeEffectPLAY()
    {
        RpcSmokeEffectPlay();
    }
   
    public void RpcSmokeEffectPlay()
    {
        smoke.Play();
    }   
     //stop
    public void CmdSmokeEffectStop()
    {
        RpcSmokeEffectStop();
    }
 
    public void RpcSmokeEffectStop()
    {
        smoke.Stop();
    }

    // bool deðiþkenlerin sunucuya gönderilmesi
    
    public void CmdSetKasaDolu(bool newValue)
    {
        kasaDolu = newValue;
    }

    public void CmdSetAnakartDolu(bool newValue)
    {
        anaKartDolu = newValue;
    }

    public void CmdSetCpuDolu(bool newValue)
    {
        cpuDolu = newValue;
    }

    public void CmdSetEkranKartiDolu(bool newValue)
    {
        ekranKartiDolu = newValue;
    }
    
    public void CmdSetRamDolu(bool newValue)
    {
        ramDolu = newValue;
    }
  
    public void CmdSetBoxKasaDolu(bool newValue)
    {
        BoxKasaDolu = newValue;
    }
    
    public void CmdSetBoxAnakartDolu(bool newValue)
    {
        BoxAnakartDolu = newValue;
    }

    public void CmdSetBoxCpuDolu(bool newValue)
    {
        BoxCpuDolu = newValue;
    }

    public void CmdSetBoxEkranKartiDolu(bool newValue)
    {
        BoxEkranKartiDolu = newValue;
    }

    public void CmdSetBoxRamDolu(bool newValue)
    {
        BoxRamDolu = newValue;
    }

}
