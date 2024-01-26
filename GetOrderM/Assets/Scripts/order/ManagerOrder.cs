using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Mirror;
using TMPro;
using UnityEngine.SceneManagement;

public class ManagerOrder : NetworkBehaviour
{
    public static ManagerOrder instance;
    public List<GameObject> OrderObject = new List<GameObject>();

    public ScriptableOrder[] orders;

    public Transform parentObject;

    public Transform[] parentTransform;

    private float minInterval = 10f; //10
    private float maxInterval = 15f; //20
    public float nextOrderTime = 0f;

    [SyncVar] float resetDelay = 0.5f;
    [SyncVar] public float lastResetTime = -1f;

    [SyncVar] public int Order;
    public SyncList<int> orderHistory = new SyncList<int>();
    public SyncList<int> orderArray = new SyncList<int>();

    [SerializeField] public SyncList<GameObject> orderUI = new SyncList<GameObject>();
    [SerializeField] private DeliveryOrder deliveryOrder;

    [SyncVar] public int sayac = 0;

    
    // para sistemi
    public TMP_Text payText;
    [SyncVar] public int payAmount;
    // gelen paranın kayma işlemleri için target sistemi
    [SyncVar] public bool isPayTrue;
    [SyncVar] public bool isPayFalse;
    public TMP_Text truePayText;
    public TMP_Text falsePayText;
    public GameObject target1; 
    public GameObject targetBase; 
    public GameObject target2; 

