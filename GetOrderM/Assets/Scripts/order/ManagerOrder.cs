using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;
using System.Collections;

public class ManagerOrder : NetworkBehaviour
{
    public List<GameObject> OrderObject = new List<GameObject>();

    public ScriptableOrder[] orders;

    public Transform parentObject;

    public Transform[] parentTransform;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    [SyncVar] public int Order;

    public int[] orderArray = new int[5];
    public List<GameObject> spawnOrderListesi = new List<GameObject>(); //spawntlanan objelerin listesini tutmak için
    
    private void Start()
    {
        DontDestroyOnLoad(this);
        CalculateNextOrderTime();
    }
    private void Update()
    {
        if (isServer && Time.time >= nextOrderTime)
        {
            GenerateRandomOrder(Order);
            CalculateNextOrderTime();
        }
    }

   
    private void CalculateNextOrderTime() // tekrardan sipariþin gelme sýklýðý
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }
    [Command(requiresAuthority = false)]
    public void GenerateRandomOrder(int Order) 
    {
       
        bool orderAssigned = false;
        int randomIndex = Random.Range(0, orders.Length);
        Order = orders[randomIndex].orderID;
      
        for (int i = 0; i < orderArray.Length; i++)
        {
            if (orderArray[i] == 1)
            {
                orderArray[i] = Order;
                
                orderAssigned = true;
                break;
            }
        }
        if (!orderAssigned)
        {
            orderArray[0] = Order;
        }



        CmdSpawnOrder(parentObject.position, Order);
    }

    [Command(requiresAuthority = false)]
    public void CmdSpawnOrder(Vector3 position, int order)
    {
        GameObject orderPrefab = null;

        if (order == 12)
        {
            orderPrefab = orders[0].orderPrefab;

        }
        else if (order == 123)
        {
            orderPrefab = orders[1].orderPrefab;
           
        }
        else if (order == 1234)
        {
            orderPrefab = orders[2].orderPrefab;
        }
        else if (order == 12345)
        {
            orderPrefab = orders[3].orderPrefab;
        }
        if (orderPrefab != null)
        {
            GameObject spawnedPrefab = Instantiate(orderPrefab, parentObject.position, Quaternion.identity, parentObject);
            NetworkServer.Spawn(spawnedPrefab);

            if (orderArray[0] != 1) spawnedPrefab.gameObject.transform.position = parentTransform[0].transform.position;
            if (orderArray[1] != 1) spawnedPrefab.gameObject.transform.position = parentTransform[1].transform.position;
            if (orderArray[2] != 1) spawnedPrefab.gameObject.transform.position = parentTransform[2].transform.position;
            if (orderArray[3] != 1) spawnedPrefab.gameObject.transform.position = parentTransform[3].transform.position;
            if (orderArray[4] != 1) spawnedPrefab.gameObject.transform.position = parentTransform[4].transform.position;
        }



        // Diðer sipariþ türleri için de kontrolleri ekleyin

    }


}
    



