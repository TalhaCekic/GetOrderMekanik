using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Order12 : NetworkBehaviour
{
    public GameObject canvas;

    [SerializeField] public ScriptableOrder order;
    [SerializeField] private Slider sliderCouldown;

    private OrderTimes orderTimes;

    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        orderTimes.orderID = 12;
       // this.transform.parent = canvas.transform;
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
        
        order.orderID = 12;
        orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject);
            ManagerOrder.instance.sayac--;
        }

    }
}