    private void Awake()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "PcLobby")
        {
            parentObject = GetComponent<Transform>();
            deliveryOrder = FindAnyObjectByType<DeliveryOrder>();
        }
    }

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        instance = this;
        if (isServer && currentScene.name != "PcLobby" && DayManager.instance.dayOn)
        {
            CalculateNextOrderTime();
        }
    }

    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (isServer && currentScene.name != "PcLobby" && Time.time >= nextOrderTime && orderUI.Count <= 4 &&
            DayManager.instance.dayOn && orderHistory.Count != DayManager.instance.orderDay)
        {
            firsRandomOrder();
            GenerateRandomOrder();
            CalculateNextOrderTime();
            ServerSpawnOrder(parentObject.position, Order);
            rpcPayText();
        }

        if (isServer && currentScene.name != "PcLobby")
        {
            cmdServer(deliveryOrder.currentobjectnumber);
            positionTransform();
        }

        if (isServer && !DayManager.instance.dayOn)
        {
            dayReflesh();
        }
    }

    [Server]
    private void CalculateNextOrderTime() // tekrardan siparişin gelme sıklığı
    {
        if (!DayManager.instance.dayOn)
        {
            return;
        }

        nextOrderTime = Time.time + Random.Range(minInterval, maxInterval);
    }

    [Command(requiresAuthority = false)]
    public void firsRandomOrder()
    {
        if (orderArray.Count == 0)
        {
            int randomIndex = Random.Range(0, orders.Length);
            int newOrder = orders[randomIndex].orderID;
            if (!orderArray.Contains(newOrder))
            {
                Order = newOrder;
            }
        }
    }

    [Server]
    public void GenerateRandomOrder()
    {
        if (sayac < 4)
        {
            int randomIndex = Random.Range(0, orders.Length);
            int newOrder = orders[randomIndex].orderID;

            for (int i = 0; i < orderArray.Count; i++)
            {
                if (orderArray[i] != newOrder)
                {
                    print("eşitlik var girme");
                    if (!orderArray.Contains(newOrder))
                    {
                        Order = newOrder;
                    }
                }

                if (orderArray[i] == newOrder)
                {
                    print("eşitlik yok gir");
                    if (!orderArray.Contains(newOrder))
                    {
                        Order = 0;
                    }
                }
            }
        }
        else
        {
            Order = 0;
        }
    }

    [Command(requiresAuthority = false)]
    public void ServerSpawnOrder(Vector3 position, int order)
    {
        GameObject orderPrefab = null;
        int orderID = 0;

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
        else if (order == 124)
        {
            orderPrefab = orders[2].orderPrefab;
            orderID = orders[2].orderID;
        }
        else if (order == 125)
        {
            orderPrefab = orders[3].orderPrefab;
            orderID = orders[3].orderID;
        }
        else if (order == 1234)
        {
            orderPrefab = orders[4].orderPrefab;
            orderID = orders[4].orderID;
        }
        else if (order == 1235)
        {
            orderPrefab = orders[5].orderPrefab;
            orderID = orders[5].orderID;
        }
        else if (order == 1245)
        {
            orderPrefab = orders[6].orderPrefab;
            orderID = orders[6].orderID;
        }
        else if (order == 12345)
        {
            orderPrefab = orders[7].orderPrefab;
            orderID = orders[7].orderID;
        }

        if (orderPrefab != null)
        {
            GameObject spawnedPrefab =
                Instantiate(orderPrefab, parentObject.position, Quaternion.identity, parentObject);

            NetworkServer.Spawn(spawnedPrefab);

            AddObjectToList(spawnedPrefab, orderID);
        }
    }

    [Command(requiresAuthority = false)]
    public void cmdServer(int currentobjectnumber)
    {
        server(currentobjectnumber);
    }

    [Command(requiresAuthority = false)]
    public void server(int currentobjectnumber)
    {
        //hatalı !!!!!!!!!!!!!!!
        if (Time.time - lastResetTime > resetDelay)
        {
            for (int i = 0; i < orderArray.Count; i++)
            {
              //  rpcPayText();
                if (deliveryOrder.submidID==orderArray[i] && deliveryOrder.submidID !=0 )
                {
                    isPayTrue = true;
                    rpcTruePayAmount(payAmount);

                    deliveryOrder.CmdorderCorrect(true);
                    deliveryOrder.CmdnotOrderCorrect(false);
                    orderUI[i].GetComponent<OrderTimes>().currentCouldown = 0;

                    orderArray.Remove(orderArray[i]);
                    orderUI.Remove(orderUI[i]);
                    sayac--;
                    deliveryOrder.lastResetTime = Time.time;
                    RaitingManager.instance.cmdTrueDelivery(true);
                    RaitingManager.instance.cmdFalseDelivery(false);
                    break;
                }
                else if(deliveryOrder.submidID != orderArray[i] && deliveryOrder.submidID !=0 && !isPayTrue && !isPayFalse)
                {
                    if (!isPayFalse)
                    {
                        rpcFalsePayAmount(payAmount);
                        isPayFalse = true;
                    }
                }

                if (orderUI[i] == null)
                {
                    orderUI.Remove(orderUI[i]);
                    orderArray.Remove(orderArray[i]);
                }
            }
        }
    }

    [ClientRpc]
    public void rpcTruePayAmount(int a)
    {
        foreach (ScriptableOrder order in orders)
        {
            if (order.orderID == deliveryOrder.submidID)
            {
                print("doğru sipariş");
                isPayTrue = true;
                payAmount += GetRandomElement(order.pay);
                payText.text = payAmount.ToString();
                truePayText.text = "+" + GetRandomElement(order.pay).ToString();
                return;
            }
        }
    }   
    [ClientRpc]
    public void rpcFalsePayAmount(int a)
    {
        foreach (ScriptableOrder order in orders)
        {
            if (order.orderID != deliveryOrder.submidID)
            {
                print("yanlış sipariş");
                payAmount -= GetRandomElement(order.pay);
                payText.text = payAmount.ToString();
                falsePayText.text = "-"+GetRandomElement(order.pay).ToString();
                isPayFalse = true;
                return;
            }
        }
    }

    [Command(requiresAuthority = false)]
    public void rpcPayText()
    {
        foreach (ScriptableOrder order in orders)
        {
            if (isPayTrue)
            {
                print("pay ekle");
            
                truePayText.gameObject.SetActive(true);
                truePayText.gameObject.transform.DOMove(target1.transform.position, 2).OnComplete(() =>
                {
                    truePayText.gameObject.SetActive(false);
                    truePayText.gameObject.transform.DOMove(targetBase.transform.position, 1);
                    isPayTrue = false;
                });
            }
            if(!isPayTrue && isPayFalse)
            {
                print("pay çıkart");
                falsePayText.gameObject.SetActive(true);
            
                falsePayText.gameObject.transform.DOMove(target2.transform.position, 2).OnComplete(() =>
                {
                    falsePayText.gameObject.SetActive(false);
                    falsePayText.gameObject.transform.DOMove(targetBase.transform.position, 1);
                   // isPayFalse = false;
                });
            }
        }
    }

    private T GetRandomElement<T>(T[] array)
    {
        if (array != null && array.Length > 0)
        {
            int randomIndex = Random.Range(0, array.Length);
            return array[randomIndex];
        }
        else
        {
            return default(T);
        }
    }

    [Server]
    public void AddObjectToList(GameObject obj, int id)
    {
        if (!orderUI.Contains(obj))
        {
            orderUI.Add(obj);
            orderArray.Add(id);
            orderHistory.Add(id);
            sayac++;
        }
    }

    [Command(requiresAuthority = false)]
    public void positionTransform()
    {
        for (int i = 0; i < orderUI.Count; i++)
        {
            orderUI[i].gameObject.transform.position = parentTransform[i].transform.position;
        }
    }

    [Command(requiresAuthority = false)]
    public void dayReflesh()
    {
        for (int i = 0; i < orderHistory.Count; i++)
        {
            orderHistory.Remove(orderHistory[i]);
        }
    }
}