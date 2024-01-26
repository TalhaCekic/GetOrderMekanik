using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnTriggerTEST : MonoBehaviour
{
    public bool isStay = false;
    public Material[] materials, materials1;

    private void Start()
    {
        this.gameObject.GetComponent<OnTriggerTEST>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "counter")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }
        if (other.gameObject.tag == "Pickup")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }
        if (other.gameObject.tag == "Controllertable")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }

        if (other.gameObject.tag == "PC")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }

        if (other.gameObject.tag == "submid")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }
        if (other.gameObject.tag == "walls")
        {

            isStay = true;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials;
        }
        
        if (other.gameObject.tag == "Player")
        {
            if (this.gameObject.layer != other.gameObject.layer)
            {
                if (this.gameObject.transform.parent != null)
                {
                    if (this.gameObject.GetComponentInParent<pickUp>() != null && this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true)
                    {
                        isStay = true;
                        this.gameObject.GetComponent<MeshRenderer>().materials = materials;
                    }

                }
                else
                {
                    return;
                }
            }
            //if (this.gameObject.GetComponentInParent<pickUp>() !=null && this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true 
            //    && other.gameObject.GetComponent<pickUp>().isControllerTable == false)
            //{
            //    
            //}
            //else if (this.gameObject.GetComponentInParent<pickUp>() != null&& this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true 
            //    && other.gameObject.GetComponent<pickUp>().isControllerTable == true 
            //    && this.gameObject.transform.parent!= this.gameObject.GetComponent<Transform>().transform)
            //{
            //    isStay = true;
            //    this.gameObject.GetComponent<MeshRenderer>().materials = materials;
            //}
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "counter")
        {
            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
        }
        if (other.gameObject.tag == "Pickup")
        {

            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
        }
        if (other.gameObject.tag == "Controllertable")
        {

            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
        }

        if (other.gameObject.tag == "PC")
        {

            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
        }

        if (other.gameObject.tag == "submid")
        {

            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
        }
        if (other.gameObject.tag == "walls")
        {

            isStay = false;
            this.gameObject.GetComponent<MeshRenderer>().materials = materials1;

        }
        if (other.gameObject.tag == "Player" )
        {
            if (this.gameObject.layer != other.gameObject.layer)
            {
                if (this.gameObject.transform.parent != null )
                {
                    if (this.gameObject.GetComponentInParent<pickUp>() != null && this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true)
                    {
                        isStay = false;
                        this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
                    }
                    
                }
                else
                {
                    return;
                }
            }

            //else if (this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true)
            //{
            //    isStay = false;
            //    this.gameObject.GetComponent<MeshRenderer>().materials = materials1;
            //}
            //if (this.gameObject.GetComponentInParent<pickUp>() !=null && this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true 
            //    && other.gameObject.GetComponent<pickUp>().isControllerTable == false)
            //{
            //    
            //}
            //else if (this.gameObject.GetComponentInParent<pickUp>() != null&& this.gameObject.GetComponentInParent<pickUp>().isControllerTable == true 
            //    && other.gameObject.GetComponent<pickUp>().isControllerTable == true 
            //    && this.gameObject.transform.parent!= this.gameObject.GetComponent<Transform>().transform)
            //{
            //    isStay = true;
            //    this.gameObject.GetComponent<MeshRenderer>().materials = materials;
            //}
        }
    }
}
