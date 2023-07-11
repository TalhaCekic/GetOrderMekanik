using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moto : MonoBehaviour
{

    [SerializeField] private float hiz;

    private void FixedUpdate()
    {
        float zAxis = Input.GetAxis("Horizontal") * hiz * Time.deltaTime;
        float xAxis = Input.GetAxis("Vertical") * hiz * Time.deltaTime;

        transform.Translate(zAxis,0,xAxis);
        
    }
}
