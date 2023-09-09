using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order123 : NetworkBehaviour
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
        // transform.parent = canvas.transform;
        orderManager = FindAnyObjectByType<OrderManager>();
        //CmdinteractID(0);
        orderID = 123;
    }
    void Update()
    {
        //CmdinteractID(123);

        UpdateGameStatus(123);


        //if (isServer)
        //{
        //    RpcinteractID(123);
        //}
        //else
        //{
        //    CmdinteractID(123);
        //}
    }
    //[Command(requiresAuthority = false)]
    //public void CmdinteractID(float objectNumber)
    //{
    //    //RpcinteractID(objectNumber);
    //    if (objectNumber == 123)
    //    {
    //        orderID = 123;
    //        print(" ÇALIÞTIRSANA  ");
    //        OrderObject[1].SetActive(true);
    //        OrderObject[2].SetActive(true);
    //        OrderObject[3].SetActive(true);
    //        OrderObject[4].SetActive(false);
    //        OrderObject[5].SetActive(false);
    //        couldown -= Time.deltaTime;
    //        sliderCouldown.value = couldown;
    //        if (couldown < 0)
    //        {
    //            NetworkServer.Destroy(this.gameObject);
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
        if (objectNumber == 123)
        {
            orderID = 123;
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            couldown -= Time.deltaTime;
            sliderCouldown.value = couldown;
            if (couldown < 0)
            {
                NetworkServer.Destroy(this.gameObject);
            }
        }
    }
    public void interactID(float objectNumber)
    {

        if (objectNumber == 123)
        {
            orderID = 123;
            print(" ÇALIÞTIRSANA  ");
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            couldown -= Time.deltaTime;
            sliderCouldown.value = couldown;
            if (couldown < 0)
            {
                NetworkServer.Destroy(this.gameObject);
            }
        }



    }
}
