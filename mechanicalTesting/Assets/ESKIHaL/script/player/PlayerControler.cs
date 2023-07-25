using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.SceneManagement;

public class PlayerControler : NetworkBehaviour
{
    public Vector3 randomPlayerPosition;

    Animator anim;
    public float speedd = 6f;
    [SerializeField] private float turnSpeed = 5f;
    private Vector3 input;

    public CharacterController controller;
    public InputAction InputAction;

    //yerçekimi deðiþkeni
    Vector3 velocity;
    public float gravity = -9.81f;

    public float acceleration = 5f;   // Hýz artýþý miktarý 
    public float deceleration = 5f;   // Hýz azalýþý miktarý 
    public float maxSpeed = 10f;      // Maksimum hýz   

    private int stationaryFrames;
    private Vector3 movement;
    private float currentSpeed;

    

    private bool isSprint = false;

    public bool LoadScene = false;
    private void Awake()
    {
        // if (!isLocalPlayer) return;
        anim = GetComponent<Animator>();
        randomPlayerPosition = new Vector3(Random.Range(53, 63), 1, Random.Range(75, 78));
        transform.position = randomPlayerPosition;

    }
    private void Start()
    {  
        movement = transform.position;
        stationaryFrames = 0;
DontDestroyOnLoad(gameObject);
        if (!isLocalPlayer) return;
        pickUp.handFull = false;
        


        //   InputAction = new InputAction("Sprint", InputActionType.Button, null);
        InputAction.performed += ctx => sprint();
        InputAction.canceled += ctx => StopSprint();
        InputAction.Enable();


    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!isLocalPlayer) return;

        if (!pickUp.cutting)
        {
            InputRotation();
            Move();
        }
        if (pickUp.handFull == true)
        {
            anim.SetBool("hand", true);
        }
        else
        {
            anim.SetBool("hand", false);
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

        velocity.y += Physics.gravity.y * Time.deltaTime; // Yerçekimi ivmesini uygula

        if (movement.magnitude > 0f)
        {
            // Hýz artýþý
            currentSpeed = Mathf.Min(currentSpeed + acceleration * Time.deltaTime, maxSpeed);

            if (isSprint)
            {
                currentSpeed = Mathf.Max(currentSpeed + acceleration * Time.deltaTime, 7);
                anim.SetBool("isSprint", true);
            }
            // Dönüþ
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            // Hýz azalýþý
            currentSpeed = Mathf.Max(currentSpeed * -2 - deceleration * Time.deltaTime, 0f);

            anim.SetBool("isSprint", false);
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


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Sahnede ne olursa olsun, sahne yüklendiðinde bu kod çalýþýr.
        //Vector3 randomPlayerPosition = new Vector3(Random.Range(53, 63), 1, Random.Range(75, 78));
        transform.position = randomPlayerPosition;
    }
}
