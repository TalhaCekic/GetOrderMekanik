using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.Profiling;

public class OrderManager : NetworkBehaviour
{
    [SyncVar]
    public List<int> possibleOrders = new List<int> { 12, 123, 124, 125, 1234, 1245, 1235, 12345 };
    [SyncVar] public List<int> orderHistory = new List<int>(); // Saklanan sipariþler
    public Transform parentObject; // Üst obje
    [SyncVar] public int Order;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    public int firstOrder;
    public int secondOrder;

    public GameObject order12;
    public GameObject order123;
    //public GameObject order124;
    //public GameObject order125;
    //public GameObject order1234;
    //public GameObject order1245;
    //public GameObject order1235;
    //public GameObject order12345;

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
            SpawnOrder(parentObject.position);
            GenerateRandomOrder();
            CalculateNextOrderTime();
            RemoveOrderFromHistory();
            
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
    //[Command(requiresAuthority = false)] public void CmdSpawnOrder()
    //{
    //    SpawnOrder();
    //}
    [Command(requiresAuthority = false)]
    public void SpawnOrder(Vector3 position)
    {
        print("  spawnlarmýsýn  ");
        if(Order == 12)
        {
           // GameObject spawnedPrefab = Instantiate(order12, parentObject.position, Quaternion.identity);
            Instantiate(order12, parentObject);
            NetworkServer.Spawn(order12);
        }
        if(Order == 123)
        {
            //GameObject spawnedPrefab = Instantiate(order123, parentObject.position, Quaternion.identity);
            Instantiate(order123, parentObject);
            NetworkServer.Spawn(order123);
        }
    }
    public void RemoveOrderFromHistory()
    {
        if (orderHistory.Count > 0)
        {
            //orderHistory.RemoveAt(0);
             firstOrder = orderHistory[0];
            secondOrder = orderHistory[1];
        }
    }

    [ClientRpc]
    private void RpcSetPossibleOrders(List<int> orders)
    {
        possibleOrders = orders;
    }
}
