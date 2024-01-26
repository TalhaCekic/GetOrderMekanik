using System;
using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pcMenu : NetworkBehaviour
{
    public GameObject myComputer;
    public GameObject trash;
    public GameObject orderRating;
    public GameObject customize;

    void Start()
    {
        this.transform.localScale = new (0, 0, 0);
        myComputer.transform.localScale = new (0, 0, 0);
        trash.transform.localScale = new (0, 0, 0);
        orderRating.transform.localScale = new (0, 0, 0);
        //customize.transform.localScale = new (0, 0, 0);
        myComputer.SetActive(false);
        trash.SetActive(false);
        //orderRating.SetActive(false);
        customize.SetActive(false);
    }

    public void myComputerButton()
    {
        myComputer.SetActive(true);
    }

    public void trashButton()
    {
        trash.SetActive(true);
    }

    public void orderRatingButton()
    {
        orderRating.transform.localScale = new (1,1,1);
        // orderRating.SetActive(true);
    }

    public void customizeButton()
    {
       // customize.transform.localScale = new (1, 1, 1);
         customize.SetActive(true);
    }

    public void BackButton()
    {
        // myComputer.transform.localScale = new (0, 0, 0);
        // trash.transform.localScale = new (0, 0, 0);
         orderRating.transform.localScale = new (0, 0, 0);
        // customize.transform.localScale = new (0, 0, 0);
        myComputer.SetActive(false);
        trash.SetActive(false);
        //orderRating.SetActive(false);
        customize.SetActive(false);
    }
}