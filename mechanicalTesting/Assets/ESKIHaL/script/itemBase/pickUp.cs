
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using System.Collections.Generic;

public class pickUp : NetworkBehaviour
{
    // karakterin elindeki tanýma ýd si0
    [SyncVar] public float ID;

    public bool test = false;
    // public static pickUp instance;
    // RAY
    private RaycastHit hit;
    [SerializeField]
    [Min(1)]
    private float hitRange = 1;

    int pickupLayerMask;
    int pickupLayerMask2;
    int pickupLayerMask3;
    int pickupLayerMask4;
    int pickupLayerMask5;

    public GameObject cam;

    public List<GameObject> childObject = new List<GameObject>();

    public bool Burgergonder;
    public bool Plategonder;
    public bool MeatRawgonder;
    public bool MeatBakedgonder;
    public bool tomatogonder;
    public bool tomatoSlicegonder;
    public bool lettuceogonder;
    public bool lettuceSlicegonder;

    // elimizi kontrole eder.
    public static bool handFull = false;
    [SyncVar]
    public bool full = false;
    private bool notCombine;
    public static bool cutting = false;

    public Transform burgerPoint;

    public controller playerInput;

    void Start()
    {
        childObject[0].SetActive(false);
        if (!isLocalPlayer) return;
        playerInput = new controller();

        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
        pickupLayerMask4 = LayerMask.GetMask("dinnerTable");
        pickupLayerMask5 = LayerMask.GetMask("cuttingTableCounter");

        int randomNumber = Random.Range(0, 10);    
    }
    void Update()
    {
        if (!isLocalPlayer) return;
        //playerInput.Player.Drop.performed += x => Drop();
        //playerInput.Player.Interact.performed += x => Interact();
        //playerInput.Player.CuttingWash.performed += PressCuttingAndWashing;
        //full = handFull;

        RpcIDcheck();
        //  IDcheck();

    }

