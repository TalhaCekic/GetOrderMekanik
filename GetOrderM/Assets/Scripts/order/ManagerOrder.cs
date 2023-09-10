using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;
public class ManagerOrder : NetworkBehaviour
{
    public ScriptableOrder[] orders;

    public Transform parentObject;

    public Transform[] parentTransform;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    [SyncVar] public int Order;

    public int firstOrder;
    public int secondOrder;
    public int thirdOrder;
    public int fourthOrder;
    public int fifthOrder;

  
    private void Start()
    {
        DontDestroyOnLoad(this);
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
    private void CalculateNextOrderTime() // tekrardan sipariþin gelme sýklýðý
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }
    public void GenerateRandomOrder() // sayýlarýn random atmasý ardýndan geçmiþe ekler.
    {
        int randomIndex = Random.Range(0, orders.Length);
        Order = orders[randomIndex].orderID;


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
            thirdOrder = Order;
        }

        else if (fourthOrder == 0)
        {
            fourthOrder = Order;
        }

        else if (fifthOrder == 0)
        {
            fifthOrder = Order;
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
            orderPrefab=orders[2].orderPrefab;
        }
        else if (order == 12345)
        {
            orderPrefab = orders[3].orderPrefab;
        }
        if (orderPrefab != null)
            {
                GameObject spawnedPrefab = Instantiate(orderPrefab, parentObject.position, Quaternion.identity, parentObject);
                NetworkServer.Spawn(spawnedPrefab);
                if (firstOrder != 0) spawnedPrefab.gameObject.transform.position = parentTransform[0].transform.position;
                if (secondOrder != 0) spawnedPrefab.gameObject.transform.position = parentTransform[1].transform.position;
                if (thirdOrder != 0) spawnedPrefab.gameObject.transform.position = parentTransform[2].transform.position;
                if (fourthOrder != 0) spawnedPrefab.gameObject.transform.position = parentTransform[3].transform.position;
                if (fifthOrder != 0) spawnedPrefab.gameObject.transform.position = parentTransform[4].transform.position;
            }
        


        // Diðer sipariþ türleri için de kontrolleri ekleyin

    }

}
