using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceNumberLettuce : MonoBehaviour
{
    public static int LettuceHowMoney = 0;

    public GameObject[] objects;

    void Update()
    {
        for (int i = 0; i < LettuceHowMoney; i++)
        {
            if (LettuceHowMoney == 3)
            {
                objects[i].SetActive(true);
            }
            if (LettuceHowMoney == 2)
            {
                objects[i].SetActive(true);

            }
            if (LettuceHowMoney == 1)
            {
                objects[i].SetActive(true);
            }
        }

    }
}
