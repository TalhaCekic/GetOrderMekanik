using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Animations.Rigging;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : NetworkBehaviour
{
    [SerializeField] private Transform spawnObjectPrefab;
    private Transform spawnObjectPrefabTransform;

    private NetworkVariable<MyCustomData> randomNumber = new NetworkVariable<MyCustomData>(
    new MyCustomData
    {
        _int = 56,
        _bool = true,
    }, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField]
    private NetworkVariable<PlayerSettings> networkVariable = new NetworkVariable<PlayerSettings> ();

    private NetworkVariable<Vector3> networkPlayerState = new NetworkVariable<Vector3> ();

    Animator anim;
    public float speedd = 6f;
    [SerializeField] private float turnSpeed = 5f;
    private Vector3 input;

    public CharacterController controller;
    public InputAction InputAction;

    //yer�ekimi de�i�keni
    Vector3 velocity;
    public float gravity = -9.81f;

    public float acceleration = 5f;   // H�z art��� miktar� 
    public float deceleration = 5f;   // H�z azal��� miktar� 
    public float maxSpeed = 10f;      // Maksimum h�z   

    private int stationaryFrames;
    private Vector3 movement;
    private float currentSpeed;


    private bool isSprint = false;

    public struct MyCustomData : INetworkSerializable
    {
        public int _int;
        public bool _bool;
        public FixedString128Bytes message;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref _int);
            serializer.SerializeValue(ref _bool);
            serializer.SerializeValue(ref message);
        }
    }

    public override void OnNetworkSpawn()
    {
        randomNumber.OnValueChanged += (MyCustomData previousValue, MyCustomData newValue) =>
        {
            Debug.Log(OwnerClientId + " ; " + newValue._int + " ; " + newValue._bool + " ; " + newValue.message);
        };
    }
    private void Awake()
    {
        if (!IsOwner) return;


        pickUp.handFull = false;

        movement = transform.position;
        stationaryFrames = 0;

        //   InputAction = new InputAction("Sprint", InputActionType.Button, null);
        InputAction.performed += ctx => sprint();
        InputAction.canceled += ctx => StopSprint();
        InputAction.Enable();
    }
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        
        if (!pickUp.cutting)
        {
            InputRotation();
            Move();
        }


        if (pickUp.handFull == true)
        {
     // anim.SetBool("hand", true);
            
        }
        else
        {
          //  anim.SetBool("hand", false);
        }
    }
    void InputRotation()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;
        if (input != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
    }
    void Move()
    {


        movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movement.Normalize();

        velocity.y += Physics.gravity.y * Time.deltaTime; // Yer�ekimi ivmesini uygula

        if (movement.magnitude > 0f)
        {

            // H�z art���
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);
            if (isSprint)
            {
                currentSpeed = Mathf.Max(currentSpeed + acceleration * Time.deltaTime, 7);
                //anim.SetBool("isSprint", true);
                // anim.SetFloat("Speed", currentSpeed);
            }
            // D�n��
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);


        }
        else
        {
            // H�z azal���
            currentSpeed = Mathf.Max(currentSpeed * -2 - deceleration * Time.deltaTime, 0f);



           // anim.SetBool("isSprint", false);
            //anim.SetFloat("Speed", currentSpeed);
        }
        // Hareket etme
        controller.Move(transform.forward * currentSpeed * Time.deltaTime);

       
            anim.SetFloat("Speed", currentSpeed / speedd);
         







    }
    public void sprint()
    {
        isSprint = true;
    }
    private void StopSprint()
    {
        isSprint = false;
    }
  
//    [ServerRpc]
//    public void UpdatePlayerState(PlayerSettings newState)
//    {
//        networkVariable.Value = newState;
//    }
}
