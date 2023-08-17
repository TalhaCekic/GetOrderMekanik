using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class RandomOrderGenerator : NetworkBehaviour
{
    [SyncVar]
    public List<int> possibleOrders = new List<int> { 12, 123, 124, 125, 1234, 1245, 1235, 12345 };
    [SyncVar] public int randomOrder;

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
        print(randomOrder);
    }

    private void CalculateNextOrderTime()
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    private void GenerateRandomOrder()
    {
        int randomIndex = Random.Range(0, possibleOrders.Count);
         randomOrder = possibleOrders[randomIndex];
        
    }

    [ClientRpc]
    private void RpcSetPossibleOrders(List<int> orders)
    {
        possibleOrders = orders;
        
    }
}
