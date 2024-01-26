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

public class TutorialPlayerControler : NetworkBehaviour
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

    private Vector3 movement;
    private float currentSpeed;

    private bool isSprint = false;


    public bool LoadScene = false;

    public TutorialpickUp PickUp;

     private bool isWalking = false;
    private bool isFall = false;
   private float fallCheckInterval = 1.0f;  // Her 1 saniyede bir kontrol etmek için
    private float nextFallCheckTime = 0.0f;
   private float totalAnimationTime = 4.8f; // 1.5 (düþme) + 6.0 (ayaða kalkma) = 7.5 saniye
     private float elapsedTime = 0f;
     private bool animationsStarted = false;
  private float randomValue;


    private string currentAnimation = "";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        PickUp = GetComponent<TutorialpickUp>();
        randomPlayerPosition = new Vector3(Random.Range(-4, 4), 0, Random.Range(-2, 2));
        transform.position = randomPlayerPosition;

    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
       
        movement = transform.position;

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
     
        anim.SetFloat("Speed", 0); // animasyonu kontrol etsin
        if (!PickUp.isWork && !PickUp.isPcInteract && !isFall)
        {
            InputRotation();
            Move();

            // Karakter yürüyorsa ve þu anki zaman, sonraki kontrol zamanýndan büyük veya eþitse
            if (isWalking && Time.time >= nextFallCheckTime)
            {
                CheckForRandomFall("fall");
                nextFallCheckTime = Time.time + fallCheckInterval;  // Sonraki kontrol için zamaný ayarla  
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
        
        randomValue = Random.value;  // 0 ile 1 arasýnda bir deðer döner

        if (randomValue < 0.02f)  // %5 þansa eþit
        {
            isFall = true;
            CmdSetAnimation(animationName);
            animationsStarted = true;
        }
    }
   
    private void CmdSetAnimation(string animationName)
    {

        currentAnimation = animationName;
        OnAnimationChanged(currentAnimation);
    }
  
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
        // Sahnede ne olursa olsun, sahne yüklendiðinde bu kod çalýþýr.
        //Vector3 randomPlayerPosition = new Vector3(Random.Range(53, 63), 1, Random.Range(75, 78));
        transform.position = randomPlayerPosition;
    }
}
