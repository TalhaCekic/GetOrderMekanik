using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;

public class NewPickUp : NetworkBehaviour
{
    private RaycastHit hit;
    private float hitRange = 1;

    // LayerMask
    int pickupLayerMask;
    int pickupLayerMask2;
    int pickupLayerMask3;
    int pickupLayerMask4;
    int pickupLayerMask5;

    // Spawn olacaðý transformlar
    [SerializeField] private Transform SpawnTransform;


    // Kontorller
    GameObject gameController;
    private counter Counter;

    public GameObject cam;

    public controller playerInput;
    public RigBuilder rigBuilder;
    public GameObject objectToRemove;


    private void Start()
    {
        rigBuilder = GetComponent<RigBuilder>();
        playerInput = new controller();
        gameController = GameObject.FindGameObjectWithTag("GameController");
        Counter = FindObjectOfType<counter>();

        pickupLayerMask = LayerMask.GetMask("Pickup");
        pickupLayerMask2 = LayerMask.GetMask("counter");
        pickupLayerMask3 = LayerMask.GetMask("trash");
        pickupLayerMask4 = LayerMask.GetMask("dinnerTable");
        pickupLayerMask5 = LayerMask.GetMask("cuttingTableCounter");
    }

}
