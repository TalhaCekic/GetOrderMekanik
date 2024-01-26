using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;

public class TrashObjectpool : NetworkBehaviour
{
    public static TrashObjectpool instance;

    public SyncList<GameObject> pooledObjects = new SyncList<GameObject>();
    [SerializeField] private GameObject objectPrefab;
    [SerializeField][SyncVar] public int poolSize;

    [SerializeField] GameObject[] objectPrefab2;
    GameObject obj1;

    [SerializeField][SyncVar] private float maxDirty = 10f;
    [SerializeField][SyncVar] private float dirtyValue = 0.01f;

    [SyncVar] public bool isOkey = false;

    [SyncVar] public int currentSize;
   


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }

    private void Start()
    {
        //pooledObjects.Clear();
        //CmdSpawnObject();
    }



    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "PcScene" && isServer)
        {
            if (dirtyValue < maxDirty)
            {
                dirtyValue += Time.deltaTime;
            }
            else if(currentSize <= poolSize)
            {
                SpawnTrash();
                dirtyValue = 0f;
                currentSize++;
            }
        }
    }

    //[Command(requiresAuthority = false)]
    //public void CmdSpawnObject()
    //{

    //    for (int i = 0; i < poolSize; i++)
    //    {
    //        Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 0.07f, Random.Range(-3, 3));

    //        obj1 = Instantiate(objectPrefab, randomPosition, Quaternion.identity, transform);

    //        NetworkServer.Spawn(obj1);
    //        RpcSetActive(obj1, false);
    //        pooledObjects.Add(obj1);
    //        obj1.SetActive(false);
    //    }
    //}

    //[ClientRpc]
    //public void RpcSetActive(GameObject obj, bool isActive)
    //{
    //    obj.SetActive(isActive);
    //    obj.transform.parent = transform;
    //}

    //public GameObject GetPooledObject()
    //{
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        if (!pooledObjects[i].activeInHierarchy)
    //        {
    //            pooledObjects[i].SetActive(true);
    //            return pooledObjects[i];
    //        }
    //    }
    //    return null;
    //}
    //[Command(requiresAuthority = false)]
    //public void GetPooledObjects()
    //{
    //    for (int i = 0; i < pooledObjects.Count; i++)
    //    {
    //        GameObject obj = pooledObjects[i];

    //        RpcSetActive(obj, true);
    //        break;
    //    }
    //}

    public void SpawnTrash()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-5, 5), 0.07f, Random.Range(-3, 3));

        GameObject obj = Instantiate(objectPrefab, randomPosition,Quaternion.identity, this.transform);
        NetworkServer.Spawn(obj);

        obj.SetActive(true);
    }
}