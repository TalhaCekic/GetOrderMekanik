using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceNumberTomato : MonoBehaviour
{
    public static int tomatoHowMoney = 0;

    public GameObject[] objects;

    void Update()
    {
        for (int i = 0; i < tomatoHowMoney; i++)
        {
            if(tomatoHowMoney == 3)
            {
                objects[i].SetActive(true);
            }
            if(tomatoHowMoney == 2)
            {
                objects[i].SetActive(true);
            }
            if (tomatoHowMoney == 1)
            {
                objects[i].SetActive(true);
            }
        }

    }
}
