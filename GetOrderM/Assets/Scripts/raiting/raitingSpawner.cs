using System;
using System.Collections.Generic;
using Mirror;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class raitingSpawner : NetworkBehaviour
{
    public static raitingSpawner instance;
    public ScriptableRaiting scriptableRaiting;
    public Transform parentTransform;

    public TMP_Text nameText;
    public TMP_Text commantText;
    public TMP_Text raitingText;

    public List<GameObject> raitingObjects = new List<GameObject>();

    public bool isSpawned;

    private void Start()
    {
        instance = this;
        isSpawned = false;
    }

    void Update()
    {
        if (isServer)
        {
            spawn();
        }

        float averageRaiting = CalculateAverageRaiting(raitingObjects);
        raitingText.text = averageRaiting.ToString() + "/5";
    }

    [Command(requiresAuthority = false)]
    public void spawn()
    {
        if (isSpawned)
        {
            GameObject newRaiting = Instantiate(scriptableRaiting.raitingPrefab, parentTransform.position,
                Quaternion.identity, parentTransform);
            NetworkServer.Spawn(newRaiting);
            rpcSpawn(newRaiting);

            isSpawned = false;
        }
    }

    [ClientRpc]
    public void rpcSpawn(GameObject obj)
    {
        if (obj != null)
        {
            obj.transform.SetParent(parentTransform);
            raitingObjects.Add(obj);
        }
    }

    float CalculateAverageRaiting(List<GameObject> objectsList)
    {
        if (objectsList == null || objectsList.Count == 0)
        {
            return 0f;
        }

        int sum = 0;
        int count = 0;

        foreach (GameObject obj in objectsList)
        {
            RaitingCustomer[] raitingCustomers = obj.GetComponentsInChildren<RaitingCustomer>();
            
            foreach (RaitingCustomer raitingCustomer in raitingCustomers)
            {
                sum += raitingCustomer.starValueR;
                count++;
            }
        }
        
        return count > 0 ? (float)sum / count : 0f;
    }
}