using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order123 : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    [SerializeField] private ScriptableOrder order;

    [SerializeField] private Slider sliderCouldown;


    void Update()
    {
        UpdateGameStatus(123);

    }
    [Server]
    void UpdateGameStatus(float objectNumber)
    {
        // Tüm istemcilere güncel durumu gönder
        RpcinteractID(objectNumber);
    }
    [ClientRpc]
    public void RpcinteractID(float objectNumber)
    {
        if (objectNumber == 123)
        {
            //this.transform.position = new Vector3(150, 522, 0);
            order.orderID = 123;
            OrderObject[1].SetActive(true);
            OrderObject[2].SetActive(true);
            OrderObject[3].SetActive(true);
            OrderObject[4].SetActive(false);
            OrderObject[5].SetActive(false);
            order.couldown -= Time.deltaTime;
            sliderCouldown.value = order.couldown;
            if (order.couldown < 0)
            {
                NetworkServer.Destroy(this.gameObject);
                
            }
        }
    }
}
