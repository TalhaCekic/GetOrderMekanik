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

    public Canvas canvas;

    private float minInterval = 10f;
    private float maxInterval = 20f;
    private float nextOrderTime = 0f;

    [SyncVar] float resetDelay = 0.5f;
    [SyncVar] public float lastResetTime = -1f;

    [SyncVar] public int Order;
    public SyncList<int> orderArray = new SyncList<int>();

    [SerializeField] public SyncList<GameObject> orderUI = new SyncList<GameObject>();
    [SerializeField] private DeliveryOrder deliveryOrder;
    Order12 order12Component;
    private void Awake()
    {
        canvas = GetComponent<Canvas>();
        deliveryOrder = FindAnyObjectByType<DeliveryOrder>();
      
    }
    private void Start()
    {
        //DontDestroyOnLoad(this);
        CalculateNextOrderTime();
        //if (isServer)
        //{
        //    for (int i = 0; i < 5; i++)
        //    {
        //        orderArray.Add(1);
        //    }
        //}
    }
    private void Update()
    {
        if (isServer && Time.time >= nextOrderTime)
        {
            GenerateRandomOrder();
            CalculateNextOrderTime();
            ServerSpawnOrder(parentObject.position, Order);
        }
        if (isServer)
        {
            server(deliveryOrder.currentobjectnumber);
        }
    }
    [Server]
    private void CalculateNextOrderTime() // tekrardan sipariþin gelme sýklýðý
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
       
    }
    [Server]
    public void GenerateRandomOrder()
    {
        // bool orderAssigned = false;
        int randomIndex = Random.Range(0, orders.Length);
        Order = orders[randomIndex].orderID;
        //for (int i = 0; i < orderArray.Count; i++)
        //{
        //    if (orderArray[i] == 1)
        //    {
        //        orderArray[i] = orderUI[i].GetComponent<OrderTimes>().orderID;
        //        break;
        //    }
        //}
       
    }
    [Command(requiresAuthority = false)]
    public void CmdSpawnOrder(Vector3 position, int order)
    {
        ServerSpawnOrder(position, order);
    }
    [Server]
    public void ServerSpawnOrder(Vector3 position, int order)
    {
        GameObject orderPrefab = null;
        int orderID=0;

        if (order == 12)
        {
            orderPrefab = orders[0].orderPrefab;
            orderID = orders[0].orderID;  
        }
        else if (order == 123)
        {
            orderPrefab = orders[1].orderPrefab;
            orderID = orders[1].orderID;
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
            AddObjectToList(spawnedPrefab,orderID);
          
            //if (orderArray[0] != 1)
            //{
            //    spawnedPrefab.gameObject.transform.position = parentTransform[0].transform.position;
            //    //spawnedPrefab.transform.parent = canvas.transform.parent;

            //}
            //if (orderArray[1] != 1)
            //{
            //    spawnedPrefab.gameObject.transform.position = parentTransform[1].transform.position;
            //    // spawnedPrefab.transform.parent = canvas.transform.parent;
            //    AddObjectToList(spawnedPrefab);
            //}
            //if (orderArray[2] != 1)
            //{
            //    spawnedPrefab.gameObject.transform.position = parentTransform[2].transform.position;
            //    // spawnedPrefab.transform.parent = canvas.transform.parent;
            //    AddObjectToList(spawnedPrefab);
            //}
            //if (orderArray[3] != 1)
            //{
            //    spawnedPrefab.gameObject.transform.position = parentTransform[3].transform.position;
            //    // spawnedPrefab.transform.parent = canvas.transform.parent;
            //    AddObjectToList(spawnedPrefab);
            //}
            //if (orderArray[4] != 1)
            //{
            //    spawnedPrefab.gameObject.transform.position = parentTransform[4].transform.position;
            //    //  spawnedPrefab.transform.parent = canvas.transform.parent;
            //    AddObjectToList(spawnedPrefab);
            //}
        }
        // Diðer sipariþ türleri için de kontrolleri ekleyin
    }

    [Server]
    public void server(int currentobjectnumber)
    {

        if (Time.time - lastResetTime > resetDelay)
        {
            for (int i = 0; i < orderArray.Count; i++)
            {

                //int intValue = orderUI[i].GetComponent<Order12>().id;
                //Debug.Log(" Int Deðer: " + intValue);
                //int intValue2 = orderUI[i].GetComponent<Order123>().id;

                //Debug.Log(" Int Deðer: " + intValue2);

                if (deliveryOrder.submidID == orderArray[i])
                {
                    //   Debug.Log("GameObject: " + uiObject.name + ", Int Deðer: " + intValue2);
                    //  currentobjectnumber = 0;
                    orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;
                    orderUI.Remove(orderUI[i]);
                    deliveryOrder.lastResetTime = Time.time;
                    deliveryOrder.orderCorrect = true;
                    orderArray.Remove(orderArray[i]);
                    break;
                }
                else
                {
                    deliveryOrder.orderCorrect = false;
                }


            }
        }

       

     //   deliveryOrder.currentID = 1;
    }
    [Server]
    public void AddObjectToList(GameObject obj, int id)
    {
        if (!orderUI.Contains(obj))
        {
            orderUI.Add(obj);
            
            orderArray.Add(id);
        }
    }
}




