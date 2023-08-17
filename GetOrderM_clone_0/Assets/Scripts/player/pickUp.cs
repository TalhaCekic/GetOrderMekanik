using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;

public class pickUp : NetworkBehaviour
{
    // karakterin elindeki tanýma ýd si0
    [SyncVar] public float ID;

    // public static pickUp instance;
    // RAY
    private RaycastHit hit;
    [SerializeField]
    [Min(10)]
    private float hitRange = 10000;

    int pickupLayerMask;
    int pickupLayerMask2;
    int pickupLayerMask3;

    public GameObject cam;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> BoxObject = new List<GameObject>();

    // elimizi kontrole eder.
    [SyncVar] public bool handFull = false;
    [SyncVar] public bool isWork = false;

    public controller playerInput;
    Animator anim;

    [SyncVar] public float retakeDelay = 0.5f;
    [SyncVar] public float workDelay = 1.1f;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        if (!isLocalPlayer) return;
    }
    void Start()
    {
        //en baþta objeleri sil
        for (int i = 0; i < childObject.Count; i++)
        {
            childObject[i].SetActive(false);
        }
        for (int i = 0; i < BoxObject.Count; i++)
        {
            BoxObject[i].SetActive(false);
        }

        workDelay = 1.1f;
        //tuþ etkileþimi
        if (!isLocalPlayer) return;
        playerInput = new controller();
        playerInput.Enable();
        playerInput.Player.Interact.performed += ctx => Interact();
        playerInput.Player.Drop.performed += ctx => secondsInteract();

        //layer atamalarý
        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        anim.SetBool("isWork", isWork);

        if (handFull)  // elde tutma ve tekrar almayý kontrol eder
        {
            anim.SetBool("hand", true);

            retakeDelay = 0.5f;
        }
        else
        {
            anim.SetBool("hand", false);
            if (retakeDelay > 0)
            {
                retakeDelay -= Time.deltaTime;
            }
            if (retakeDelay <= 0)
            {
                retakeDelay = 0;
            }
        }

        if (isWork) //el tamir animasyonunu kontrol eder
        {
            handFull = false;
            if (workDelay > 0)
            {
                workDelay -= Time.deltaTime;
            }
            if (workDelay <= 0)
            {
                workDelay = 0;
                isWork = false;
            }
        }
        else if (!isWork)
        {
            if (workDelay <= 0)
            {
                workDelay = 1.1f;
            }
        }

    }
    public void secondsInteract()
    {
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter))
            {
                float value = counter.counterID;
                if (counter.counterID == 1.1f)
                {
                    counter.CmdSetBoxKasaDolu(false);
                    counter.CmdSetKasaDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.counterID == 2.2f)
                {
                    counter.CmdSetBoxAnakartDolu(false);
                    counter.CmdSetAnakartDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.counterID == 3.3f)
                {
                    counter.CmdSetBoxCpuDolu(false);
                    counter.CmdSetCpuDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.counterID == 4.4f)
                {
                    counter.CmdSetBoxEkranKartýDolu(false);
                    counter.CmdSetEkranKartýDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.counterID == 5.5f)
                {
                    counter.CmdSetBoxRamDolu(false);
                    counter.CmdSetRamDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
            }
        }
    }
    public void Interact()
    {
        if (workDelay == 1.1f)
        {
            //  counter;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
            {
                if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == true)
                {
                    float value = counter.counterID;
                    if (ID == 1)
                    {
                        if (counter.counterID == 2 || counter.counterID == 23 || counter.counterID == 24 || counter.counterID == 234 || counter.counterID == 2345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 2)
                    {
                        if (counter.counterID == 1 || counter.counterID == 3 || counter.counterID == 4 || counter.counterID == 5)
                        {
                            ID = 0;
                            counter.CmdSetAnakartDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 3)
                    {
                        if (counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 124 || counter.counterID == 125 || counter.counterID == 1245 || counter.counterID != 12345)
                        {
                            ID = 0;
                            counter.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 4)
                    {
                        if (counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 123 || counter.counterID == 125 || counter.counterID == 1235)
                        {
                            ID = 0;
                            counter.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 5)
                    {
                        if (counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 123 || counter.counterID == 124 || counter.counterID == 1234)
                        {
                            ID = 0;
                            counter.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1.1f)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxKasaDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }
                    if (ID == 2.2f)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxAnakartDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }
                    if (ID == 3.3f)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxCpuDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }
                    if (ID == 4.4f)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxEkranKartýDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }
                    if (ID == 5.5f)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }
                    if (ID == 12)
                    {
                        if (counter.counterID == 3 || counter.counterID == 4 || counter.counterID == 34 || counter.counterID == 345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 123)
                    {
                        if (counter.counterID == 4 || counter.counterID == 5)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            isWork = false;
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 124)
                    {
                        if (counter.counterID == 5)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 125)
                    {
                        if (counter.counterID == 3 || counter.counterID == 4)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1234)
                    {
                        if (counter.counterID == 5)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1235)
                    {
                        if (counter.counterID == 4)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1245)
                    {
                        if (counter.counterID == 3)
                        {
                            ID = 0;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            counter.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 12345)
                    {
                        if (counter.counterID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                }
                //tekrardan alma iþlemi
                if (hit.collider.gameObject.TryGetComponent<counter>(out var counter2) && handFull == false && retakeDelay <= 0)
                {
                    float value = counter.counterID;
                    if (ID == 0)
                    {
                        if (counter.counterID == 1)
                        {
                            counter.CmdSetKasaDolu(false);
                            ID = 1;
                            handFull = true;
                            CmdinteractID(1);
                        }
                        if (counter.counterID == 1.1f)
                        {
                            counter.CmdSetBoxKasaDolu(false);
                            ID = 1.1f;
                            handFull = true;
                            CmdinteractID(1.1f);
                        }
                        if (counter.counterID == 2)
                        {
                            counter.CmdSetAnakartDolu(false);
                            ID = 2;
                            handFull = true;
                            CmdinteractID(2);
                        }
                        if (counter.counterID == 2.2f)
                        {
                            counter.CmdSetBoxAnakartDolu(false);
                            ID = 2.2f;
                            handFull = true;
                            CmdinteractID(2.2f);
                        }
                        if (counter.counterID == 3)
                        {
                            counter.CmdSetCpuDolu(false);
                            ID = 3;
                            handFull = true;
                            CmdinteractID(3);
                        }
                        if (counter.counterID == 3.3f)
                        {
                            counter.CmdSetBoxCpuDolu(false);
                            ID = 3.3f;
                            handFull = true;
                            CmdinteractID(3.3f);
                        }
                        if (counter.counterID == 4)
                        {
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 4;
                            handFull = true;
                            CmdinteractID(4);
                        }
                        if (counter.counterID == 4.4f)
                        {
                            counter.CmdSetBoxEkranKartýDolu(false);
                            ID = 4.4f;
                            handFull = true;
                            CmdinteractID(4.4f);
                        }
                        if (counter.counterID == 5)
                        {
                            counter.CmdSetRamDolu(false);
                            ID = 5;
                            handFull = true;
                            CmdinteractID(5);
                        }
                        if (counter.counterID == 5.5f)
                        {
                            counter.CmdSetBoxRamDolu(false);
                            ID = 5.5f;
                            handFull = true;
                            CmdinteractID(5.5f);
                        }
                        if (counter.counterID == 12)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            ID = 12;
                            handFull = true;
                            CmdinteractID(12);
                        }
                        if (counter.counterID == 123)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            ID = 123;
                            handFull = true;
                            CmdinteractID(123);
                        }
                        if (counter.counterID == 124)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 124;
                            handFull = true;
                            CmdinteractID(124);
                        }
                        if (counter.counterID == 125)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 125;
                            handFull = true;
                            CmdinteractID(125);
                        }
                        if (counter.counterID == 1234)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 1234;
                            handFull = true;
                            CmdinteractID(1234);
                        }
                        if (counter.counterID == 1245)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1245;
                            handFull = true;
                            CmdinteractID(1245);
                        }
                        if (counter.counterID == 1235)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1235;
                            handFull = true;
                            CmdinteractID(1235);
                        }
                        if (counter.counterID == 12345)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 12345;
                            handFull = true;
                            CmdinteractID(12345);
                        }
                    }
                }
            }

        }
        //ele obje alýmý;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask))
        {
            float id = hit.transform.GetComponent<itemID>().ItemID;
            if (id == 1 && handFull == false)
            {
                CmdinteractID(1.1f);
                handFull = true;
            }
            if (id == 2 && handFull == false)
            {
                CmdinteractID(2.2f);
                handFull = true;
            }
            if (id == 3 && handFull == false)
            {
                CmdinteractID(3.3f);
                handFull = true;
            }
            if (id == 4 && handFull == false)
            {
                CmdinteractID(4.4f);
                handFull = true;
            }
            if (id == 5 && handFull == false)
            {
                CmdinteractID(5.5f);
                handFull = true;
            }
        }
        //geri obje býrakmak;
        //if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask) && handFull)
        //{
        //    float id = hit.transform.GetComponent<itemID>().ItemID;
        //    if (id == 1 && ID == 1)
        //    {
        //        print("girmezmiisn");
        //        CmdinteractID(0);
        //        handFull = false;
        //    }
        //    if (id == 2 && ID == 2)
        //    {
        //        CmdinteractID(0);
        //        handFull = false;
        //    }
        //    if (id == 3 && ID == 3)
        //    {
        //        CmdinteractID(0);
        //        handFull = false;
        //    }
        //    if (id == 4 && ID == 4)
        //    {
        //        CmdinteractID(0);
        //        handFull = false;
        //    }
        //    if (id == 5 && ID == 5)
        //    {
        //        CmdinteractID(0);
        //        handFull = false;
        //    }
        //}
    }
    [Command]
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
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
            for (int i = 0; i < BoxObject.Count; i++)
            {
                BoxObject[i].SetActive(false);
            }
        }
        if (objectNumber == 1.1f) // kasa
        {
            BoxObject[1].SetActive(true);
            ID = 1.1f;
        }
        if (objectNumber == 1) // kasa
        {
            childObject[1].SetActive(true);
            ID = 1;
        }
        if (objectNumber == 2.2f) // anakar
        {
            BoxObject[2].SetActive(true);
            ID = 2.2f;
        }
        if (objectNumber == 2) // anakar
        {
            childObject[2].SetActive(true);
            ID = 2;
        }
        if (objectNumber == 3.3f) // CPU
        {
            BoxObject[3].SetActive(true);
            ID = 3.3f;
        }
        if (objectNumber == 3) // CPU
        {
            childObject[3].SetActive(true);
            ID = 3;
        }
        if (objectNumber == 4.4f) // ekran kartý
        {
            BoxObject[4].SetActive(true);
            ID = 4.4f;
        }
        if (objectNumber == 4) // ekran kartý
        {
            childObject[4].SetActive(true);
            ID = 4;
        }
        if (objectNumber == 5.5f) // ram
        {
            BoxObject[5].SetActive(true);
            ID = 5.5f;
        }
        if (objectNumber == 5) // ram
        {
            childObject[5].SetActive(true);
            ID = 5;
        }
        if (objectNumber == 12)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            ID = 12;
        }
        if (objectNumber == 123)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            ID = 123;
        }
        if (objectNumber == 124)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            ID = 124;
        }
        if (objectNumber == 125)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            ID = 125;
        }
        if (objectNumber == 1234)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            ID = 1234;
        }
        if (objectNumber == 1235)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            ID = 1235;
        }
        if (objectNumber == 1245)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            ID = 1245;
        }
        if (objectNumber == 12345)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            ID = 12345;
        }
    }
}



