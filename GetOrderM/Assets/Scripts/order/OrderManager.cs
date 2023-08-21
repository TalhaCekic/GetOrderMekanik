using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.Profiling;

public class OrderManager : NetworkBehaviour
{
    [SyncVar]
    public List<int> possibleOrders = new List<int> { 12, 123, 124, 125, 1234, 1245, 1235, 12345 };
    [SyncVar] public List<int> orderHistory = new List<int>(); // Saklanan sipari�ler
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
    private void CalculateNextOrderTime() // tekrardan sipari�in gelme s�kl���
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }
    public void GenerateRandomOrder() // say�lar�n random atmas� ard�ndan ge�mi�e ekler.
    {
        int randomIndex = Random.Range(0, possibleOrders.Count);
        Order = possibleOrders[randomIndex];
        orderHistory.Add(Order); // Yeni sipari�i orderHistory listesine ekle

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
