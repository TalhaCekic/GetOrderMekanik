using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }




    public void Use(GameObject actor)
    {
       
          OnUse?.Invoke();
          Destroy(gameObject);
        
        
       
    }

}
