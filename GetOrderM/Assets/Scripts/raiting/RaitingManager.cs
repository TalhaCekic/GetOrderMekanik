using System;
using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.UI;

public class RaitingManager : NetworkBehaviour
{
    public ScriptableRaiting scriptableRaiting;
    public static RaitingManager instance;

    [SyncVar] public string randomCustomerName;
    [SyncVar] public string randomGoodComment;

    [SyncVar] public bool trueDelivery;
    [SyncVar] public bool falseDelivery;
    [SyncVar] public bool justTrueDelivery;
    [SyncVar] public int trueOrFalseDelivery; // 0 ise doğru 1 ise yanlış ifadesidir

    [SyncVar] public bool isSpawn;
    [SyncVar] public int starValue;

    public GameObject[] players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        trueDelivery = false;
        falseDelivery = false;
        isSpawn = false;
        instance = this;
    }

    void Update()
    {
        if (isServer)
        {
            raitingManager();
        }
    }

    [Command(requiresAuthority = false)]
    public void raitingManager()
    {
        if (scriptableRaiting != null && trueDelivery)
        {
            RpcRaitingManager();

            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    raitingSpawner RaitingSpawner = player.GetComponent<raitingSpawner>();
                    if (RaitingSpawner != null)
                    {
                        RaitingSpawner.isSpawned = true;
                    }
                }
            }
        }  
        if (scriptableRaiting != null && falseDelivery)
        {
            RpcRaitingManager();
        
            // her karakter spawnı başlatır
            foreach (GameObject player in players)
            {
                if (player != null)
                {
                    raitingSpawner RaitingSpawner = player.GetComponent<raitingSpawner>();
                    if (RaitingSpawner != null)
                    {
                        RaitingSpawner.isSpawned = true;
                    }
                }
            }
        }
    }

    [ClientRpc]
    public void RpcRaitingManager()
    {
        if (!justTrueDelivery)
        {
            if (trueDelivery)
            {
                // İSİM belirleme
                randomCustomerName = GetRandomElement(scriptableRaiting.CustomerName);
                //star value
                starValue = Random.Range(0, 5);

                // yorumu belirleme
                if (starValue >= 3)
                {
                    randomGoodComment = GetRandomElement(scriptableRaiting.CustomerCommentGood);
                }
                else
                {
                    randomGoodComment = GetRandomElement(scriptableRaiting.CustomerCommentBad);
                }
                trueDelivery = false;
                trueOrFalseDelivery = 0;
                
            }
        }
        else
        {
            randomCustomerName = GetRandomElement(scriptableRaiting.CustomerName);

            randomGoodComment = GetRandomElement(scriptableRaiting.CustomerCommentJustGood);

            //star value
            starValue = Random.Range(4, 5);

            justTrueDelivery = false;
            trueOrFalseDelivery = 0;
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

    [Command(requiresAuthority = false)]
    public void cmdTrueDelivery(bool value)
    {
        trueDelivery = value;
        if (DeliveryOrder.instance.isControlledCounter)
        {
            justTrueDelivery = true;
        }
    }

    [Command(requiresAuthority = false)]
    public void cmdFalseDelivery(bool value)
    {
        falseDelivery = value;
    }

    [Command(requiresAuthority = false)]
    public void cmdIsSpawn(bool value)
    {
        isSpawn = value;
    }
}