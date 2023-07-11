using Unity.VisualScripting;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 15;
    public float spawnInterval = 10f; // Spawn aralýðý (saniye cinsinden)
    private float timer = 0f;

    float lastSpawnTime = 10f;
    bool isSpawned = false;

    void Update()
    {
        if (Time.time > lastSpawnTime + spawnDelay && !isSpawned)
        {
            lastSpawnTime = Time.time;

            GameObject[] emptyTables = GameObject.FindGameObjectsWithTag("dinnertable");
            int emptyTableCount = 0;

            foreach (GameObject table in emptyTables)
            {
                dinnerTable tableScript = table.GetComponent<dinnerTable>();

                if (tableScript.emptyDinnerTable)
                {
                    emptyTableCount++;
                }

            }
            if (emptyTableCount > 0)
            {
                SpawnCustomer();
                isSpawned = true;
            }
            print(emptyTableCount);

        }
        else if (Time.time > lastSpawnTime + spawnInterval)
        {
            isSpawned = false;
        }
    }

    void SpawnCustomer()
    {
        Instantiate(customerPrefab, spawnPoint.transform.position, Quaternion.identity);
    }
}
