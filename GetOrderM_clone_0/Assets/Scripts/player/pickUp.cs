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
    [SyncVar] public int ID;

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

    [SyncVar] public bool kasaGonder;
    [SyncVar] public bool anakartGonder;
    [SyncVar] public bool cpuGonder;
    [SyncVar] public bool ekranKartýGonder;
    [SyncVar] public bool ramGonder;
    [SyncVar] public bool tomatoSlicegonder;
    [SyncVar] public bool lettuceogonder;
    [SyncVar] public bool lettuceSlicegonder;

    // elimizi kontrole eder.
    [SyncVar] public bool handFull = false;
    [SyncVar] public bool isWork = false;

    public controller playerInput;
    Animator anim;

    [SyncVar] public float retakeDelay = 0.5f;
    [SyncVar] public float workDelay = 1f;
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
        //tuþ etkileþimi
        if (!isLocalPlayer) return;
        playerInput = new controller();
        playerInput.Enable();
        playerInput.Player.Interact.performed += ctx => Interact();

        //layer atamalarý
        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        anim.SetBool("isWork", isWork);
        if (handFull)
        {
            anim.SetBool("hand", true);
            retakeDelay = 0.5f;
            workDelay = 1f;
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
    }
    public void Interact()
    {
        //  counter;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == true)
            {
                float value = counter.counterID;
                if (ID == 1)
                {
                    if (counter.counterID == 0 || counter.counterID == 2 || counter.counterID == 23 || counter.counterID == 24 || counter.counterID == 234 || counter.counterID == 2345) //masa boþsa
                    {
                        ID = 0;
                        handFull = false;
                        counter.kasaDolu = true;
                        CmdinteractID(0);
                        isWork = true;
                        if (counter.counterID != 0)
                        {
                            counter.smoke.Play();
                        }
                    }
                }
                if (ID == 2)
                {
                    if (counter.counterID == 0 || counter.counterID == 1 || counter.counterID == 3 || counter.counterID == 4 || counter.counterID == 5)
                    {
                        ID = 0;
                        counter.anaKartDolu = true;
                        handFull = false;
                        CmdinteractID(0);
                        isWork = true;
                        if (counter.counterID != 0)
                        {
                            counter.smoke.Play();
                        }
                    }
                }
                if (ID == 3)
                {
                    if (counter.counterID == 0 || counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 124 || counter.counterID == 125 || counter.counterID == 1245)
                    {
                        ID = 0;
                        counter.cpuDolu = true;
                        handFull = false;
                        CmdinteractID(0);
                        isWork = true;
                        if (counter.counterID != 0)
                        {
                            counter.smoke.Play();
                        }
                    }
                }
                if (ID == 4)
                {
                    if (counter.counterID == 0 || counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 123 || counter.counterID == 125 || counter.counterID == 1235)
                    {
                        ID = 0;
                        counter.ekranKartýDolu = true;
                        handFull = false;
                        CmdinteractID(0);
                        isWork = true;
                        if (counter.counterID != 0)
                        {
                            counter.smoke.Play();
                        }
                    }
                }
                if (ID == 5)
                {
                    if (counter.counterID == 0 || counter.counterID == 2 || counter.counterID == 12 || counter.counterID == 123 || counter.counterID == 124 || counter.counterID == 1234)
                    {
                        ID = 0;
                        counter.ramDolu = true;
                        handFull = false;
                        CmdinteractID(0);
                        isWork = true;
                        if (counter.counterID != 0)
                        {
                            counter.smoke.Play();
                        }
                    }
                }
            }
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter2) && handFull == false && retakeDelay <= 0)
            {
                float value = counter.counterID;
                if (ID == 0)
                {
                    if (counter.counterID == 1)
                    {
                        counter.kasaDolu = false;
                        ID = 1;
                        handFull = true;
                        CmdinteractID(1);
                    }
                    if (counter.counterID == 2)
                    {
                        counter.anaKartDolu = false;
                        ID = 2;
                        handFull = true;
                        CmdinteractID(2);
                    }
                    if (counter.counterID == 3)
                    {
                        counter.cpuDolu = false;
                        ID = 3;
                        handFull = true;
                        CmdinteractID(3);
                    }
                    if (counter.counterID == 4)
                    {
                        counter.ekranKartýDolu = false;
                        ID = 4;
                        handFull = true;
                        CmdinteractID(4);
                    }
                    if (counter.counterID == 5)
                    {
                        counter.ramDolu = false;
                        ID = 5;
                        handFull = true;
                        CmdinteractID(5);
                    }
                    if (counter.counterID == 12)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        ID = 12;
                        handFull = true;
                        CmdinteractID(12);
                    }
                    if (counter.counterID == 123)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.cpuDolu = false;
                        ID = 123;
                        handFull = true;
                        CmdinteractID(123);
                    }
                    if (counter.counterID == 124)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.ekranKartýDolu = false;
                        ID = 124;
                        handFull = true;
                        CmdinteractID(124);
                    }
                    if (counter.counterID == 125)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.ramDolu = false;
                        ID = 125;
                        handFull = true;
                        CmdinteractID(125);
                    }
                    if (counter.counterID == 1234)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.cpuDolu = false;
                        counter.ekranKartýDolu = false;
                        ID = 1234;
                        handFull = true;
                        CmdinteractID(1234);
                    }
                    if (counter.counterID == 1245)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.ekranKartýDolu = false;
                        counter.ramDolu = false;
                        ID = 1245;
                        handFull = true;
                        CmdinteractID(1245);
                    }
                    if (counter.counterID == 1235)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.cpuDolu = false;
                        counter.ramDolu = false;
                        ID = 1235;
                        handFull = true;
                        CmdinteractID(135);
                    }
                    if (counter.counterID == 12345)
                    {
                        counter.kasaDolu = false;
                        counter.anaKartDolu = false;
                        counter.cpuDolu = false;
                        counter.ekranKartýDolu = false;
                        counter.ramDolu = false;
                        ID = 12345;
                        handFull = true;
                        CmdinteractID(12345);
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
                CmdinteractID(1);
                handFull = true;
            }
            if (id == 2 && handFull == false)
            {
                CmdinteractID(2);
                handFull = true;
            }
            if (id == 3 && handFull == false)
            {
                CmdinteractID(3);
                handFull = true;
            }
            if (id == 4 && handFull == false)
            {
                CmdinteractID(4);
                handFull = true;
            }
            if (id == 5 && handFull == false)
            {
                CmdinteractID(5);
                handFull = true;
            }
        }
    }
    [Command]
    public void CmdinteractID(int objectNumber)
    {
        interactID(objectNumber);
        RpcinteractID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(int objectNumber)
    {
        interactID(objectNumber);
    }
    public void interactID(int objectNumber)
    {
        if (objectNumber == 0) // boþ
        {
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }
            childObject[0].SetActive(false);
            childObject[1].SetActive(false);
            childObject[2].SetActive(false);
            childObject[3].SetActive(false);
            childObject[4].SetActive(false);
            childObject[5].SetActive(false);
        }
        if (objectNumber == 1) // kasa
        {
            childObject[1].SetActive(true);
            ID = 1;
        }
        if (objectNumber == 2) // anakar
        {
            childObject[2].SetActive(true);
            ID = 2;
        }
        if (objectNumber == 3) // CPU
        {
            childObject[3].SetActive(true);
            ID = 3;
        }
        if (objectNumber == 4) // ekran kartý
        {
            childObject[4].SetActive(true);
            ID = 4;
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


