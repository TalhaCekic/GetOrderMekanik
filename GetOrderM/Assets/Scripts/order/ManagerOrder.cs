using UnityEngine;
using System.Collections.Generic;
using Mirror;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
public class ManagerOrder : NetworkBehaviour
{
    public static ManagerOrder instance;
    public List<GameObject> OrderObject = new List<GameObject>();

    public ScriptableOrder[] orders;

   [SyncVar] public Transform parentObject;

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

    [SyncVar] public int sayac=0;
    private void Awake()
    {
        parentObject = GetComponent<Transform>();
        canvas = GetComponent<Canvas>();
        deliveryOrder = FindAnyObjectByType<DeliveryOrder>();

    }
    private void Start()
    {
        instance = this;
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
        if (isServer && Time.time >= nextOrderTime )
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
    private void CalculateNextOrderTime() // tekrardan siparişin gelme sıklığı
    {
        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);

    }
    [Server]
    public void GenerateRandomOrder()
    {
        if (sayac < 3)
        {
            int randomIndex = Random.Range(0, orders.Length);
            int newOrder = orders[randomIndex].orderID;


           if (!orderArray.Contains(newOrder))
            {
                Order = newOrder;
            }
           
        }
        else
        {
            Order = 0;
        }

    }
 



    //private bool IsOrderIDInArray(int orderID)
    //{
    //    foreach (int existingOrderID in orderArray)
    //    {
    //        if (existingOrderID == orderID)
    //        {
    //            return true; // Sipariş ID zaten orderArray içinde bulunuyor.
    //        }
    //    }

    //    return false; // Sipariş ID orderArray içinde bulunmuyor.
    //}

    // [Server]
    //public void GenerateRandomOrder()
    //{

    //    int randomIndex = Random.Range(0, orders.Length);
    //    Order = orders[randomIndex].orderID;



    //       // Order = order;



    //}

    [Command(requiresAuthority = false)]
    public void CmdSpawnOrder(Vector3 position, int order)
    {
        ServerSpawnOrder(position, order);

    }
    [Server]
    public void ServerSpawnOrder(Vector3 position, int order)
    {
       
      
            GameObject orderPrefab = null;
            int orderID = 0;

            if (order == 12 )
            {
                orderPrefab = orders[0].orderPrefab;
                orderID = orders[0].orderID;
            }
            else if (order == 123 )
            {
                orderPrefab = orders[1].orderPrefab;
                orderID = orders[1].orderID;
            }
            else if (order == 124 )
            {
                orderPrefab = orders[2].orderPrefab;
                orderID = orders[2].orderID;
            }
            else if (order == 125 )
            {
                orderPrefab = orders[3].orderPrefab;
                orderID = orders[3].orderID;
            }
            else if (order == 1234 )
            {
                orderPrefab = orders[4].orderPrefab;
                orderID = orders[4].orderID;
            }
            else if (order == 1235 )
            {
                orderPrefab = orders[5].orderPrefab;
                orderID = orders[5].orderID;
            }
            else if (order == 1245 )
            {
                orderPrefab = orders[6].orderPrefab;
                orderID = orders[6].orderID;
            }
            else if (order == 12345 )
            {
                orderPrefab = orders[7].orderPrefab;
                orderID = orders[7].orderID;
        }
        if (orderPrefab != null)
        {
            GameObject spawnedPrefab = Instantiate(orderPrefab, parentObject.position, Quaternion.identity, canvas.transform);
            
            NetworkServer.Spawn(spawnedPrefab);
            
            AddObjectToList(spawnedPrefab, orderID);

           // if (orderUI[0] != null)
           // {
           //     spawnedPrefab.gameObject.transform.position = parentTransform[0].transform.position;
           //     //spawnedPrefab.transform.parent = canvas.transform.parent;

           // }
           // if (orderUI[1] != null)
           // {
           //     spawnedPrefab.gameObject.transform.position = parentTransform[1].transform.position;
           //     // spawnedPrefab.transform.parent = canvas.transform.parent;

           // }
           //  if (orderUI[2] != null)
           // {
           //     spawnedPrefab.gameObject.transform.position = parentTransform[2].transform.position;
           //     // spawnedPrefab.transform.parent = canvas.transform.parent;

           // }
           //  if (orderUI[3] != null)
           // {
           //     spawnedPrefab.gameObject.transform.position = parentTransform[3].transform.position;
           //     // spawnedPrefab.transform.parent = canvas.transform.parent;

           // }
           //if (orderUI[4] != null)
           // {
           //     spawnedPrefab.gameObject.transform.position = parentTransform[4].transform.position;
           //     //  spawnedPrefab.transform.parent = canvas.transform.parent;

           // }
        }




    }

    [Server]
    public void server(int currentobjectnumber)
    {
        if (Time.time - lastResetTime > resetDelay)
        {
            for (int i = 0; i < orderArray.Count; i++)
            {
                if (orderArray[i] == deliveryOrder.submidID)
                {
                    deliveryOrder.orderCorrect = true;
                    orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;

                    orderArray.Remove(orderArray[i]);
                    orderUI.Remove(orderUI[i]);
                    sayac--;
                    deliveryOrder.lastResetTime = Time.time;

                    break;
                }
                if (orderUI[i] == null)
                {
                    orderUI.Remove(orderUI[i]);
                    orderArray.Remove(orderArray[i]);
                }
                else
                {
                    deliveryOrder.orderCorrect = false;
                }
            }
        }
    }
    [Server]
    public void AddObjectToList(GameObject obj, int id)
    {
        if (!orderUI.Contains(obj))
        {
            orderUI.Add(obj);
            orderArray.Add(id);
            sayac++;
            
        }
        for (int i = 0; i < orderUI.Count; i++)
        {
            //orderUI[i].gameObject.transform.position = parentTransform[i].transform.position;
            orderUI[i].GetComponent<Transform>().parent = canvas.transform;
            orderUI[i].GetComponent<Transform>().position = parentTransform[i].transform.position;


        }
    }
}




