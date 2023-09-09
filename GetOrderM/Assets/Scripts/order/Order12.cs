using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Order12 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    [SerializeField] private OrderManager orderManager;
    [SerializeField] private GameObject canvas;

    [SyncVar] public int orderID = 0;
    public float couldown = 35;

    [SerializeField] private Slider sliderCouldown;

    void Start()
    {
        DontDestroyOnLoad(this);
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        transform.parent = canvas.transform;
        orderManager = FindAnyObjectByType<OrderManager>();
    }
    void Update()
    {
        // CmdinteractID(12);

        // RpcinteractID(12);

        UpdateGameStatus(12);
        //if (isServer)
        //{
        //    RpcinteractID(12);
        //}
        //else
        //{
        //    CmdinteractID(12);
        //}
    }
    //[Command(requiresAuthority = false)]
    //public void CmdinteractID(float objectNumber)
    //{
    //    //RpcinteractID(objectNumber);
    //    if (objectNumber == 12)
    //    {
    //        orderID = 12;
    //        print(" ÇALIÞTIRSANA  ");
    //        OrderObject[1].SetActive(true);
    //        OrderObject[2].SetActive(true);
    //        OrderObject[3].SetActive(false);
    //        OrderObject[4].SetActive(false);
    //        OrderObject[5].SetActive(false);
    //        couldown -= Time.deltaTime;
    //        sliderCouldown.value = couldown;
    //        if (couldown < 0)
    //        {
    //            Destroy(this.gameObject);
    //        }
    //    }
    //}
    [Server]
    void UpdateGameStatus(float objectNumber)
    {

        // Tüm istemcilere güncel durumu gönder
        RpcinteractID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        //CmdinteractID(objectNumber);
        if (objectNumber == 12)
        {
            orderID = 12;
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(false);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            couldown -= Time.deltaTime;
            sliderCouldown.value = couldown;
            if (couldown < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void interactID(float objectNumber)
    {
        if (objectNumber == 12)
        {
            orderID = 12;
            print(" ÇALIÞTIRSANA  ");
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(false);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            couldown -= Time.deltaTime;
            sliderCouldown.value = couldown;
            if (couldown < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    
}
