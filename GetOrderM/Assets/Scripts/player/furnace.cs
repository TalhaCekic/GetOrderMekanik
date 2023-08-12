using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class furnace : MonoBehaviour
{
    //ocak ::
    public bool burgerdolu = false;
    public bool cleanPlatedolu = false;
    public bool dirtyPlatedolu = false;
    public bool meatRawdolu = false;
    public bool meatbakeddolu = false;
    public bool tomatodolu = false;
    public bool tomatoSliceDolu = false;
    public bool lettucedolu = false;
    public bool lettuceSliceDolu = false;
    public bool cheddarCheeseDolu = false;

    public GameObject burger;         //id : 1
    public GameObject dirtyPlate;     //id : 2.2
    public GameObject cleanPlate;     //id : 2
    public GameObject meatRaw;        //id : 3.3
    public GameObject meatBaked;      //id : 3
    public GameObject tomato;         //id : 4.4
    public GameObject SliceTomato;    //id : 4
    public GameObject lettuce;        //id : 5.5
    public GameObject SliceLettuce;   //id : 5
    public GameObject cheddarCheese;  //id : 6


    public float counterID = 0;
    public bool notCombine;

    private float cookTime = 10;

    void Start()
    {


    }
    // Update is called once per frame
    void Update()
    {
        IDcheck();
        IDdetector();
        if (counterID == 3.3f)
        {
            cookTime -= Time.deltaTime;
            if (cookTime < 0)
            {
                cooked();
                if (counterID == 3)
                {
                    meatRawdolu = false;
                    meatbakeddolu = true;
                }
            }
        }
    }
    private void IDcheck()
    {
        if (counterID == 0)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
            //if (pickUp.handFull == false)
            //{
            //    burgerdolu = false;
            //    cleanPlatedolu = false;
            //    meatbakeddolu = false;
            //    tomatoSliceDolu = false;
            //    lettuceSliceDolu = false;
            //    cheddarCheeseDolu = false;
            //}
        }
        // Tekli Kombinasyon
        if (counterID == 1)
        {
            notCombine = false;
            burger.SetActive(true);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 2)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);  // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 2.2f)
        {
            notCombine = true;
            burger.SetActive(false);
            dirtyPlate.SetActive(true);   // TRUE
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 3)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);   // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 3.3f)
        {
            notCombine = true;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(true);     // TRUE
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 4)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);    // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 4.4f)
        {
            notCombine = true;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(true);           // TRUE
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 5)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 5.5f)
        {
            notCombine = true;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(true);            // TRUE
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 6)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);     // TRUE
        }

        // 2 li kombinasyon
        if (counterID == 12)
        {
            notCombine = false;
            burger.SetActive(true);          // true
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // true
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 13)
        {
            notCombine = false;
            burger.SetActive(true);       // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);     // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 14)
        {
            notCombine = false;
            burger.SetActive(true);             // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);       // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 15)
        {
            notCombine = false;
            burger.SetActive(true);             // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);      // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 16)
        {
            notCombine = false;
            burger.SetActive(true);                 // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);         // TRUE
        }
        if (counterID == 23)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);   // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);    // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 24)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);        // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 25)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 26)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);     // TRUE
        }
        if (counterID == 34)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);     // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 35)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);         // true
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // true
            cheddarCheese.SetActive(false);
        }
        if (counterID == 36)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);          // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);      // TRUE
        }
        if (counterID == 45)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);          // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);         // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 46)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);          // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);       // TRUE
        }
        if (counterID == 56)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);          // TRUE
            cheddarCheese.SetActive(true);         // TRUE
        }

        // 3 li kombinasyon
        if (counterID == 123)
        {
            notCombine = false;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 124)
        {
            notCombine = false;
            burger.SetActive(true);            // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);        // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 125)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 126)
        {
            notCombine = false;
            burger.SetActive(true);          // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);     // TRUE
        }
        if (counterID == 134)
        {
            notCombine = false;
            burger.SetActive(true);        // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);     // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 135)
        {
            notCombine = false;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 136)
        {
            notCombine = false;
            burger.SetActive(true);             // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);          // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);      // TRUE
        }
        if (counterID == 145)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 146)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 156)
        {
            notCombine = false;
            burger.SetActive(true);        // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);  // TRUE
        }
        if (counterID == 234)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 235)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);    // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 236)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);     // TRUE
        }
        if (counterID == 245)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 246)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);    // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true); // TRUE
        }
        if (counterID == 256)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(true);  // TRUE
        }
        if (counterID == 345)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);          // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);        // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);       // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 346)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);   // TRUE
        }
        if (counterID == 356)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);        // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 456)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(true);   // TRUE
        }

        // 4 li kombinasyon
        if (counterID == 1234)
        {
            notCombine = false;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(false);
        }
        if (counterID == 1235)
        {
            notCombine = false;
            burger.SetActive(true);            // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);        // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);      // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 1236)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);        // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 1245)
        {
            notCombine = false;
            burger.SetActive(true);          // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 1246)
        {
            notCombine = false;
            burger.SetActive(true);        // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);    // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);   // TRUE
        }
        if (counterID == 1256)
        {
            notCombine = false;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(true);  // TRUE
        }
        if (counterID == 1345)
        {
            notCombine = false;
            burger.SetActive(true);            // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);         // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);       // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);      // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 1346)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);        // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 1356)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);        // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 1456)
        {
            notCombine = false;
            burger.SetActive(true);        // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(true);  // TRUE
        }
        if (counterID == 2345)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 2346)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);      // TRUE
        }
        if (counterID == 2356)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);      // TRUE
            cheddarCheese.SetActive(true);     // TRUE
        }
        if (counterID == 2456)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 3456)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);     // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);   // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);  // TRUE
            cheddarCheese.SetActive(true); // TRUE
        }

        // 5 li kombinasyon
        if (counterID == 12345)
        {
            notCombine = false;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);    // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(false);
        }
        if (counterID == 12346)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);        // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);          // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);        // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(false);
            cheddarCheese.SetActive(true);        // TRUE
        }
        if (counterID == 12356)
        {
            notCombine = false;
            burger.SetActive(true);          // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(false);
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);   // TRUE
        }
        if (counterID == 12456)
        {
            notCombine = false;
            burger.SetActive(true);          // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);      // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(false);
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 13456)
        {
            notCombine = false;
            burger.SetActive(true);           // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(false);
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);       // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);     // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);    // TRUE
            cheddarCheese.SetActive(true);   // TRUE
        }
        if (counterID == 23456)
        {
            notCombine = false;
            burger.SetActive(false);
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);       // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);        // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);      // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);     // TRUE
            cheddarCheese.SetActive(true);    // TRUE
        }
        if (counterID == 123456)
        {
            notCombine = true;
            burger.SetActive(true);         // TRUE
            dirtyPlate.SetActive(false);
            cleanPlate.SetActive(true);     // TRUE
            meatRaw.SetActive(false);
            meatBaked.SetActive(true);      // TRUE
            tomato.SetActive(false);
            SliceTomato.SetActive(true);    // TRUE
            lettuce.SetActive(false);
            SliceLettuce.SetActive(true);   // TRUE
            cheddarCheese.SetActive(true);  // TRUE
        }
    }

    public void IDdetector()
    {
        //tekli ve ikili kombinasyonlar
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 1;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 2;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu && dirtyPlatedolu && !meatRawdolu && !tomato && !lettucedolu)
        {
            counterID = 2.2f;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 3;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu && !dirtyPlatedolu && meatRawdolu && !tomato && !lettucedolu)
        {
            counterID = 3.3f;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 4;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu && !dirtyPlatedolu && !meatRawdolu && tomato && !lettucedolu)
        {
            counterID = 4.4f;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 5;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu && !dirtyPlatedolu && !meatRawdolu && !tomato && lettucedolu)
        {
            counterID = 5.5f;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 6;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 12;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 13;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 14;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 15;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 16;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 23;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 24;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 25;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 26;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 34;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 35;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 36;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 45;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 46;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 56;
        }

        // 3 lü kombinasyonlar
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 123;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 124;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 125;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 126;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 134;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 135;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 136;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 145;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 146;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 156;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 234;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 235;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 236;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 245;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 246;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 256;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 345;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 346;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 356;
        }
        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 456;
        }

        // 4 lü kombinasyonlar
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 1234;
        }
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 1235;
        }
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1236;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 1245;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1246;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1256;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 1345;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1346;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1356;
        }
        if (burgerdolu && !cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 1456;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 2345;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 2346;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 2356;
        }
        if (!burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 2456;
        }
        if (!burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 3456;
        }

        // 5 li kombinasyonlar
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && !cheddarCheeseDolu)
        {
            counterID = 12345;
        }
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && !lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 12346;
        }
        if (burgerdolu && cleanPlatedolu && meatbakeddolu && !tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 12356;
        }
        if (burgerdolu && cleanPlatedolu && !meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 12456;
        }
        if (burgerdolu && !cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 13456;
        }
        if (!burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 23456;
        }

        if (burgerdolu && cleanPlatedolu && meatbakeddolu && tomatoSliceDolu && lettuceSliceDolu && cheddarCheeseDolu)
        {
            counterID = 123456;
        }

        if (!burgerdolu && !cleanPlatedolu && !meatbakeddolu && !tomatoSliceDolu && !lettuceSliceDolu && !cheddarCheeseDolu && !dirtyPlatedolu && !meatRawdolu && !tomatodolu && !lettucedolu)
        {
            counterID = 0;
        }
    }

    private void cooked()
    {
        counterID = 3;
        cookTime = 10;
    }
}
