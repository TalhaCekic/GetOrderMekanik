using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class TrashTrigger : NetworkBehaviour
{
    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cleaner")
        {

            this.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            yield return new WaitForSeconds(0.5f);
            this.gameObject.SetActive(false);
            
        }
    }
}