    public void Drop()
    {
        //counter
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == true)
            {
                float value = counter.counterID;
                if (!counter.notCombine)
                {
                    if (ID == 1)
                    {
                        if (!counter.burgerdolu)
                        {
                            counter.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2)
                    {
                        if (!counter.cleanPlatedolu)
                        {
                            counter.cleanPlatedolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3)
                    {
                        if (!counter.meatbakeddolu)
                        {
                            counter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 4)
                    {
                        if (!counter.tomatoSliceDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 5)
                    {
                        if (!counter.lettuceSliceDolu)
                        {
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 6)
                    {
                        if (!counter.cheddarCheeseDolu)
                        {
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 14)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 15)
                    {
                        if (!counter.burgerdolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 16)
                    {
                        if (!counter.burgerdolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 24)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 25)
                    {
                        if (!counter.cleanPlatedolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 26)
                    {
                        if (!counter.cleanPlatedolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 34)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 35)
                    {
                        if (!counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 36)
                    {
                        if (!counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 45)
                    {
                        if (!counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 46)
                    {
                        if (!counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 56)
                    {
                        if (!counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 124)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 125)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 126)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 134)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 135)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 136)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 145)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 146)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 156)
                    {
                        if (!counter.burgerdolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 234)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 235)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 236)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 245)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 246)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 256)
                    {
                        if (!counter.cleanPlatedolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 345)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 346)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 356)
                    {
                        if (!counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 456)
                    {
                        if (!counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 1234)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1235)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1236)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1245)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1246)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1256)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1345)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1346)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1356)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1456)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2345)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2346)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2356)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2456)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3456)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 12345)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12346)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12356)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12456)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                            ID = 0;
                        }
                    }
                    if (ID == 13456)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23456)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123456)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                }
                if (ID == 2.2f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 2.2f;
                        counter.dirtyPlatedolu = false;
                        ID = 0;
                    }
                }
                if (ID == 3.3f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 3.3f;
                        counter.meatRawdolu = true;
                        ID = 0;
                    }
                }
                if (ID == 4.4f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 4.4f;
                        counter.tomatodolu = true;
                        ID = 0;
                    }
                }
                if (ID == 5.5f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 5.5f;
                        counter.lettucedolu = true;
                        ID = 0;
                    }
                }
            }
        }
        //furnace;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<furnace>(out var furnace) && handFull == true)
            {
                float value = furnace.counterID;
                if (!furnace.notCombine)
                {
                    if (ID == 1)
                    {
                        if (!furnace.burgerdolu)
                        {
                            furnace.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2)
                    {
                        if (!furnace.cleanPlatedolu)
                        {
                            furnace.cleanPlatedolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3)
                    {
                        if (!furnace.meatbakeddolu)
                        {
                            furnace.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 4)
                    {
                        if (!furnace.tomatoSliceDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 5)
                    {
                        if (!furnace.lettuceSliceDolu)
                        {
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 6)
                    {
                        if (!furnace.cheddarCheeseDolu)
                        {
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 14)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 15)
                    {
                        if (!furnace.burgerdolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 16)
                    {
                        if (!furnace.burgerdolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 24)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 25)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 26)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 34)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 35)
                    {
                        if (!furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 36)
                    {
                        if (!furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 45)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 46)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 56)
                    {
                        if (!furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 124)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 125)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 126)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 134)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 135)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 136)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 145)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 146)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 156)
                    {
                        if (!furnace.burgerdolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 234)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 235)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 236)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 245)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 246)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 256)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 345)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 346)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 356)
                    {
                        if (!furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 456)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 1234)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1235)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1236)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1245)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1246)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1256)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1345)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1346)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1356)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1456)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2345)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2346)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2356)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2456)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3456)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 12345)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12346)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12356)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12456)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13456)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23456)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123456)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                }
                if (ID == 2.2f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 2.2f;
                        furnace.dirtyPlatedolu = true;
                        ID = 0;
                    }
                }
                if (ID == 3.3f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 3.3f;
                        furnace.meatRawdolu = true;
                        ID = 0;
                    }
                }
                if (ID == 4.4f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 4.4f;
                        furnace.tomatodolu = true;
                        ID = 0;
                    }
                }
                if (ID == 5.5f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 5.5f;
                        furnace.lettucedolu = true;
                        ID = 0;
                    }
                }
            }
        }
        //dinnerTable;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask4))
        {
            if (hit.collider.gameObject.TryGetComponent<dinnerTable>(out var dinnerTable) && handFull == true)
            {
                float value = dinnerTable.counterID;
                if (!dinnerTable.notCombine)
                {
                    if (ID == 1)
                    {
                        if (!dinnerTable.burgerdolu)
                        {
                            dinnerTable.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2)
                    {
                        if (!dinnerTable.cleanPlatedolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3)
                    {
                        if (!dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 4)
                    {
                        if (!dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 5)
                    {
                        if (!dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 6)
                    {
                        if (!dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 14)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 15)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 16)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 24)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 25)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 26)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 34)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 35)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 36)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 45)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 46)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 56)
                    {
                        if (!dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 124)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 125)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 126)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 134)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 135)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 136)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 145)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 146)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 156)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 234)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 235)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 236)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 245)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 246)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 256)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 345)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 346)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 356)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 456)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 1234)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1235)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1236)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1245)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1246)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1256)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1345)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1346)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1356)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2345)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2346)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2356)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2456)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3456)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 12345)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12346)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12356)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23456)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                }
                if (ID == 2.2f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 2.2f;
                        dinnerTable.dirtyPlatedolu = true;
                        ID = 0;
                    }
                }
                if (ID == 3.3f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 3.3f;
                        dinnerTable.meatRawdolu = true;
                        ID = 0;
                    }
                }
                if (ID == 4.4f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 4.4f;
                        dinnerTable.tomatodolu = true;
                        ID = 0;
                    }
                }
                if (ID == 5.5f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 5.5f;
                        dinnerTable.lettucedolu = true;
                        ID = 0;
                    }
                }
            }
        }
        //cuttingTable;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask5))
        {
            if (hit.collider.gameObject.TryGetComponent<CuttingBoardCounter>(out var CuttingBoardCounter) && handFull == true)
            {
                float value = CuttingBoardCounter.counterID;
                if (!CuttingBoardCounter.notCombine)
                {
                    if (ID == 1)
                    {
                        if (!CuttingBoardCounter.burgerdolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 4)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 5)
                    {
                        if (!CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 6)
                    {
                        if (!CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.burgerdolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 14)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 15)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 16)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 24)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 25)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 26)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 34)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 35)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 36)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 45)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 46)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 56)
                    {
                        if (!CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 124)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 125)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 126)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 134)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 135)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 136)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 145)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 146)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 156)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 234)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 235)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 236)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 245)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 246)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 256)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 345)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 346)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 356)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 456)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 1234)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1235)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1236)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1245)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1246)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1256)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1345)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1346)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1356)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 1456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2345)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2346)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2356)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 2456)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 3456)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 12345)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12346)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12356)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 12456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 13456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                    if (ID == 23456)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }

                    if (ID == 123456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID = 0;
                        }
                    }
                }
                if (ID == 2.2f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 2.2f;
                        CuttingBoardCounter.dirtyPlatedolu = true;
                        ID = 0;
                    }
                }
                if (ID == 3.3f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 3.3f;
                        CuttingBoardCounter.meatRawdolu = true;
                        ID = 0;
                    }
                }
                if (ID == 4.4f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 4.4f;
                        CuttingBoardCounter.tomatodolu = true;
                        ID = 0;
                    }
                }
                if (ID == 5.5f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 5.5f;
                        CuttingBoardCounter.lettucedolu = true;
                        ID = 0;
                    }
                }
            }
        }
        //çöp kutusu etkileþimi;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask3))
        {
            if (ID == 1 || ID == 3.3f || ID == 3 || ID == 4.4f || ID == 4 || ID == 5.5f || ID == 5 || ID == 6f)
            {





            }
        }
    }
    public void Interact()
    {
        if (!isLocalPlayer) return;
        //  counter;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == false)
            {
                float value = counter.counterID;

                if (ID == 0)
                {
                    if (counter.counterID != 0)
                    {
                        ID = counter.counterID;

                        counter.counterID = 0;
                        counter.burgerdolu = false;
                        counter.dirtyPlatedolu = false;
                        counter.cleanPlatedolu = false;
                        counter.meatRawdolu = false;
                        counter.meatbakeddolu = false;
                        counter.tomatodolu = false;
                        counter.tomatoSliceDolu = false;
                        counter.lettucedolu = false;
                        counter.lettuceSliceDolu = false;
                        counter.cheddarCheeseDolu = false;
                    }
                }
            }
        }
        //furnace;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<furnace>(out var furnace) && handFull == false)
            {
                float value = furnace.counterID;
                if (ID == 0)
                {
                    ID = furnace.counterID;
                    if (furnace.counterID != 0)
                    {
                        furnace.counterID = 0;
                        furnace.burgerdolu = false;
                        furnace.dirtyPlatedolu = false;
                        furnace.cleanPlatedolu = false;
                        furnace.meatRawdolu = false;
                        furnace.meatbakeddolu = false;
                        furnace.tomatodolu = false;
                        furnace.tomatoSliceDolu = false;
                        furnace.lettucedolu = false;
                        furnace.lettuceSliceDolu = false;
                        furnace.cheddarCheeseDolu = false;
                    }
                }
            }
        }
        //dinnerTable;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask4))
        {
            if (hit.collider.gameObject.TryGetComponent<dinnerTable>(out var dinnerTable) && handFull == false)
            {
                float value = dinnerTable.counterID;
                if (ID == 0)
                {
                    if (dinnerTable.counterID != 0)
                    {
                        ID = dinnerTable.counterID;

                        dinnerTable.counterID = 0;
                        dinnerTable.burgerdolu = false;
                        dinnerTable.dirtyPlatedolu = false;
                        dinnerTable.cleanPlatedolu = false;
                        dinnerTable.meatRawdolu = false;
                        dinnerTable.meatbakeddolu = false;
                        dinnerTable.tomatodolu = false;
                        dinnerTable.tomatoSliceDolu = false;
                        dinnerTable.lettucedolu = false;
                        dinnerTable.lettuceSliceDolu = false;
                        dinnerTable.cheddarCheeseDolu = false;
                    }
                }
            }
        }
        //cuttingTable;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask5))
        {
            if (hit.collider.gameObject.TryGetComponent<CuttingBoardCounter>(out var CuttingBoardCounter) && handFull == false)
            {
                float value = CuttingBoardCounter.counterID;
                if (ID == 0)
                {
                    if (CuttingBoardCounter.counterID != 0)
                    {
                        ID = CuttingBoardCounter.counterID;

                        CuttingBoardCounter.counterID = 0;
                        CuttingBoardCounter.burgerdolu = false;
                        CuttingBoardCounter.dirtyPlatedolu = false;
                        CuttingBoardCounter.cleanPlatedolu = false;
                        CuttingBoardCounter.meatRawdolu = false;
                        CuttingBoardCounter.meatbakeddolu = false;
                        CuttingBoardCounter.tomatodolu = false;
                        CuttingBoardCounter.tomatoSliceDolu = false;
                        CuttingBoardCounter.lettucedolu = false;
                        CuttingBoardCounter.lettuceSliceDolu = false;
                        CuttingBoardCounter.cheddarCheeseDolu = false;
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
                //ID = 1;
                //handFull = true;
                CmdSwitch(0);
            }
            if (id == 2 && handFull == false)
            {
                ID = 2;
                handFull = true;
            }
            if (id == 3.3f && handFull == false)
            {
                ID = 3.3f;
                handFull = true;
            }
            if (id == 4.4f && handFull == false)
            {
                ID = 4.4f;
                handFull = true;
            }
            if (id == 5.5f && handFull == false)
            {
                ID = 5.5f;
                handFull = true;
            }
            if (id == 6f && handFull == false)
            {
                ID = 6f;
                handFull = true;
            }
            if (id == 6 && handFull == false)
            {
                ID = 6f;
                handFull = true;
            }
        }
    }
    [Command]
    public void CmdSwitch(int objectNumber)
    {
        Switch(objectNumber);
        RpcSwitch(objectNumber);
    }
    [ClientRpc]
    void RpcSwitch(int objectNumber)
    {

        Switch(objectNumber);

    }
    public void Switch(int objectNumber)
    {
        //Activate the selected object, deactivate the others
        if (objectNumber == 0)
        {
            
             childObject[0].SetActive(true);
                ID = 1;
            
        }
    }

    public void PressCuttingAndWashing(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask5))
        {
            if (hit.collider.gameObject.TryGetComponent<CuttingBoardCounter>(out var CuttingBoardCounter))
            {
                float value = CuttingBoardCounter.counterID;
                if (context.started)
                {
                    cutting = false;
                }
                if (context.performed)
                {
                    if (CuttingBoardCounter.counterID == 4.4f || CuttingBoardCounter.counterID == 5.5f || CuttingBoardCounter.counterID == 6.6f)
                    {
                        cutting = true;
                    }
                    else
                    {
                        cutting = false;
                    }
                }
                if (context.canceled)
                {
                    cutting = false;
                }
            }
        }
    }
    public void PressCuttingAndWashing2()
    {
        Debug.LogError("performed");
    }
    public void PressCuttingAndWashing3()
    {
        Debug.LogError("canceled");
    }


}


