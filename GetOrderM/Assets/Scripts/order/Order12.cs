using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Order12 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    public GameObject canvas;

    [SerializeField] public ScriptableOrder order;
    [SerializeField] private Slider sliderCouldown;

    // private float currentCouldown;
    private OrderTimes orderTimes;
    //   public int id = 12;
    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        orderTimes.orderID = 12;

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
        //  RpcinteractID(12);


        //  transform.parent = canvas.transform;
        order.orderID = 12;
        orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject); ;
        }

    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        if (objectNumber == 12)
        {
            //  transform.parent = canvas.transform;
            order.orderID = 12;
            orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
            sliderCouldown.value = orderTimes.currentCouldown;
            if (orderTimes.currentCouldown < 0)
            {
                NetworkServer.Destroy(this.gameObject); ;
            }
        }
    }
}
