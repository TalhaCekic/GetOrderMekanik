using UnityEngine;
using System.Collections.Generic;
using Mirror;

public class OrderManager : NetworkBehaviour
{
    [SyncVar]
    public List<int> possibleOrders = new List<int> { 12, 123, 124, 125, 1234, 1245, 1235, 12345 }; // olas� sipari�ler
    [SyncVar] public List<int> orderHistory = new List<int>(); // Saklanan sipari�ler
    public Transform parentObject; // �st obje
    [SyncVar] public int Order;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    public int firstOrder;
    public int secondOrder;
    public int thirdOrder;
    public int fourthOrder;
    public int fifthOrder;

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
        DontDestroyOnLoad(this);
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

        if (firstOrder == 0)
        {
            firstOrder = Order;   
        }
        else if (secondOrder == 0)
        {
            secondOrder = Order;
        }
        else if (thirdOrder == 0)
        {
            firstOrder = Order;
        }
        else if (fourthOrder == 0)
        {
            secondOrder = Order;
        }
        else if (fifthOrder == 0)
        {
            secondOrder = Order;
        }
        CmdSpawnOrder(parentObject.position, Order);
    }
    [Command(requiresAuthority = false)]
    public void CmdSpawnOrder(Vector3 position, int order)
    {
        print("yazd�rsanaa");
        GameObject orderPrefab = null;
        if (order == 12)
        {
            orderPrefab = order12;
        }
        else if (order == 123)
        {
            orderPrefab = order123;
        }
        // Di�er sipari� t�rleri i�in de kontrolleri ekleyin

        if (orderPrefab != null)
        {
            GameObject spawnedPrefab = Instantiate(orderPrefab, parentObject.position, Quaternion.identity, parentObject);
            NetworkServer.Spawn(spawnedPrefab);
        }
    }
    [ClientRpc]
    private void RpcSetPossibleOrders(List<int> orders)
    {
        possibleOrders = orders;
    }
}
