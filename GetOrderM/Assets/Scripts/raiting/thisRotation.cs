using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class thisRotation : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(Vector3.up, 0 * Time.deltaTime);
    }
}
