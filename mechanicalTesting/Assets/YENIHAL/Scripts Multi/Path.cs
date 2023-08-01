using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Path : MonoBehaviour
{
    [SerializeField] private Transform pathParent;
    [SerializeField] private PathType pathType;
    private void Start()
    {
        Vector3[] pathArray = new Vector3[pathParent.childCount];
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = pathParent.GetChild(i).position;
        }

        transform.DOPath(pathArray,50f,pathType).SetLookAt(0.0001f);
    }
}
