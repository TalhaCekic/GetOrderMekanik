using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.Profiling;

public class OrderManager : NetworkBehaviour
{
    [SyncVar]
    public List<int> possibleOrders = new List<int> { 12, 123, 124, 125, 1234, 1245, 1235, 12345 };
    [SyncVar] public List<int> orderHistory = new List<int>(); // Saklanan sipariþler
    [SyncVar] public int Order;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    private void Start()
    {
        if (isServer)
        {
            RpcSetPossibleOrders(possibleOrders);
        }
        CalculateNextOrderTime();
    }
    private void Update()
    {
        if (isServer && Time.time >= nextOrderTime)
        {
            GenerateRandomOrder();
            CalculateNextOrderTime();
        }
        print(Order);
    }
    private void CalculateNextOrderTime() // tekrardan sipariþin gelme sýklýðý
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }
    public void GenerateRandomOrder() // sayýlarýn random atmasý ardýndan geçmiþe ekler.
    {
        int randomIndex = Random.Range(0, possibleOrders.Count);
        Order = possibleOrders[randomIndex];
        orderHistory.Add(Order); // Yeni sipariþi orderHistory listesine ekle

    }
    //public void RemoveOrderFromHistory()
    //{
    //    if (orderHistory.Count > 0)
    //    {
    //        orderHistory.RemoveAt(0);
    //    }
    //}

    [ClientRpc]
    private void RpcSetPossibleOrders(List<int> orders)
    {
        possibleOrders = orders;
    }
}
