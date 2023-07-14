using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerItemData : MonoBehaviour
{
    public int[] inventory;
    public GameObject[] slot;
    public GameObject gameController;
    public void Awake()
    {
        gameController = GetComponent<GameObject>();
    }
    void Start()
    {
        inventory = new int[2];
        slot = new GameObject[2];

        slot[0] = GameObject.FindGameObjectWithTag ("hamburger");
        slot[1] = GameObject.FindGameObjectWithTag ("plate");
    }

    public void Update()
    {
     
    }
}
