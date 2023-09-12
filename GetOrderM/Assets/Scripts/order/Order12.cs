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

    [SerializeField] private ScriptableOrder order;
    [SerializeField] private Slider sliderCouldown;

    // private float currentCouldown;
    private OrderTimes orderTimes;
    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        CMDinteract();
    }
    void Update()
    {
        UpdateGameStatus(12);
    }
    [Server]
    void UpdateGameStatus(float objectNumber)
    {
        // T�m istemcilere g�ncel durumu g�nder
        RpcinteractID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        if (objectNumber == 12)
        {

            order.orderID = 12;
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(false);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            orderTimes.currentCouldown -= Time.deltaTime; // Bu �zg�n de�eri azalt
            sliderCouldown.value = orderTimes.currentCouldown;
            if (orderTimes.currentCouldown < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void CMDinteract()
    {
        transform.parent = canvas.transform;
    }


}
