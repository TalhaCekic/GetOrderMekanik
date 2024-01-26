using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Order1245 : NetworkBehaviour
{
    public GameObject canvas;

    [SerializeField] private ScriptableOrder order;

    [SerializeField] private Slider sliderCouldown;

    private OrderTimes orderTimes;
    private bool time = false;

    [SyncVar] public int payOrder; 

    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        orderTimes.orderID = 1245;
        
        //tutarý belirle
        payOrder = GetRandomElement(order.pay);
    }

    void Update()
    {
        if (isServer)
        {
            UpdateGameStatus();
        }
        else
        {
            cmdTransform();
            cmdTime();
        }
    }

    [Command(requiresAuthority = false)]
    public void cmdTime()
    {
        rpcTime();
    }

    [ClientRpc]
    public void rpcTime()
    {
        Times();
    }

    public void Times()
    {
        // orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        OnSliderValueChanged(orderTimes.currentCouldown, sliderCouldown.value);
        order.orderID = 123;

        if (!time && orderTimes.currentCouldown <= 25)
        {
            sliderCouldown.fillRect.GetComponent<Image>().color = Color.red;
            this.transform.DOShakePosition(4, 2, 30, 90);
            time = true;
        }

        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject);
            ManagerOrder.instance.sayac--;
        }
    }

    [Server]
    void UpdateGameStatus()
    {
        orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        sliderCouldown.value = orderTimes.currentCouldown;
        OnSliderValueChanged(orderTimes.currentCouldown, sliderCouldown.value);
        order.orderID = 123;

        if (!time && orderTimes.currentCouldown <= 25)
        {
            sliderCouldown.fillRect.GetComponent<Image>().color = Color.red;
            this.transform.DOShakePosition(4, 2, 30, 90);
            time = true;
        }

        if (orderTimes.currentCouldown < 0)
        {
            NetworkServer.Destroy(this.gameObject);
            ManagerOrder.instance.sayac--;
        }
    }
    
    private T GetRandomElement<T>(T[] array)
    {
        if (array != null && array.Length > 0)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
        else
        {
            return default(T);
        }
    }

    [Command(requiresAuthority = false)]
    public void cmdTransform()
    {
        rpcTransform();
    }

    [ClientRpc]
    public void rpcTransform()
    {
        this.transform.SetParent(canvas.transform);
    }

    private void OnSliderValueChanged(float oldValue, float newValue)
    {
        sliderCouldown.value = newValue;
    }
}