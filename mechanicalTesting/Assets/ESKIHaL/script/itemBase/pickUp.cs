using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using Unity.Netcode;

public class pickUp : NetworkBehaviour
{
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

    GameObject gameController;
    private counter Counter;

    public GameObject cam;

    public GameObject burger;         //id : 1
    public GameObject dirtyPlate;     //id : 2.2
    public GameObject cleanPlate;     //id : 2
    public GameObject meatRaw;        //id : 3.3
    public GameObject meatBaked;      //id : 3
    public GameObject tomato;         //id : 4.4
    public GameObject SliceTomato;    //id : 4
    public GameObject lettuce;        //id : 5.5
    public GameObject SliceLettuce;   //id : 5
    public GameObject cheddarCheese;  //id : 6

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
    // karakterin elindeki tanýma ýd si0
    public NetworkVariable<float> ID = new NetworkVariable<float>();
    public string Hand = " Null ";

    private bool notCombine;
    public static bool cutting = false;


    public NetworkVariable<controller>  playerInput = new NetworkVariable<controller>();

    public RigBuilder rigBuilder;
    public GameObject objectToRemove; // Kaldýrýlacak objeyi atan deðiþken
    public GameObject testIK;
    void Start()
    {
        if (!IsOwner) return;
        rigBuilder = GetComponent<RigBuilder>();
        playerInput.Value = new controller();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        Counter = FindObjectOfType<counter>();

        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
        pickupLayerMask4 = LayerMask.GetMask("dinnerTable");
        pickupLayerMask5 = LayerMask.GetMask("cuttingTableCounter");
    }
    void Update()
    {
        if (!IsOwner) return;
        playerInput.Value.Player.Interact.performed += x => Interact();
        playerInput.Value.Player.Drop.performed += x => Drop();

        playerInput.Value.Player.CuttingWash.performed += PressCuttingAndWashing;

        IDcheck();


        //rig sistemini aktif ve deaktif 
     //   rigBuilder.enabled = cutting;
        if (rigBuilder.enabled == true) // kol animasyonunu tetikler
        {
    
        }
    }

