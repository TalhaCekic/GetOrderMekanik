using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Collections.Generic;
using System;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class pickUp : NetworkBehaviour
{
    //ta��d���m�z masan�n materialleri
    public Material[] material1, material2;

    // karakterin elindeki tan�ma �d si0
    [SyncVar] public float ID;


    // public static pickUp instance;
    // RAY
    private RaycastHit hit;
    [SerializeField] [Min(10)] private float hitRange = 100;

    int pickupLayerMask;
    int pickupLayerMask2;
    int pickupLayerMask3;
    int pickupLayerMask4;
    int pickupLayerMask5;
    int pickupLayerMask6;
    int pickupLayerMask7;

    public GameObject cam;

    public List<GameObject> childObject = new List<GameObject>();
    public List<GameObject> BoxObject = new List<GameObject>();

    // elimizi kontrole eder.
    [SyncVar] public bool handFull = false;
    [SyncVar] public bool isWork = false;
    [SyncVar] public bool isControlledPlayer = false;

    public controller playerInput;
    Animator anim;

    [SyncVar] public float retakeDelay = 0.5f;
    [SyncVar] public float workDelay = 1.1f;

    public GameObject pcInteract;
    public bool isPcInteract = false;

    private GameObject table;
    private Transform tablePosition;
    [SyncVar] [SerializeField] public bool isTable;
    [SyncVar] [SerializeField] public bool isControllerTable = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        pcInteract.transform.localScale = new Vector3(0, 0, 0);
        pcInteract.SetActive(true);
        //en ba�ta objeleri sil
        for (int i = 0; i < childObject.Count; i++)
        {
            childObject[i].SetActive(false);
        }

        for (int i = 0; i < BoxObject.Count; i++)
        {
            BoxObject[i].SetActive(false);
        }

        workDelay = 1.1f;
        //tu� etkile�imi
        if (!isLocalPlayer) return;
        playerInput = new controller();
        playerInput.Enable();
        playerInput.Player.Interact.performed += ctx => Interact();
        playerInput.Player.Drop.performed += ctx => secondsInteract();

        //layer atamalar�
        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
        pickupLayerMask4 = LayerMask.GetMask("submid");
        pickupLayerMask5 = LayerMask.GetMask("pcInteract");
        pickupLayerMask6 = LayerMask.GetMask("pcControllerTable");
        pickupLayerMask7 = LayerMask.GetMask("Cleaner");
    }

    void Update()
    {
        pcInteract.SetActive(true);
        if (!isLocalPlayer) return;
        anim.SetBool("isWork", isWork);

        if (handFull) // elde tutma ve tekrar almay� kontrol eder
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
                if (ID > 0)
                {
                    handFull = true;
                }
            }
        }
    }

    [Command]
    public void CMDObjectChangePosition(GameObject hit1)
    {
        if (!DayManager.instance.dayOn)
        {
            handFull = true;
            ObjectChangePosition(hit1);
            RpcObjectChangePositon(hit1);
        }
    }

    [ClientRpc]
    public void RpcObjectChangePositon(GameObject hit1)
    {
        ObjectChangePosition(hit1);
    }

    public void ObjectChangePosition(GameObject hit1)
    {
        string aktifSahneAdi = SceneManager.GetActiveScene().name;
        if (aktifSahneAdi != "PcTutorial")
        {
            table = hit1.gameObject;
            table.tag = "portableobject";
            table.gameObject.GetComponent<OnTriggerTEST>().enabled = true;
            table.GetComponent<Collider>().isTrigger = true;
            hit1.gameObject.transform.SetParent(this.transform);
            Vector3 newLocalPosition = new Vector3(0.0989999995f, 1.4f, 1.49399996f);
            hit1.gameObject.transform.localPosition = newLocalPosition;
            Vector3 newLocalRotation = new Vector3(-90, 180, 0);
            hit1.gameObject.transform.localRotation = Quaternion.Euler(newLocalRotation);
            isTable = false;
            table.GetComponent<MeshRenderer>().materials = material1;
            table.GetComponent<Rigidbody>().useGravity = false;
            isControllerTable = true;
        }
    }

    [Command]
    public void CMDObjectChangePosition1(GameObject hit1)
    {
        if (!DayManager.instance.dayOn)
        {
            handFull = false;
            ObjectChangePosition1(hit1);
            RpcObjectChangePositon1(hit1);
        }
    }

    [ClientRpc]
    public void RpcObjectChangePositon1(GameObject hit1)
    {
        ObjectChangePosition1(hit1);
    }

    public void ObjectChangePosition1(GameObject hit1)
    {
        table.GetComponent<Rigidbody>().useGravity = true;
        tablePosition = GameObject.FindGameObjectWithTag("TablePosition").transform;
        hit1.gameObject.transform.SetParent(tablePosition);
        hit1.gameObject.layer = LayerMask.NameToLayer("counter");
        ChangeLayerRecursively(hit1.transform, "counter");
        table.GetComponent<Rigidbody>().isKinematic = true;
        isTable = true;
        table.GetComponent<Collider>().isTrigger = false;
        table.GetComponent<MeshRenderer>().materials = material2;
        table.GetComponent<OnTriggerTEST>().enabled = false;
        table.tag = "counter";
        isControllerTable = false;
    }

    void ChangeLayerRecursively(Transform trans, string newLayer)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(newLayer);

        foreach (Transform child in trans)
        {
            ChangeLayerRecursively(child, newLayer);

            //   child.GetComponent<Camera>().cullingMask = 11;
        }
    }

    public void secondsInteract()
    {
        if (this.gameObject.GetComponentInChildren<Cleaner>() != null)
        {
            this.gameObject.GetComponentInChildren<Cleaner>().gameObject.layer = LayerMask.NameToLayer("Cleaner");
            this.gameObject.GetComponentInChildren<Cleaner>().transform.parent = null;
        }

        if (handFull == true && table.gameObject.transform.parent == this.transform)
        {
            Quaternion currentRotation = table.gameObject.transform.localRotation;
            Quaternion newRotation = Quaternion.Euler(0, 0, 45f);
            table.gameObject.transform.localRotation = currentRotation * newRotation;
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange,
                pickupLayerMask2))
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

                if (counter.submidID == 2.1f)
                {
                    counter.CmdSetBoxAnakartDolu(false);
                    counter.CmdSetAnakartDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }

                if (counter.submidID == 3.1f)
                {
                    counter.CmdSetBoxCpuDolu(false);
                    counter.CmdSetCpuDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }

                if (counter.submidID == 4.1f)
                {
                    counter.CmdSetBoxEkranKartiDolu(false);
                    counter.CmdSetEkranKartiDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }

                if (counter.submidID == 5.1f)
                {
                    counter.CmdSetBoxRamDolu(false);
                    counter.CmdSetRamDolu(true);
                    isWork = true;
                    counter.CmdSmokeEffectPLAY();
                }
            }
        }

        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange,
                pickupLayerMask6))
        {
            if (hit.collider.gameObject.TryGetComponent<ControlledTestTable>(out var ControlledTestTable))
            {
                if (ControlledTestTable.submidID == 12)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 123)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "cpuDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 124)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ekranKartiDolu")
                    {
                        ControlledTestTable.CmdSetEkranKartiDolu(false);
                        CmdtrashInteractID(4.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 125)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ramDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(5.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 1234)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "cpuDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ekranKartiDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 1245)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ekranKartiDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ramDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(5.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 1235)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "cpuDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ramDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(5.3f);
                        isWork = true;
                        handFull = true;
                    }
                }

                if (ControlledTestTable.submidID == 12345)
                {
                    if (ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                    {
                        ControlledTestTable.CmdSetKasaDolu(false);
                        CmdtrashInteractID(1.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                    {
                        ControlledTestTable.CmdSetAnakartDolu(false);
                        CmdtrashInteractID(2.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "cpuDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ekranKartiDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(3.3f);
                        isWork = true;
                        handFull = true;
                    }

                    if (ControlledTestTable.selectedBoolVariableName == "ramDolu")
                    {
                        ControlledTestTable.CmdSetCpuDolu(false);
                        CmdtrashInteractID(5.3f);
                        isWork = true;
                        handFull = true;
                    }
                }
            }
        }
    }

    public void Interact()
    {
        // Temizlik sopası
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange,
                pickupLayerMask7))
        {
            if (handFull == false)
            {
                hit.collider.gameObject.transform.parent = this.transform;
                hit.collider.gameObject.transform.SetParent(this.transform, true);
            }
        }

        // Masa etkile�imi i�in
        if (workDelay == 1.1f)
        {
            // ��p kutusu
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    hitRange, pickupLayerMask3))
            {
                interactID(0);
                CmdtrashInteractID(0);
            }

            //sahne kontrolü
            string aktifSahneAdi = SceneManager.GetActiveScene().name;
            if (aktifSahneAdi != "PcTutorial")
            {
                //obje ta��ma
                if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
                        hitRange, pickupLayerMask2))
                {
                    if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == false &&
                        isTable == true)
                    {
                        CMDObjectChangePosition(hit.collider.gameObject);
                    }
                }
                else if (handFull && table.gameObject.transform.parent == this.transform && isTable == false)
                {
                    CMDObjectChangePosition1(table);
                }
            }
            // else
            // {
            //     if (!DayManager.instance.dayOn)
            //     {
            //         //obje ta��ma
            //         if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
            //                 hitRange, pickupLayerMask2))
            //         {
            //             if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == false &&
            //                 isTable == true)
            //             {
            //                 handFull = true;
            //                 CMDObjectChangePosition(hit.collider.gameObject);
            //             }
            //         }
            //         else if (handFull == true && table.gameObject.transform.parent == this.transform &&
            //                  isTable == false &&
            //                  table.gameObject.GetComponent<OnTriggerTEST>().isStay == false)
            //         {
            //             handFull = false;
            //             CMDObjectChangePosition1(table);
            //         }
            //     }
            // }

            // counter;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    hitRange, pickupLayerMask2))
            {
                if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == true)
                {
                    float value = counter.submidID;
                    if (ID == 1)
                    {
                        if (counter.submidID == 2 || counter.submidID == 23 || counter.submidID == 24 ||
                            counter.submidID == 234 || counter.submidID == 2345) //masa bo�sa
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
                        if (counter.submidID == 1 || counter.submidID == 3 || counter.submidID == 4 ||
                            counter.submidID == 5)
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
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 124 ||
                            counter.submidID == 125 || counter.submidID == 1245 || counter.submidID != 12345 ||
                            counter.submidID == 24 || counter.submidID == 25 || counter.submidID == 245)
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
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 123 ||
                            counter.submidID == 125 || counter.submidID == 1235 || counter.submidID == 23 ||
                            counter.submidID == 25 || counter.submidID == 235)
                        {
                            ID = 0;
                            counter.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }

                        if (counter.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 5)
                    {
                        if (counter.submidID == 2 || counter.submidID == 12 || counter.submidID == 123 ||
                            counter.submidID == 124 || counter.submidID == 1234 || counter.submidID == 23 ||
                            counter.submidID == 24 || counter.submidID == 234)
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

                    if (ID == 2.1f)
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

                    if (ID == 3.1f)
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

                    if (ID == 4.1f)
                    {
                        if (counter.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }

                    if (ID == 5.1f)
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
                        if (counter.submidID == 3 || counter.submidID == 4 || counter.submidID == 34 ||
                            counter.submidID == 345) //masa bo�sa
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetEkranKartiDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    // kontrol sonras� ID
                    if (ID == 1.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
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
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxAnakartDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }

                    if (ID == 3.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxCpuDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }

                    if (ID == 4.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }

                    if (ID == 5.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetBoxRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdSmokeEffectStop();
                        }
                    }

                    if (ID == 12.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 123.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
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

                    if (ID == 124.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 125.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 1234.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 1235.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
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

                    if (ID == 1245.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 12345.2f)
                    {
                        if (counter.submidID == 0)
                        {
                            counter.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            counter.CmdSetKasaDolu(true);
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 23)
                    {
                        if (counter.submidID == 1 || counter.submidID == 4 || counter.submidID == 5)
                        {
                            ID = 0;
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
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            isWork = false;
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 24)
                    {
                        if (counter.submidID == 1 || counter.submidID == 5)
                        {
                            ID = 0;
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }

                        if (counter.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 25)
                    {
                        if (counter.submidID == 1 || counter.submidID == 3 || counter.submidID == 4)
                        {
                            ID = 0;
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
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 234)
                    {
                        if (counter.submidID == 1 || counter.submidID == 5)
                        {
                            ID = 0;
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                            counter.CmdAffixEffectPLAY();
                        }

                        if (counter.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 235)
                    {
                        if (counter.submidID == 1 || counter.submidID == 4)
                        {
                            ID = 0;
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
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetCpuDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }

                    if (ID == 245)
                    {
                        if (counter.submidID == 1 || counter.submidID == 3)
                        {
                            ID = 0;
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
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
                            counter.CmdSetAnakartDolu(true);
                            counter.CmdSetEkranKartiDolu(true);
                            counter.CmdSetRamDolu(true);
                            CmdinteractID(0);
                            counter.CmdAffixEffectStop();
                        }
                    }
                }

                //tekrardan alma i�lemi
                if (hit.collider.gameObject.TryGetComponent<counter>(out var counter2) && handFull == false &&
                    retakeDelay <= 0)
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

                        if (counter.submidID == 2.1f)
                        {
                            counter.CmdSetBoxAnakartDolu(false);
                            ID = 2.1f;
                            handFull = true;
                            CmdinteractID(2.1f);
                        }

                        if (counter.submidID == 3)
                        {
                            counter.CmdSetCpuDolu(false);
                            ID = 3;
                            handFull = true;
                            CmdinteractID(3);
                        }

                        if (counter.submidID == 3.1f)
                        {
                            counter.CmdSetBoxCpuDolu(false);
                            ID = 3.3f;
                            handFull = true;
                            CmdinteractID(3.1f);
                        }

                        if (counter.submidID == 4)
                        {
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 4;
                            handFull = true;
                            CmdinteractID(4);
                        }

                        if (counter.submidID == 4.1f)
                        {
                            counter.CmdSetBoxEkranKartiDolu(false);
                            ID = 4.4f;
                            handFull = true;
                            CmdinteractID(4.1f);
                        }

                        if (counter.submidID == 5)
                        {
                            counter.CmdSetRamDolu(false);
                            ID = 5;
                            handFull = true;
                            CmdinteractID(5);
                        }

                        if (counter.submidID == 5.1f)
                        {
                            counter.CmdSetBoxRamDolu(false);
                            ID = 5.1f;
                            handFull = true;
                            CmdinteractID(5.1f);
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
                            counter.CmdSetEkranKartiDolu(false);
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
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 1234;
                            handFull = true;
                            CmdinteractID(1234);
                        }

                        if (counter.submidID == 1245)
                        {
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
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
                            counter.CmdSetEkranKartiDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 12345;
                            handFull = true;
                            CmdinteractID(12345);
                        }

                        // kontrol sonras� ID i�lemi

                        if (counter.submidID == 1.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            ID = 1.2f;
                            handFull = true;
                            CmdinteractID(1.2f);
                        }

                        if (counter.submidID == 2.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetAnakartDolu(false);
                            ID = 2.2f;
                            handFull = true;
                            CmdinteractID(2.2f);
                        }

                        if (counter.submidID == 3.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetCpuDolu(false);
                            ID = 3.2f;
                            handFull = true;
                            CmdinteractID(3.2f);
                        }

                        if (counter.submidID == 4.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 4.2f;
                            handFull = true;
                            CmdinteractID(4.2f);
                        }

                        if (counter.submidID == 5.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetRamDolu(false);
                            ID = 5.2f;
                            handFull = true;
                            CmdinteractID(5.2f);
                        }

                        if (counter.submidID == 12.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            ID = 12.2f;
                            handFull = true;
                            CmdinteractID(12.2f);
                        }

                        if (counter.submidID == 123.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            ID = 123.2f;
                            handFull = true;
                            CmdinteractID(123.2f);
                        }

                        if (counter.submidID == 124.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 124.2f;
                            handFull = true;
                            CmdinteractID(124.2f);
                        }

                        if (counter.submidID == 125.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 125.2f;
                            handFull = true;
                            CmdinteractID(125.2f);
                        }

                        if (counter.submidID == 1234.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 1234.2f;
                            handFull = true;
                            CmdinteractID(1234.2f);
                        }

                        if (counter.submidID == 1245.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1245.2f;
                            handFull = true;
                            CmdinteractID(1245.2f);
                        }

                        if (counter.submidID == 1235.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 1235.2f;
                            handFull = true;
                            CmdinteractID(1235.2f);
                        }

                        if (counter.submidID == 12345.2f)
                        {
                            counter.CmdSetisControlled(false);
                            counter.CmdSetKasaDolu(false);
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 12345.2f;
                            handFull = true;
                            CmdinteractID(12345.2f);
                        }

                        if (counter.submidID == 23)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            ID = 23;
                            handFull = true;
                            CmdinteractID(23);
                        }

                        if (counter.submidID == 24)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 24;
                            handFull = true;
                            CmdinteractID(24);
                        }

                        if (counter.submidID == 25)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 25;
                            handFull = true;
                            CmdinteractID(25);
                        }

                        if (counter.submidID == 234)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            ID = 234;
                            handFull = true;
                            CmdinteractID(234);
                        }

                        if (counter.submidID == 235)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetCpuDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 235;
                            handFull = true;
                            CmdinteractID(235);
                        }

                        if (counter.submidID == 245)
                        {
                            counter.CmdSetAnakartDolu(false);
                            counter.CmdSetEkranKartiDolu(false);
                            counter.CmdSetRamDolu(false);
                            ID = 245;
                            handFull = true;
                            CmdinteractID(245);
                        }
                    }
                }
            }

            // submid;
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    hitRange, pickupLayerMask4))
            {
                if (hit.collider.gameObject.TryGetComponent<DeliveryOrder>(out var DeliveryOrder) && handFull == true)
                {
                    float value = DeliveryOrder.submidID;
                    if (ID == 1)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 23 ||
                            DeliveryOrder.submidID == 24 || DeliveryOrder.submidID == 234 ||
                            DeliveryOrder.submidID == 2345 || DeliveryOrder.submidID == 245) //masa bo�sa
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 2)
                    {
                        if (DeliveryOrder.submidID == 1 || DeliveryOrder.submidID == 3 || DeliveryOrder.submidID == 4 ||
                            DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 3)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 ||
                            DeliveryOrder.submidID == 124 || DeliveryOrder.submidID == 125 ||
                            DeliveryOrder.submidID == 1245 || DeliveryOrder.submidID != 12345 ||
                            DeliveryOrder.submidID == 24 || DeliveryOrder.submidID == 25 ||
                            DeliveryOrder.submidID == 245)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetCpuDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 4)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 ||
                            DeliveryOrder.submidID == 123 || DeliveryOrder.submidID == 125 ||
                            DeliveryOrder.submidID == 1235 || DeliveryOrder.submidID == 23 ||
                            DeliveryOrder.submidID == 25 || DeliveryOrder.submidID == 235)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 5)
                    {
                        if (DeliveryOrder.submidID == 2 || DeliveryOrder.submidID == 12 ||
                            DeliveryOrder.submidID == 123 || DeliveryOrder.submidID == 124 ||
                            DeliveryOrder.submidID == 1234 || DeliveryOrder.submidID == 23 ||
                            DeliveryOrder.submidID == 24 || DeliveryOrder.submidID == 234)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 12)
                    {
                        if (DeliveryOrder.submidID == 3 || DeliveryOrder.submidID == 4 ||
                            DeliveryOrder.submidID == 34 || DeliveryOrder.submidID == 345) //masa bo�sa
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                            isWork = true;
                        }
                        else if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
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
                        }
                    }

                    if (ID == 124)
                    {
                        if (DeliveryOrder.submidID == 5)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
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
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
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
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
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
                        }
                    }

                    if (ID == 1245)
                    {
                        if (DeliveryOrder.submidID == 3)
                        {
                            ID = 0;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            handFull = false;
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (DeliveryOrder.submidID == 0)
                        {
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
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
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    // kontrol sonras� ID
                    if (ID == 1.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 2.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 3.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 4.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 5.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 12.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 123.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            CmdinteractID(0);
                            isWork = false;
                        }
                    }

                    if (ID == 124.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 125.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 1234.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 1235.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 1245.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }

                    if (ID == 12345.2f)
                    {
                        if (DeliveryOrder.submidID == 0)
                        {
                            DeliveryOrder.CmdSetisControlled(true);
                            ID = 0;
                            handFull = false;
                            DeliveryOrder.CmdSetKasaDolu(true);
                            DeliveryOrder.CmdSetAnakartDolu(true);
                            DeliveryOrder.CmdSetCpuDolu(true);
                            DeliveryOrder.CmdSetEkranKartiDolu(true);
                            DeliveryOrder.CmdSetRamDolu(true);
                            CmdinteractID(0);
                        }
                    }
                }

                //tekrardan alma i�lemi
                if (hit.collider.gameObject.TryGetComponent<DeliveryOrder>(out var DeliveryOrder2) &&
                    handFull == false && retakeDelay <= 0)
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
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
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
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
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
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 1234;
                            handFull = true;
                            CmdinteractID(1234);
                        }

                        if (DeliveryOrder.submidID == 1245)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
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
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 12345;
                            handFull = true;
                            CmdinteractID(12345);
                        }

                        // Kontrol sonraso ID
                        if (DeliveryOrder.submidID == 1.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            ID = 1.2f;
                            handFull = true;
                            CmdinteractID(1.2f);
                        }

                        if (DeliveryOrder.submidID == 2.2f)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            ID = 2.2f;
                            handFull = true;
                            CmdinteractID(2.2f);
                        }

                        if (DeliveryOrder.submidID == 3.2f)
                        {
                            DeliveryOrder.CmdSetCpuDolu(false);
                            ID = 3.2f;
                            handFull = true;
                            CmdinteractID(3.2f);
                        }

                        if (DeliveryOrder.submidID == 4.2f)
                        {
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 4.2f;
                            handFull = true;
                            CmdinteractID(4.2f);
                        }

                        if (DeliveryOrder.submidID == 5.2f)
                        {
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 5.2f;
                            handFull = true;
                            CmdinteractID(5.2f);
                        }

                        if (DeliveryOrder.submidID == 12.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            ID = 12.2f;
                            handFull = true;
                            CmdinteractID(12.2f);
                        }

                        if (DeliveryOrder.submidID == 123.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            ID = 123.2f;
                            handFull = true;
                            CmdinteractID(123);
                        }

                        if (DeliveryOrder.submidID == 124.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 124.2f;
                            handFull = true;
                            CmdinteractID(124.2f);
                        }

                        if (DeliveryOrder.submidID == 125.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 125.2f;
                            handFull = true;
                            CmdinteractID(125.2f);
                        }

                        if (DeliveryOrder.submidID == 1234.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 1234.2f;
                            handFull = true;
                            CmdinteractID(1234.2f);
                        }

                        if (DeliveryOrder.submidID == 1245.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 1245.2f;
                            handFull = true;
                            CmdinteractID(1245.2f);
                        }

                        if (DeliveryOrder.submidID == 1235.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 1235.2f;
                            handFull = true;
                            CmdinteractID(1235.2f);
                        }

                        if (DeliveryOrder.submidID == 12345.2f)
                        {
                            DeliveryOrder.CmdSetKasaDolu(false);
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 12345.2f;
                            handFull = true;
                            CmdinteractID(12345.2f);
                        }

                        if (DeliveryOrder.submidID == 23)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            ID = 23;
                            handFull = true;
                            CmdinteractID(23.2f);
                        }

                        if (DeliveryOrder.submidID == 24)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 24;
                            handFull = true;
                            CmdinteractID(24);
                        }

                        if (DeliveryOrder.submidID == 25)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 25;
                            handFull = true;
                            CmdinteractID(25);
                        }

                        if (DeliveryOrder.submidID == 234)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            ID = 234;
                            handFull = true;
                            CmdinteractID(234);
                        }

                        if (DeliveryOrder.submidID == 245)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetEkranKartiDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 245;
                            handFull = true;
                            CmdinteractID(245);
                        }

                        if (DeliveryOrder.submidID == 235)
                        {
                            DeliveryOrder.CmdSetAnakartDolu(false);
                            DeliveryOrder.CmdSetCpuDolu(false);
                            DeliveryOrder.CmdSetRamDolu(false);
                            ID = 235;
                            handFull = true;
                            CmdinteractID(235);
                        }
                    }
                }
            }

            // pcControllerTable
            if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    hitRange, pickupLayerMask6))
            {
                if (hit.collider.gameObject.TryGetComponent<ControlledTestTable>(out var ControlledTestTable) &&
                    handFull == true)
                {
                    if (!ControlledTestTable.isReady && !ControlledTestTable.isControlled)
                    {
                        if (ID == 1)
                        {
                            if (ControlledTestTable.submidID == 2 || ControlledTestTable.submidID == 23 ||
                                ControlledTestTable.submidID == 24 || ControlledTestTable.submidID == 234 ||
                                ControlledTestTable.submidID == 2345) //masa bo�sa
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                CmdinteractID(0);
                                isWork = true;
                            }
                            else if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 2)
                        {
                            if (ControlledTestTable.submidID == 1 || ControlledTestTable.submidID == 3 ||
                                ControlledTestTable.submidID == 4 || ControlledTestTable.submidID == 5)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }
                            else if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 3)
                        {
                            if (ControlledTestTable.submidID == 2 || ControlledTestTable.submidID == 12 ||
                                ControlledTestTable.submidID == 124 || ControlledTestTable.submidID == 125 ||
                                ControlledTestTable.submidID == 1245 || ControlledTestTable.submidID != 12345 ||
                                ControlledTestTable.submidID == 24 || ControlledTestTable.submidID == 25 ||
                                ControlledTestTable.submidID == 245)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetCpuDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetCpuDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 4)
                        {
                            if (ControlledTestTable.submidID == 2 || ControlledTestTable.submidID == 12 ||
                                ControlledTestTable.submidID == 123 || ControlledTestTable.submidID == 125 ||
                                ControlledTestTable.submidID == 1235 || ControlledTestTable.submidID == 23 ||
                                ControlledTestTable.submidID == 25 || ControlledTestTable.submidID == 235)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 5)
                        {
                            if (ControlledTestTable.submidID == 2 || ControlledTestTable.submidID == 12 ||
                                ControlledTestTable.submidID == 123 || ControlledTestTable.submidID == 124 ||
                                ControlledTestTable.submidID == 1234 || ControlledTestTable.submidID == 23 ||
                                ControlledTestTable.submidID == 24 || ControlledTestTable.submidID == 234)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetRamDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetRamDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 12)
                        {
                            if (ControlledTestTable.submidID == 3 || ControlledTestTable.submidID == 4 ||
                                ControlledTestTable.submidID == 34 || ControlledTestTable.submidID == 345) //masa bo�sa
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                CmdinteractID(0);
                                isWork = true;
                            }
                            else if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 123)
                        {
                            if (ControlledTestTable.submidID == 4 || ControlledTestTable.submidID == 5)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }
                            else if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                CmdinteractID(0);
                                isWork = false;
                            }
                        }

                        if (ID == 124)
                        {
                            if (ControlledTestTable.submidID == 5)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 125)
                        {
                            if (ControlledTestTable.submidID == 3 || ControlledTestTable.submidID == 4)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 1234)
                        {
                            if (ControlledTestTable.submidID == 5)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 1235)
                        {
                            if (ControlledTestTable.submidID == 4)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 1245)
                        {
                            if (ControlledTestTable.submidID == 3)
                            {
                                ID = 0;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                handFull = false;
                                CmdinteractID(0);
                                isWork = true;
                            }

                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                CmdinteractID(0);
                            }
                        }

                        if (ID == 12345)
                        {
                            if (ControlledTestTable.submidID == 0)
                            {
                                ID = 0;
                                handFull = false;
                                ControlledTestTable.CmdSetKasaDolu(true);
                                ControlledTestTable.CmdSetAnakartDolu(true);
                                ControlledTestTable.CmdSetCpuDolu(true);
                                ControlledTestTable.CmdSetEkranKartiDolu(true);
                                ControlledTestTable.CmdSetRamDolu(true);
                                CmdinteractID(0);
                            }
                        }
                    }

                    //kontrol tezgah� dolu oldu�u senaryo
                    if (!ControlledTestTable.isReady && ControlledTestTable.isControlled)
                    {
                        if (ID == 1 && ControlledTestTable.selectedBoolVariableName == "kasaDolu")
                        {
                            ID = 0;
                            handFull = false;
                            ControlledTestTable.CmdSetKasaDolu(true);
                            ControlledTestTable.CmdSetisReady(true);
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (ID == 2 && ControlledTestTable.selectedBoolVariableName == "anaKartDolu")
                        {
                            ID = 0;
                            handFull = false;
                            ControlledTestTable.CmdSetAnakartDolu(true);
                            ControlledTestTable.CmdSetisReady(true);
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (ID == 3 && ControlledTestTable.selectedBoolVariableName == "cpuDolu")
                        {
                            ID = 0;
                            handFull = false;
                            ControlledTestTable.CmdSetCpuDolu(true);
                            ControlledTestTable.CmdSetisReady(true);
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (ID == 4 && ControlledTestTable.selectedBoolVariableName == "ekranKartiDolu")
                        {
                            ID = 0;
                            handFull = false;
                            ControlledTestTable.CmdSetEkranKartiDolu(true);
                            ControlledTestTable.CmdSetisReady(true);
                            CmdinteractID(0);
                            isWork = true;
                        }

                        if (ID == 5 && ControlledTestTable.selectedBoolVariableName == "ramDolu")
                        {
                            ID = 0;
                            handFull = false;
                            ControlledTestTable.CmdSetRamDolu(true);
                            ControlledTestTable.CmdSetisReady(true);
                            CmdinteractID(0);
                            isWork = true;
                        }
                    }
                }

                //tekrardan alma i�lemi
                if (hit.collider.gameObject.TryGetComponent<ControlledTestTable>(out var ControlledTestTable2) &&
                    handFull == false && retakeDelay <= 0)
                {
                    float value = ControlledTestTable.submidID;
                    if (ID == 0)
                    {
                        if (!ControlledTestTable.isReady && !ControlledTestTable.isControlled)
                        {
                            if (ControlledTestTable.submidID == 1)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ID = 1;
                                handFull = true;
                                CmdinteractID(1);
                            }

                            if (ControlledTestTable.submidID == 2)
                            {
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ID = 2;
                                handFull = true;
                                CmdinteractID(2);
                            }

                            if (ControlledTestTable.submidID == 3)
                            {
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ID = 3;
                                handFull = true;
                                CmdinteractID(3);
                            }

                            if (ControlledTestTable.submidID == 4)
                            {
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 4;
                                handFull = true;
                                CmdinteractID(4);
                            }

                            if (ControlledTestTable.submidID == 5)
                            {
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 5;
                                handFull = true;
                                CmdinteractID(5);
                            }

                            if (ControlledTestTable.submidID == 12)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ID = 12;
                                handFull = true;
                                CmdinteractID(12);
                            }

                            if (ControlledTestTable.submidID == 123)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ID = 123;
                                handFull = true;
                                CmdinteractID(123);
                            }

                            if (ControlledTestTable.submidID == 124)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 124;
                                handFull = true;
                                CmdinteractID(124);
                            }

                            if (ControlledTestTable.submidID == 125)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 125;
                                handFull = true;
                                CmdinteractID(125);
                            }

                            if (ControlledTestTable.submidID == 1234)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 1234;
                                handFull = true;
                                CmdinteractID(1234);
                            }

                            if (ControlledTestTable.submidID == 1245)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 1245;
                                handFull = true;
                                CmdinteractID(1245);
                            }

                            if (ControlledTestTable.submidID == 1235)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 1235;
                                handFull = true;
                                CmdinteractID(1235);
                            }

                            if (ControlledTestTable.submidID == 12345)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 12345;
                                handFull = true;
                                CmdinteractID(12345);
                            }
                        }

                        // kontro sonras� ID
                        if (ControlledTestTable.isReady && ControlledTestTable.isControlled)
                        {
                            isControlledPlayer = ControlledTestTable.isControlled;
                            if (ControlledTestTable.submidID == 1.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ID = 1;
                                handFull = true;
                                CmdinteractID(1.2f);
                            }

                            if (ControlledTestTable.submidID == 2.2f)
                            {
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ID = 2.2f;
                                handFull = true;
                                CmdinteractID(2.2f);
                            }

                            if (ControlledTestTable.submidID == 3.2f)
                            {
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ID = 3.2f;
                                handFull = true;
                                CmdinteractID(3.2f);
                            }

                            if (ControlledTestTable.submidID == 4.2f)
                            {
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 4;
                                handFull = true;
                                CmdinteractID(4.2f);
                            }

                            if (ControlledTestTable.submidID == 5.2f)
                            {
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 5;
                                handFull = true;
                                CmdinteractID(5.2f);
                            }

                            if (ControlledTestTable.submidID == 12.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ID = 12;
                                handFull = true;
                                CmdinteractID(12.2f);
                            }

                            if (ControlledTestTable.submidID == 123.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ID = 123.2f;
                                handFull = true;
                                CmdinteractID(123.2f);
                            }

                            if (ControlledTestTable.submidID == 124.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 124.2f;
                                handFull = true;
                                CmdinteractID(124.2f);
                            }

                            if (ControlledTestTable.submidID == 125.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 125.2f;
                                handFull = true;
                                CmdinteractID(125.2f);
                            }

                            if (ControlledTestTable.submidID == 1234.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ID = 1234.2f;
                                handFull = true;
                                CmdinteractID(1234.2f);
                            }

                            if (ControlledTestTable.submidID == 1245.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 1245.2f;
                                handFull = true;
                                CmdinteractID(1245.2f);
                            }

                            if (ControlledTestTable.submidID == 1235.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 1235.2f;
                                handFull = true;
                                CmdinteractID(1235.2f);
                            }

                            if (ControlledTestTable.submidID == 12345.2f)
                            {
                                ControlledTestTable.CmdSetKasaDolu(false);
                                ControlledTestTable.CmdSetAnakartDolu(false);
                                ControlledTestTable.CmdSetCpuDolu(false);
                                ControlledTestTable.CmdSetEkranKartiDolu(false);
                                ControlledTestTable.CmdSetRamDolu(false);
                                ID = 12345.2f;
                                handFull = true;
                                CmdinteractID(12345.2f);
                            }
                        }
                    }
                }
            }
        }

        //ele obje al�m�;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange,
                pickupLayerMask))
        {
            float id = hit.transform.GetComponent<itemID>().ItemID;
            if (id == 1 && ID == 0 && handFull == false)
            {
                CmdinteractID(1.1f);
                handFull = true;
            }

            if (id == 2 && ID == 0 && handFull == false)
            {
                CmdinteractID(2.1f);
                handFull = true;
            }

            if (id == 3 && ID == 0 && handFull == false)
            {
                CmdinteractID(3.1f);
                handFull = true;
            }

            if (id == 4 && ID == 0 && handFull == false)
            {
                CmdinteractID(4.1f);
                handFull = true;
            }

            if (id == 5 && ID == 0 && handFull == false)
            {
                CmdinteractID(5.1f);
                handFull = true;
            }

            // tekrardan b�rakma i�lemi
            if (id == 1 && ID == 1.1f && handFull && retakeDelay >= 0.5f)
            {
                CmdinteractID(0);
                ID = 0;
                handFull = false;
            }

            if (id == 2 && ID == 2.1f && handFull && retakeDelay >= 0.5f)
            {
                CmdinteractID(0);
                ID = 0;
                handFull = false;
            }

            if (id == 3 && ID == 3.1f && handFull && retakeDelay >= 0.5f)
            {
                CmdinteractID(0);
                handFull = false;
            }

            if (id == 4 && ID == 4.1f && handFull && retakeDelay >= 0.5f)
            {
                CmdinteractID(0);
                handFull = false;
            }

            if (id == 5 && ID == 5.1f && handFull && retakeDelay >= 0.5f)
            {
                CmdinteractID(0);
                handFull = false;
            }
        }

        // canvas etkile�imi i�in
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange,
                pickupLayerMask5))
        {
            isPcInteract = !isPcInteract;
            if (isPcInteract)
            {
                pcInteract.transform.localScale = new Vector3(1, 1, 1);
            }
            else
            {
                pcInteract.transform.localScale = new Vector3(0, 0, 0);
            }
        }
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
        if (objectNumber == 0) // bo�
        {
            isControlledPlayer = false;
            handFull = false;
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }

            for (int i = 0; i < BoxObject.Count; i++)
            {
                BoxObject[i].SetActive(false);
            }

            ID = 0;
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

        if (objectNumber == 2.1f) // anakar
        {
            BoxObject[2].SetActive(true);
            ID = 2.1f;
        }

        if (objectNumber == 2) // anakar
        {
            childObject[2].SetActive(true);
            ID = 2;
        }

        if (objectNumber == 3.1f) // CPU
        {
            BoxObject[3].SetActive(true);
            ID = 3.1f;
        }

        if (objectNumber == 3) // CPU
        {
            childObject[3].SetActive(true);
            ID = 3;
        }

        if (objectNumber == 4.1f) // ekran kart�
        {
            BoxObject[4].SetActive(true);
            ID = 4.1f;
        }

        if (objectNumber == 4) // ekran kart�
        {
            childObject[4].SetActive(true);
            ID = 4;
        }

        if (objectNumber == 5.1f) // ram
        {
            BoxObject[5].SetActive(true);
            ID = 5.1f;
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

        if (objectNumber == 23)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            ID = 23;
        }

        if (objectNumber == 24)
        {
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            ID = 24;
        }

        if (objectNumber == 25)
        {
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            ID = 25;
        }

        if (objectNumber == 234)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            ID = 234;
        }

        if (objectNumber == 235)
        {
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            ID = 235;
        }

        if (objectNumber == 245)
        {
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            ID = 245;
        }

        //kontrol sonraso ID
        if (objectNumber == 1.2f) // kasa
        {
            childObject[1].SetActive(true);
            ID = 1.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 2.2f) // anakar
        {
            childObject[2].SetActive(true);
            ID = 2.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 3.2f) // CPU
        {
            childObject[3].SetActive(true);
            ID = 3.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 4.2f) // ekran kart�
        {
            childObject[4].SetActive(true);
            ID = 4.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 5.2f) // ram
        {
            childObject[5].SetActive(true);
            ID = 5.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 12.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            ID = 12.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 123.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            ID = 123.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 124.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            ID = 124.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 125.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[5].SetActive(true);
            ID = 125.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 1234.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            ID = 1234.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 1235.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[5].SetActive(true);
            ID = 1235.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 1245.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            ID = 1245.2f;
            isControlledPlayer = true;
        }

        if (objectNumber == 12345.2f)
        {
            childObject[1].SetActive(true);
            childObject[2].SetActive(true);
            childObject[3].SetActive(true);
            childObject[4].SetActive(true);
            childObject[5].SetActive(true);
            ID = 12345.2f;
            isControlledPlayer = true;
        }
    }

    //bozuk par�alar i�in yer
    [Command]
    public void CmdtrashInteractID(float objectNumber)
    {
        trashInteractID(objectNumber);
        RpctrashInteractID(objectNumber);
    }

    [ClientRpc]
    public void RpctrashInteractID(float objectNumber)
    {
        trashInteractID(objectNumber);
    }

    public void trashInteractID(float objectNumber)
    {
        if (objectNumber == 0) // bo�
        {
            for (int i = 0; i < childObject.Count; i++)
            {
                childObject[i].SetActive(false);
            }

            ID = 0;
            handFull = false;
        }

        if (objectNumber == 1.3f) // kasa
        {
            childObject[1].SetActive(true);
            ID = 1.3f;
        }

        if (objectNumber == 2.3f) // anakar
        {
            childObject[2].SetActive(true);
            ID = 2.3f;
        }

        if (objectNumber == 3.3f) // CPU
        {
            childObject[3].SetActive(true);
            ID = 3.3f;
        }

        if (objectNumber == 4.3f) // ekran kart�
        {
            childObject[4].SetActive(true);
            ID = 4.3f;
        }

        if (objectNumber == 5.3f) // ram
        {
            childObject[5].SetActive(true);
            ID = 5.3f;
        }
    }
}