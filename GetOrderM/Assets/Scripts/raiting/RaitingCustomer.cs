using System.Collections;
using System.Collections.Generic;
using Mirror;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class RaitingCustomer : NetworkBehaviour
{
    public Image[] pp;
    public string name;

    public string comment;
    public int starValueR;
    public Image[] star;

    public TMP_Text nameText;
    public TMP_Text commantText;

    void Start()
    {
        //isim ve yorum
        name = RaitingManager.instance.randomCustomerName;
        comment = RaitingManager.instance.randomGoodComment;
        nameText.text = name;
        commantText.text = comment;

        //yıldız
        starValueR = RaitingManager.instance.starValue;
        for (int i = 0; i < star.Length; i++)
        {
            star[starValueR].gameObject.SetActive(true);
        }

        int randomIndex = Random.Range(0, pp.Length);
        for (int i = 0; i < pp.Length; i++)
        {
            pp[randomIndex].gameObject.SetActive(true);
        }
    }
}