    //input sistemi etkileþimler
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
                    if (ID.Value == 1)
                    {
                        if (!counter.burgerdolu)
                        {
                            counter.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2)
                    {
                        if (!counter.cleanPlatedolu)
                        {
                            counter.cleanPlatedolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3)
                    {
                        if (!counter.meatbakeddolu)
                        {
                            counter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 4)
                    {
                        if (!counter.tomatoSliceDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 5)
                    {
                        if (!counter.lettuceSliceDolu)
                        {
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 6)
                    {
                        if (!counter.cheddarCheeseDolu)
                        {
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 14)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 15)
                    {
                        if (!counter.burgerdolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 16)
                    {
                        if (!counter.burgerdolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 24)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 25)
                    {
                        if (!counter.cleanPlatedolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 26)
                    {
                        if (!counter.cleanPlatedolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 34)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 35)
                    {
                        if (!counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 36)
                    {
                        if (!counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 45)
                    {
                        if (!counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 46)
                    {
                        if (!counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 56)
                    {
                        if (!counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 124)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 125)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 126)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 134)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 135)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 136)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 145)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 146)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 156)
                    {
                        if (!counter.burgerdolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 234)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 235)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 236)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 245)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 246)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 256)
                    {
                        if (!counter.cleanPlatedolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 345)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 346)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 356)
                    {
                        if (!counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 456)
                    {
                        if (!counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 1234)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1235)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1236)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1245)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1246)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1256)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1345)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1346)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1356)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1456)
                    {
                        if (!counter.burgerdolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2345)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2346)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2356)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2456)
                    {
                        if (!counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3456)
                    {
                        if (!counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 12345)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12346)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12356)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12456)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13456)
                    {
                        if (!counter.burgerdolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23456)
                    {
                        if (!counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123456)
                    {
                        if (!counter.burgerdolu && !counter.cleanPlatedolu && !counter.meatbakeddolu && !counter.tomatoSliceDolu && !counter.lettuceSliceDolu && !counter.cheddarCheeseDolu)
                        {
                            counter.burgerdolu = true;
                            counter.cleanPlatedolu = true;
                            counter.meatbakeddolu = true;
                            counter.tomatoSliceDolu = true;
                            counter.lettuceSliceDolu = true;
                            counter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                }
                if (ID.Value == 2.2f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 2.2f;
                        counter.dirtyPlatedolu = false;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 3.3f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 3.3f;
                        counter.meatRawdolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 4.4f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 4.4f;
                        counter.tomatodolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 5.5f)
                {
                    if (counter.counterID == 0)
                    {
                        counter.counterID = 5.5f;
                        counter.lettucedolu = true;
                        ID.Value = 0;
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
                    if (ID.Value == 1)
                    {
                        if (!furnace.burgerdolu)
                        {
                            furnace.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2)
                    {
                        if (!furnace.cleanPlatedolu)
                        {
                            furnace.cleanPlatedolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3)
                    {
                        if (!furnace.meatbakeddolu)
                        {
                            furnace.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 4)
                    {
                        if (!furnace.tomatoSliceDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 5)
                    {
                        if (!furnace.lettuceSliceDolu)
                        {
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 6)
                    {
                        if (!furnace.cheddarCheeseDolu)
                        {
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 14)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 15)
                    {
                        if (!furnace.burgerdolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 16)
                    {
                        if (!furnace.burgerdolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 24)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 25)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 26)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 34)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 35)
                    {
                        if (!furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 36)
                    {
                        if (!furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 45)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 46)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 56)
                    {
                        if (!furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 124)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 125)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 126)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 134)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 135)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 136)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 145)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 146)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 156)
                    {
                        if (!furnace.burgerdolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 234)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 235)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 236)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 245)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 246)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 256)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 345)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 346)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 356)
                    {
                        if (!furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 456)
                    {
                        if (!furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 1234)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1235)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1236)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1245)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1246)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1256)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1345)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1346)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1356)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1456)
                    {
                        if (!furnace.burgerdolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2345)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2346)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2356)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2456)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3456)
                    {
                        if (!furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 12345)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12346)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12356)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12456)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13456)
                    {
                        if (!furnace.burgerdolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23456)
                    {
                        if (!furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123456)
                    {
                        if (!furnace.burgerdolu && !furnace.cleanPlatedolu && !furnace.meatbakeddolu && !furnace.tomatoSliceDolu && !furnace.lettuceSliceDolu && !furnace.cheddarCheeseDolu)
                        {
                            furnace.burgerdolu = true;
                            furnace.cleanPlatedolu = true;
                            furnace.meatbakeddolu = true;
                            furnace.tomatoSliceDolu = true;
                            furnace.lettuceSliceDolu = true;
                            furnace.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                }
                if (ID.Value == 2.2f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 2.2f;
                        furnace.dirtyPlatedolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 3.3f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 3.3f;
                        furnace.meatRawdolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 4.4f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 4.4f;
                        furnace.tomatodolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 5.5f)
                {
                    if (furnace.counterID == 0)
                    {
                        furnace.counterID = 5.5f;
                        furnace.lettucedolu = true;
                        ID.Value = 0;
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
                    if (ID.Value == 1)
                    {
                        if (!dinnerTable.burgerdolu)
                        {
                            dinnerTable.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2)
                    {
                        if (!dinnerTable.cleanPlatedolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3)
                    {
                        if (!dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 4)
                    {
                        if (!dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 5)
                    {
                        if (!dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 6)
                    {
                        if (!dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 14)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 15)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 16)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 24)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 25)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 26)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 34)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 35)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 36)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 45)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 46)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 56)
                    {
                        if (!dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 124)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 125)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 126)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 134)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 135)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 136)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 145)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 146)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 156)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 234)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 235)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 236)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 245)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 246)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 256)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 345)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 346)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 356)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 456)
                    {
                        if (!dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 1234)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1235)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1236)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1245)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1246)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1256)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1345)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1346)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1356)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2345)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2346)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2356)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2456)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3456)
                    {
                        if (!dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 12345)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12346)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12356)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23456)
                    {
                        if (!dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123456)
                    {
                        if (!dinnerTable.burgerdolu && !dinnerTable.cleanPlatedolu && !dinnerTable.meatbakeddolu && !dinnerTable.tomatoSliceDolu && !dinnerTable.lettuceSliceDolu && !dinnerTable.cheddarCheeseDolu)
                        {
                            dinnerTable.burgerdolu = true;
                            dinnerTable.cleanPlatedolu = true;
                            dinnerTable.meatbakeddolu = true;
                            dinnerTable.tomatoSliceDolu = true;
                            dinnerTable.lettuceSliceDolu = true;
                            dinnerTable.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                }
                if (ID.Value == 2.2f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 2.2f;
                        dinnerTable.dirtyPlatedolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 3.3f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 3.3f;
                        dinnerTable.meatRawdolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 4.4f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 4.4f;
                        dinnerTable.tomatodolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 5.5f)
                {
                    if (dinnerTable.counterID == 0)
                    {
                        dinnerTable.counterID = 5.5f;
                        dinnerTable.lettucedolu = true;
                        ID.Value = 0;
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
                    if (ID.Value == 1)
                    {
                        if (!CuttingBoardCounter.burgerdolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 4)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 5)
                    {
                        if (!CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 6)
                    {
                        if (!CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.burgerdolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 14)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 15)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 16)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 24)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 25)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 26)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 34)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 35)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 36)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 45)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 46)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 56)
                    {
                        if (!CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 124)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 125)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 126)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 134)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 135)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 136)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 145)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 146)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 156)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 234)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 235)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 236)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 245)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 246)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 256)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 345)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 346)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 356)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 456)
                    {
                        if (!CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 1234)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1235)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1236)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1245)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1246)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1256)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1345)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1346)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1356)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 1456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2345)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2346)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2356)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 2456)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 3456)
                    {
                        if (!CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 12345)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12346)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12356)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 12456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 13456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                    if (ID.Value == 23456)
                    {
                        if (!CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }

                    if (ID.Value == 123456)
                    {
                        if (!CuttingBoardCounter.burgerdolu && !CuttingBoardCounter.cleanPlatedolu && !CuttingBoardCounter.meatbakeddolu && !CuttingBoardCounter.tomatoSliceDolu && !CuttingBoardCounter.lettuceSliceDolu && !CuttingBoardCounter.cheddarCheeseDolu)
                        {
                            CuttingBoardCounter.burgerdolu = true;
                            CuttingBoardCounter.cleanPlatedolu = true;
                            CuttingBoardCounter.meatbakeddolu = true;
                            CuttingBoardCounter.tomatoSliceDolu = true;
                            CuttingBoardCounter.lettuceSliceDolu = true;
                            CuttingBoardCounter.cheddarCheeseDolu = true;
                            ID.Value = 0;
                        }
                    }
                }
                if (ID.Value == 2.2f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 2.2f;
                        CuttingBoardCounter.dirtyPlatedolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 3.3f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 3.3f;
                        CuttingBoardCounter.meatRawdolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 4.4f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 4.4f;
                        CuttingBoardCounter.tomatodolu = true;
                        ID.Value = 0;
                    }
                }
                if (ID.Value == 5.5f)
                {
                    if (CuttingBoardCounter.counterID == 0)
                    {
                        CuttingBoardCounter.counterID = 5.5f;
                        CuttingBoardCounter.lettucedolu = true;
                        ID.Value = 0;
                    }
                }
            }
        }
        //çöp kutusu etkileþimi;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask3))
        {
            if (ID.Value == 1 || ID.Value == 3.3f || ID.Value == 3 || ID.Value == 4.4f || ID.Value == 4 || ID.Value == 5.5f || ID.Value == 5 || ID.Value == 6f)
            {
                if (cleanPlate.activeSelf == true)
                {
                    burger.SetActive(false);
                    meatRaw.SetActive(false);
                    meatBaked.SetActive(false);
                    handFull = true;
                    ID.Value = 2;
                }
                else
                {
                    ID.Value = 0;
                }
            }
        }
    }
    public void Interact()
    {
        //counter;
        if (Physics.Raycast(this.transform.position, transform.TransformDirection(Vector3.forward), out hit, hitRange, pickupLayerMask2))
        {
            if (hit.collider.gameObject.TryGetComponent<counter>(out var counter) && handFull == false)
            {
                float value = counter.counterID;

                if (ID.Value == 0)
                {
                    if (counter.counterID != 0)
                    {
                        ID.Value = counter.counterID;

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
                if (ID.Value == 0)
                {
                    ID.Value = furnace.counterID;
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
                if (ID.Value == 0)
                {
                    if (dinnerTable.counterID != 0)
                    {
                        ID.Value = dinnerTable.counterID;

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
                if (ID.Value == 0)
                {
                    if (CuttingBoardCounter.counterID != 0)
                    {
                        ID.Value = CuttingBoardCounter.counterID;

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
                ID.Value = 1;
                handFull = true;
            }
            if (id == 2 && handFull == false)
            {
                ID.Value = 2;
                handFull = true;
            }
            if (id == 3.3f && handFull == false)
            {
                ID.Value = 3.3f;
                handFull = true;
            }
            if (id == 4.4f && handFull == false)
            {
                ID.Value = 4.4f;
                handFull = true;
            }
            if (id == 5.5f && handFull == false)
            {
                ID.Value = 5.5f;
                handFull = true;
            }
            if (id == 6f && handFull == false)
            {
                ID.Value = 6f;
                handFull = true;
            }
            if (id == 6 && handFull == false)
            {
                ID.Value = 6f;
                handFull = true;
            }
            // elde item varken kombinasyon yapmamýzý saðlar.
            if (id == 1)
            {
                if (ID.Value == 2)
                {
                    ID.Value = 12;
                }
                if (ID.Value == 3)
                {
                    ID.Value = 13;
                }
                if (ID.Value == 4)
                {
                    ID.Value = 14;
                }
                if (ID.Value == 5)
                {
                    ID.Value = 15;
                }
                if (ID.Value == 6)
                {
                    ID.Value = 16;
                }
                if (ID.Value == 23)
                {
                    ID.Value = 123;
                }
                if (ID.Value == 24)
                {
                    ID.Value = 124;
                }
                if (ID.Value == 25)
                {
                    ID.Value = 125;
                }
                if (ID.Value == 26)
                {
                    ID.Value = 126;
                }
                if (ID.Value == 34)
                {
                    ID.Value = 134;
                }
                if (ID.Value == 35)
                {
                    ID.Value = 135;
                }
                if (ID.Value == 36)
                {
                    ID.Value = 136;
                }
                if (ID.Value == 45)
                {
                    ID.Value = 145;
                }
                if (ID.Value == 46)
                {
                    ID.Value = 146;
                }
                if (ID.Value == 56)
                {
                    ID.Value = 156;
                }
                if (ID.Value == 234)
                {
                    ID.Value = 1234;
                }
                if (ID.Value == 235)
                {
                    ID.Value = 1235;
                }
                if (ID.Value == 236)
                {
                    ID.Value = 1236;
                }
                if (ID.Value == 245)
                {
                    ID.Value = 1245;
                }
                if (ID.Value == 246)
                {
                    ID.Value = 1246;
                }
                if (ID.Value == 256)
                {
                    ID.Value = 1256;
                }
                if (ID.Value == 345)
                {
                    ID.Value = 1345;
                }
                if (ID.Value == 346)
                {
                    ID.Value = 1346;
                }
                if (ID.Value == 356)
                {
                    ID.Value = 1356;
                }
                if (ID.Value == 456)
                {
                    ID.Value = 1456;
                }
                if (ID.Value == 2345)
                {
                    ID.Value = 12345;
                }
                if (ID.Value == 2346)
                {
                    ID.Value = 12346;
                }
                if (ID.Value == 2356)
                {
                    ID.Value = 12356;
                }
                if (ID.Value == 2456)
                {
                    ID.Value = 12456;
                }
                if (ID.Value == 3456)
                {
                    ID.Value = 13456;
                }
                if (ID.Value == 23456)
                {
                    ID.Value = 123456;
                }
            }
            if (id == 2)
            {
                if (ID.Value == 1)
                {
                    ID.Value = 12;
                }
                if (ID.Value == 3)
                {
                    ID.Value = 23;
                }
                if (ID.Value == 4)
                {
                    ID.Value = 24;
                }
                if (ID.Value == 5)
                {
                    ID.Value = 25;
                }
                if (ID.Value == 6)
                {
                    ID.Value = 26;
                }
                if (ID.Value == 13)
                {
                    ID.Value = 123;
                }
                if (ID.Value == 14)
                {
                    ID.Value = 124;
                }
                if (ID.Value == 15)
                {
                    ID.Value = 125;
                }
                if (ID.Value == 16)
                {
                    ID.Value = 126;
                }
                if (ID.Value == 34)
                {
                    ID.Value = 234;
                }
                if (ID.Value == 35)
                {
                    ID.Value = 235;
                }
                if (ID.Value == 36)
                {
                    ID.Value = 236;
                }
                if (ID.Value == 45)
                {
                    ID.Value = 245;
                }
                if (ID.Value == 46)
                {
                    ID.Value = 246;
                }
                if (ID.Value == 56)
                {
                    ID.Value = 256;
                }
                if (ID.Value == 134)
                {
                    ID.Value = 1234;
                }
                if (ID.Value == 135)
                {
                    ID.Value = 1235;
                }
                if (ID.Value == 136)
                {
                    ID.Value = 1236;
                }
                if (ID.Value == 145)
                {
                    ID.Value = 1245;
                }
                if (ID.Value == 146)
                {
                    ID.Value = 1246;
                }
                if (ID.Value == 156)
                {
                    ID.Value = 1256;
                }
                if (ID.Value == 345)
                {
                    ID.Value = 2345;
                }
                if (ID.Value == 346)
                {
                    ID.Value = 2346;
                }
                if (ID.Value == 356)
                {
                    ID.Value = 2356;
                }
                if (ID.Value == 456)
                {
                    ID.Value = 2456;
                }
                if (ID.Value == 1345)
                {
                    ID.Value = 12345;
                }
                if (ID.Value == 1346)
                {
                    ID.Value = 12346;
                }
                if (ID.Value == 1356)
                {
                    ID.Value = 12356;
                }
                if (ID.Value == 1456)
                {
                    ID.Value = 12456;
                }
                if (ID.Value == 3456)
                {
                    ID.Value = 23456;
                }
                if (ID.Value == 13456)
                {
                    ID.Value = 123456;
                }
            }
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

    private void IDcheck()
    {
        if (ID.Value == 0)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
            handFull = false;
        }
        if (ID.Value > 0)
        {
            handFull = true;
            // Tekli Kombinasyon
            if (ID.Value == 1)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 2)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);  // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 2.2)
            {
                handFull = true;
                notCombine = true;
                burger.SetActive(false);
                dirtyPlate.SetActive(true);   // TRUE
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 3)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);   // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 3.3f)
            {
                handFull = true;
                notCombine = true;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(true);     // TRUE
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 4)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);    // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 4.4f)
            {
                handFull = true;
                notCombine = true;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(true);           // TRUE
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 5)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 5.5f)
            {
                handFull = true;
                notCombine = true;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(true);            // TRUE
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 6)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);     // TRUE
            }

            // 2 li kombinasyon
            if (ID.Value == 12)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);          // true
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // true
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 13)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);       // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);     // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 14)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);             // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);       // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 15)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);             // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);      // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 16)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(true);                 // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);         // TRUE
            }
            if (ID.Value == 23)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);   // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);    // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 24)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);        // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 25)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 26)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);     // TRUE
            }
            if (ID.Value == 34)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);     // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 35)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);         // true
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // true
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 36)
            {
                handFull = true;
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);          // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);      // TRUE
            }
            if (ID.Value == 45)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);          // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);         // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 46)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);          // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);       // TRUE
            }
            if (ID.Value == 56)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);          // TRUE
                cheddarCheese.SetActive(true);         // TRUE
            }

            // 3 li kombinasyon
            if (ID.Value == 123)
            {
                notCombine = false;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 124)
            {
                notCombine = false;
                burger.SetActive(true);            // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);        // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 125)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 126)
            {
                notCombine = false;
                burger.SetActive(true);          // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);     // TRUE
            }
            if (ID.Value == 134)
            {
                notCombine = false;
                burger.SetActive(true);        // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);     // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 135)
            {
                notCombine = false;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 136)
            {
                notCombine = false;
                burger.SetActive(true);             // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);          // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);      // TRUE
            }
            if (ID.Value == 145)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 146)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 156)
            {
                notCombine = false;
                burger.SetActive(true);        // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);  // TRUE
            }
            if (ID.Value == 234)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 235)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);    // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 236)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);     // TRUE
            }
            if (ID.Value == 245)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 246)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);    // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true); // TRUE
            }
            if (ID.Value == 256)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(true);  // TRUE
            }
            if (ID.Value == 345)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);          // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);        // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);       // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 346)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);   // TRUE
            }
            if (ID.Value == 356)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);        // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 456)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(true);   // TRUE
            }

            // 4 li kombinasyon
            if (ID.Value == 1234)
            {
                notCombine = false;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 1235)
            {
                notCombine = false;
                burger.SetActive(true);            // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);        // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);      // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 1236)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);        // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 1245)
            {
                notCombine = false;
                burger.SetActive(true);          // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 1246)
            {
                notCombine = false;
                burger.SetActive(true);        // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);    // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);   // TRUE
            }
            if (ID.Value == 1256)
            {
                notCombine = false;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(true);  // TRUE
            }
            if (ID.Value == 1345)
            {
                notCombine = false;
                burger.SetActive(true);            // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);         // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);       // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);      // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 1346)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);        // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 1356)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);        // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 1456)
            {
                notCombine = false;
                burger.SetActive(true);        // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(true);  // TRUE
            }
            if (ID.Value == 2345)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 2346)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);      // TRUE
            }
            if (ID.Value == 2356)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);      // TRUE
                cheddarCheese.SetActive(true);     // TRUE
            }
            if (ID.Value == 2456)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 3456)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);     // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);   // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);  // TRUE
                cheddarCheese.SetActive(true); // TRUE
            }

            // 5 li kombinasyon
            if (ID.Value == 12345)
            {
                notCombine = false;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);    // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(false);
            }
            if (ID.Value == 12346)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);        // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);          // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);        // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(false);
                cheddarCheese.SetActive(true);        // TRUE
            }
            if (ID.Value == 12356)
            {
                notCombine = false;
                burger.SetActive(true);          // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(false);
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);   // TRUE
            }
            if (ID.Value == 12456)
            {
                notCombine = false;
                burger.SetActive(true);          // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);      // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(false);
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 13456)
            {
                notCombine = false;
                burger.SetActive(true);           // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(false);
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);       // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);     // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);    // TRUE
                cheddarCheese.SetActive(true);   // TRUE
            }
            if (ID.Value == 23456)
            {
                notCombine = false;
                burger.SetActive(false);
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);       // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);        // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);      // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);     // TRUE
                cheddarCheese.SetActive(true);    // TRUE
            }
            if (ID.Value == 123456)
            {
                notCombine = true;
                burger.SetActive(true);         // TRUE
                dirtyPlate.SetActive(false);
                cleanPlate.SetActive(true);     // TRUE
                meatRaw.SetActive(false);
                meatBaked.SetActive(true);      // TRUE
                tomato.SetActive(false);
                SliceTomato.SetActive(true);    // TRUE
                lettuce.SetActive(false);
                SliceLettuce.SetActive(true);   // TRUE
                cheddarCheese.SetActive(true);  // TRUE
            }
        }

    }
}


