using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order125 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    public GameObject canvas;

    [SerializeField] private ScriptableOrder order;

    [SerializeField] private Slider sliderCouldown;

    private OrderTimes orderTimes;

    //private float currentCouldown;
    public int id = 125;
    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        orderTimes.orderID = 125;
    }

    void Update()
    {
        if (isServer)
        {
            UpdateGameStatus();
        }

    }
    [Server]
    void UpdateGameStatus()
    {
        // Tüm istemcilere güncel durumu gönder
        // RpcinteractID(123);

        //  transform.parent = canvas.transform;
        //this.transform.position = new Vector3(150, 522, 0);
        order.orderID = 123;
        orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject);

        }

    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        if (objectNumber == 123)
        {
            //  transform.parent = canvas.transform;
            //this.transform.position = new Vector3(150, 522, 0);
            order.orderID = 123;
            orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
            sliderCouldown.value = orderTimes.currentCouldown;
            if (orderTimes.currentCouldown < 0)
            {
                NetworkServer.Destroy(this.gameObject);
                ManagerOrder.instance.sayac--;
            }
        }
    }
}
