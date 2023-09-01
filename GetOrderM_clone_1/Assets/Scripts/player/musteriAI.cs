using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class musteriAI : MonoBehaviour
{
    public static musteriAI instance;
    public NavMeshAgent agent;
    public GameObject exit;    // ��k�� objesi

    public GameObject[] gameManager;
    public Animator anim;

    public bool order = false;   // sipari� verip vermedi�ini g�sterir
    public bool finishedEat = false;   //yeme�in bitimini g�sterir
    public bool goTable = false;  // masaya y�neldi�ini g�sterir.
    public bool quitGo = false;   // masadan ��k�p ��k��a y�nelmesini g�sterir
    public bool spawnEt = false;

    public RaycastHit hit;

    public GameObject test;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }
    void Update()
    {
        if (!quitGo)
        {
            tableSelected();
        }
        if (finishedEat)
        {
            agent.destination = exit.transform.position;
            anim.SetBool("end", false);
        }
    }

    IEnumerator deleteCustomer()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void tableSelected()
    {
        List<GameObject> prefabList = new List<GameObject>(GameObject.FindGameObjectsWithTag("dinnertable"));
        int randomIndex = Random.Range(0, prefabList.Count);
        GameObject selectedTable = prefabList[randomIndex];
        dinnerTable selectedTableScript = selectedTable.GetComponent<dinnerTable>();
        // Bo� olan masay� ara
        foreach (GameObject table in prefabList)
        {
            dinnerTable script = table.GetComponent<dinnerTable>();
            if (script.emptyDinnerTable == true)
            {
                agent.destination = table.transform.position;
                spawnEt = true; 
                // Se�ilen masan�n "emptyDinnerTable" �zelli�ini false yap
             //   selectedTableScript.emptyDinnerTable = false;

                // Yeni masay� kaydet
                selectedTable = table;
                selectedTableScript = script;

                // D�ng�den ��k
                break;
            }
        }
        // Se�ilen masay� listeden ��kar
        if (selectedTableScript.emptyDinnerTable == false)
        {
            prefabList.Remove(selectedTable);
        }
    }
    [System.Obsolete]
    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("dinnertable"))
        {
            dinnerTable script = other.gameObject.GetComponent<dinnerTable>();
            if (finishedEat == true)
            {
                StartCoroutine(deleteCustomer());
                script.burgerOrder.SetActive(false);
                order = false;
                script.emptyDinnerTable = true;
                script.eatDelay = 4;
            }
            else
            {
                agent.transform.rotation = script.chairPosition.rotation;
                agent.destination = script.chairPosition.position;
                script.emptyDinnerTable = false;
                goTable = false;
                order = true;
                anim.SetBool("end", true);
              
            }
        }

    }

}


