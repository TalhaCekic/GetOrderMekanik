using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Telepathy;

public class PlayerControler : NetworkBehaviour
{
    public Vector3 randomPlayerPosition;

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

    private Vector3 movement;
    private float currentSpeed;

    private bool isSprint = false;


    public bool LoadScene = false;

   // public GameObject hud;

    public pickUp PickUp;

    [SyncVar] private bool isWalking = false;
    [SyncVar] private bool isFall = false;
    [SyncVar] private bool animationCompleted = false;
    [SyncVar] private float fallCheckInterval = 1.0f;  // Her 1 saniyede bir kontrol etmek i�in
    [SyncVar] private float nextFallCheckTime = 0.0f;
   [SyncVar] private float totalAnimationTime = 4.8f; // 1.5 (d��me) + 6.0 (aya�a kalkma) = 7.5 saniye
    [SyncVar] private float elapsedTime = 0f;
    [SyncVar] private bool animationsStarted = false;
    [SyncVar] private float randomValue;

    [SyncVar]
    private string currentAnimation = "";

    private void Awake()
    {
        // if (!isLocalPlayer) return;
        anim = GetComponent<Animator>();
        PickUp = GetComponent<pickUp>();
        randomPlayerPosition = new Vector3(Random.Range(-4, 4), 0, Random.Range(-2, 2));
        transform.position = randomPlayerPosition;

    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //hud.SetActive(false);
        if (!isLocalPlayer) return;
        movement = transform.position;

        //   InputAction = new InputAction("Sprint", InputActionType.Button, null);
        InputAction.performed += ctx => sprint();
        InputAction.canceled += ctx => StopSprint();
        InputAction.Enable();
        //hud.SetActive(true);

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
        anim.SetFloat("Speed", 0); // animasyonu kontrol etsin
        if (!PickUp.isWork && isFall == false)
        {
            InputRotation();
            Move();

            // Karakter y�r�yorsa ve �u anki zaman, sonraki kontrol zaman�ndan b�y�k veya e�itse
            if (isWalking && Time.time >= nextFallCheckTime)
            {
                CheckForRandomFall("fall");
                nextFallCheckTime = Time.time + fallCheckInterval;  // Sonraki kontrol i�in zaman� ayarla  
            }
        }
        if (animationsStarted == true)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= totalAnimationTime)
            {
                isFall = false;
                elapsedTime = 0;  
                animationsStarted = false;
            }
        }
    }
    public void CheckForRandomFall(string animationName)
    {
        if (!isLocalPlayer) return;
        randomValue = Random.value;  // 0 ile 1 aras�nda bir de�er d�ner

        if (randomValue < 0.005f)  // %5 �ansa e�it
        {
            isFall = true;
            CmdSetAnimation(animationName);
            animationsStarted = true;
        }
    }
    [Command]
    private void CmdSetAnimation(string animationName)
    {
        
        currentAnimation = animationName;
        OnAnimationChanged(currentAnimation);
    }
    [ClientRpc]
    private void OnAnimationChanged(string newAnimation)
    {
        anim.SetTrigger(newAnimation);
       
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
                anim.SetBool("isSprint", true);
            }
            // D�n��
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            // H�z azal���
            currentSpeed = Mathf.Max(currentSpeed * -2 - deceleration * Time.deltaTime, 0f);

            anim.SetBool("isSprint", false);
        }
        // Hareket etme
        controller.Move(transform.forward * currentSpeed * Time.deltaTime);
        anim.SetFloat("Speed", currentSpeed / speedd);
       // isWalking = true;
        if (movement.magnitude <= 0f)
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

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
        // Sahnede ne olursa olsun, sahne y�klendi�inde bu kod �al���r.
        //Vector3 randomPlayerPosition = new Vector3(Random.Range(53, 63), 1, Random.Range(75, 78));
        transform.position = randomPlayerPosition;
    }
}
