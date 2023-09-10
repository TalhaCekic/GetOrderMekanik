using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Order", menuName = "Order")]
public class ScriptableOrder : ScriptableObject
{
    public string orderName;
    public int orderID;
    public float couldown;
    public GameObject orderPrefab;
    public Slider sliderCouldown;

    
}
