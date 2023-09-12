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
    int pickupLayerMask4;

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
        pickupLayerMask4 = LayerMask.GetMask("submid");
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
                float value = counter.submidID;
                if (counter.submidID == 1.1f)
                {
                    counter.CmdSetBoxKasaDolu(false);
                    counter.CmdSetKasaDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.submidID == 2.2f)
                {
                    counter.CmdSetBoxAnakartDolu(false);
                    counter.CmdSetAnakartDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.submidID == 3.3f)
                {
                    counter.CmdSetBoxCpuDolu(false);
                    counter.CmdSetCpuDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.submidID == 4.4f)
                {
                    counter.CmdSetBoxEkranKartýDolu(false);
                    counter.CmdSetEkranKartýDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
                if (counter.submidID == 5.5f)
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
            // counter;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
            {
                if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == true)
                {
                    float value = counter.submidID;
                    if (ID == 1)
                    {
                        if (counter.submidID == 2 || counter.submidID == 23 || counter.submidID == 24 || counter.submidID == 234 || counter.submidID == 2345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.submidID == 0)
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
                        if (counter.submidID == 1 || counter.submidID == 3 || counter.submidID == 4 || counter.submidID == 5)
                        {
                            ID = 0;
                            counter.CmdSetAnakartDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.submidID == 0)
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
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 124 || counter.submidID == 125 || counter.submidID == 1245 || counter.submidID != 12345)
                        {
                            ID = 0;
                            counter.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 123 || counter.submidID == 125 || counter.submidID == 1235)
                        {
                            ID = 0;
                            counter.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 123 || counter.submidID == 124 || counter.submidID == 1234)
                        {
                            ID = 0;
                            counter.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 3 || counter.submidID == 4 || counter.submidID == 34 || counter.submidID == 345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }
                        else if (counter.submidID == 0)
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
                        if (counter.submidID == 4 || counter.submidID == 5)
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
                        else if (counter.submidID == 0)
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
                        if (counter.submidID == 5)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 3 || counter.submidID == 4)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 5)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 4)
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
                        if (counter.submidID == 0)
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
                        if (counter.submidID == 3)
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
                        if (counter.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartýDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 12345)
                    {
                        if (counter.submidID == 0)
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
                    float value = counter.submidID;
                    if (ID == 0)
                    {
                        if (counter.submidID == 1)
                        {
                            counter.CmdSetKasaDolu(false);
                            ID = 1;
                            handFull = true;
                            CmdinteractID(1);
                        }
                        if (counter.submidID == 1.1f)
                        {
                            counter.CmdSetBoxKasaDolu(false);
                            ID = 1.1f;
                            handFull = true;
                            CmdinteractID(1.1f);
                        }
                        if (counter.submidID == 2)
                        {
                            counter.CmdSetAnakartDolu(false);
                            ID = 2;
                            handFull = true;
                            CmdinteractID(2);
                        }
                        if (counter.submidID == 2.2f)
                        {
                            counter.CmdSetBoxAnakartDolu(false);
                            ID = 2.2f;
                            handFull = true;
                            CmdinteractID(2.2f);
                        }
                        if (counter.submidID == 3)
                        {
                            counter.CmdSetCpuDolu(false);
                            ID = 3;
                            handFull = true;
                            CmdinteractID(3);
                        }
                        if (counter.submidID == 3.3f)
                        {
                            counter.CmdSetBoxCpuDolu(false);
                            ID = 3.3f;
                            handFull = true;
                            CmdinteractID(3.3f);
                        }
                        if (counter.submidID == 4)
                        {
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 4;
                            handFull = true;
                            CmdinteractID(4);
                        }
                        if (counter.submidID == 4.4f)
                        {
                            counter.CmdSetBoxEkranKartýDolu(false);
                            ID = 4.4f;
                            handFull = true;
                            CmdinteractID(4.4f);
                        }
                        if (counter.submidID == 5)
                        {
                            counter.CmdSetRamDolu(false);
                            ID = 5;
                            handFull = true;
                            CmdinteractID(5);
                        }
                        if (counter.submidID == 5.5f)
                        {
                            counter.CmdSetBoxRamDolu(false);
                            ID = 5.5f;
                            handFull = true;
                            CmdinteractID(5.5f);
                        }
                        if (counter.submidID == 12)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            ID = 12;
                            handFull = true;
                            CmdinteractID(12);
                        }
                        if (counter.submidID == 123)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            ID = 123;
                            handFull = true;
                            CmdinteractID(123);
                        }
                        if (counter.submidID == 124)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 124;
                            handFull = true;
                            CmdinteractID(124);
                        }
                        if (counter.submidID == 125)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 125;
                            handFull = true;
                            CmdinteractID(125);
                        }
                        if (counter.submidID == 1234)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            ID = 1234;
                            handFull = true;
                            CmdinteractID(1234);
                        }
                        if (counter.submidID == 1245)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartýDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1245;
                            handFull = true;
                            CmdinteractID(1245);
                        }
                        if (counter.submidID == 1235)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1235;
                            handFull = true;
                            CmdinteractID(1235);
                        }
                        if (counter.submidID == 12345)
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
            // submid;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask4))
            {
                if (hit.collider.gameObject.TryGetComponent<DeliveryOrder>(out var DeliveryOrder) && handFull == true)
                {
                    float value = DeliveryOrder.submidID;
                    if (ID == 1)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 23 || DeliveryOrder.submidID == 24 || DeliveryOrder.submidID == 234 || DeliveryOrder.submidID == 2345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            //DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            //DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 2)
                    {
                        if (DeliveryOrder.submidID == 1 || DeliveryOrder.submidID == 3 || DeliveryOrder.submidID == 4 || DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            // counter.CmdAffixEffectPLAY();
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            // DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 3)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 || DeliveryOrder.submidID == 124 || DeliveryOrder.submidID == 125 || DeliveryOrder.submidID == 1245 || DeliveryOrder.submidID != 12345)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            // DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            //DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 4)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 || DeliveryOrder.submidID == 123 || DeliveryOrder.submidID == 125 || DeliveryOrder.submidID == 1235)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            // DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 5)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 || DeliveryOrder.submidID == 123 || DeliveryOrder.submidID == 124 || DeliveryOrder.submidID == 1234)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            //DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 12)
                    {
                        if (DeliveryOrder.submidID == 3 || DeliveryOrder.submidID == 4 || DeliveryOrder.submidID == 34 || DeliveryOrder.submidID == 345) //masa boþsa
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                            // DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            //  DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 123)
                    {
                        if (DeliveryOrder.submidID == 4 || DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            // DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            isWork = false;
                            // DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 124)
                    {
                        if (DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            // DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            // DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 125)
                    {
                        if (DeliveryOrder.submidID == 3 || DeliveryOrder.submidID == 4)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //   DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            //   DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1234)
                    {
                        if (DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //  DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            CmdinteractID(0);
                            //  DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1235)
                    {
                        if (DeliveryOrder.submidID == 4)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //  DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            //   DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 1245)
                    {
                        if (DeliveryOrder.submidID == 3)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            //   DeliveryOrder.CmdAffixEffectPLAY();
                        }
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            // DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                    if (ID == 12345)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartýDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            //  DeliveryOrder.CmdAffixEffectStop();
                        }
                    }
                }
                //tekrardan alma iþlemi
                if (hit.collider.gameObject.TryGetComponent<DeliveryOrder>(out var DeliveryOrder2) && handFull == false && retakeDelay <= 0)
                {
                    float value = DeliveryOrder.submidID;
                    if (ID == 0)
                    {
                        if (DeliveryOrder.submidID == 1)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            ID = 1;
                            handFull = true;
                            CmdinteractID(1);
                        }
                        if (DeliveryOrder.submidID == 2)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            ID = 2;
                            handFull = true;
                            CmdinteractID(2);
                        }
                        if (DeliveryOrder.submidID == 3)
                        {
                            DeliveryOrder.CmdSetCpuDolu(false);
                            ID = 3;
                            handFull = true;
                            CmdinteractID(3);
                        }
                        if (DeliveryOrder.submidID == 4)
                        {
                            DeliveryOrder.CmdSetEkranKartýDolu(false);
                            ID = 4;
                            handFull = true;
                            CmdinteractID(4);
                        }
                        if (DeliveryOrder.submidID == 5)
                        {
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 5;
                            handFull = true;
                            CmdinteractID(5);
                        }
                        if (DeliveryOrder.submidID == 12)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            ID = 12;
                            handFull = true;
                            CmdinteractID(12);
                        }
                        if (DeliveryOrder.submidID == 123)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            ID = 123;
                            handFull = true;
                            CmdinteractID(123);
                        }
                        if (DeliveryOrder.submidID == 124)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartýDolu(false);
                            ID = 124;
                            handFull = true;
                            CmdinteractID(124);
                        }
                        if (DeliveryOrder.submidID == 125)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 125;
                            handFull = true;
                            CmdinteractID(125);
                        }
                        if (DeliveryOrder.submidID == 1234)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetEkranKartýDolu(false);
                            ID = 1234;
                            handFull = true;
                            CmdinteractID(1234);
                        }
                        if (DeliveryOrder.submidID == 1245)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartýDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 1245;
                            handFull = true;
                            CmdinteractID(1245);
                        }
                        if (DeliveryOrder.submidID == 1235)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 1235;
                            handFull = true;
                            CmdinteractID(1235);
                        }
                        if (DeliveryOrder.submidID == 12345)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetEkranKartýDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
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



