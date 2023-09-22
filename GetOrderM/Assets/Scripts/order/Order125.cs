using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order125 : NetworkBehaviour
{
    public GameObject canvas;

    [SerializeField] private ScriptableOrder order;

    [SerializeField] private Slider sliderCouldown;

    private OrderTimes orderTimes;
    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        orderTimes.orderID = 125;

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
        
        order.orderID = 125;
        orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject);
            ManagerOrder.instance.sayac--;

        }
    }
}
