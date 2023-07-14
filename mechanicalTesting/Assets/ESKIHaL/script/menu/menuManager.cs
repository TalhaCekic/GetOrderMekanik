using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class menuManager : MonoBehaviour
{
    public GameObject Escape;
   // private bool escape = false;
    void Start()
    {
      Escape.SetActive(false);  
    }
    private void Update()
    {
     
       
    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Escape.gameObject.SetActive(!Escape.gameObject.activeSelf);
        }

    }
    public void CountiuneButton()
    {
       
    }

}
