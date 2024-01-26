using Mirror;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Order123 : NetworkBehaviour
{
    public GameObject canvas;

    [SerializeField] private ScriptableOrder order;

    [SerializeField] private Slider sliderCouldown;

    private OrderTimes orderTimes;
    private bool time = false;

    private void Start()
    {
        orderTimes = GetComponent<OrderTimes>();
        orderTimes.currentCouldown = order.couldown;
        canvas = GameObject.FindGameObjectWithTag("Canvas");

        orderTimes.orderID = 123;
    }

    void Update()
    {
        if (isServer)
        {
            UpdateGameStatus();
            //rpcTransform();
            //rpcTime();
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
        Tiems();
    }

    [ClientRpc]
    public void rpcTime()
    {
        Tiems();
    }

    public void Tiems()
    {
        // orderTimes.currentCouldown -= Time.deltaTime; // Bu özgün deðeri azalt
        // sliderCouldown.value = orderTimes.currentCouldown;
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

    //transform ayarý
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

    // slider deðeri
    private void OnSliderValueChanged(float oldValue, float newValue)
    {
        sliderCouldown.value = newValue;
    }
}