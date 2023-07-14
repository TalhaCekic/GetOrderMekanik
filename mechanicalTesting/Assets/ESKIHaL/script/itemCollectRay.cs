using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class itemCollectRay : MonoBehaviour
{
    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;
    public Animator anim;

    [SerializeField]
    [Min(1)]
    private float hitRange = 3;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    private GameObject inHandItem;

    [SerializeField]
    private InputActionReference interactionInput, dropInput;

    private RaycastHit hit;

    [SerializeField]
    private AudioSource pickUpSource;
    private void Awake()
    {

    }
    private void Start()
    {
        
        interactionInput.action.performed += PickUp;
        dropInput.action.performed += Drop;
    }

    private void Drop(InputAction.CallbackContext obj)
    {
       
        if (inHandItem != null && hit.collider != null )
        {
            anim.SetBool("hand", true);
            print(anim);
            inHandItem = hit.collider.gameObject;
            
            inHandItem.transform.SetParent(null);
            inHandItem = null;

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
    private void PickUp(InputAction.CallbackContext obj)
    {
        
        if (hit.collider != null && inHandItem == null)
        {
            IPickable pickableItem = hit.collider.GetComponent<IPickable>();
            if (pickableItem != null)
            {

                pickUpSource.Play();
                inHandItem = pickableItem.PickUp();
                inHandItem.transform.SetParent(pickUpParent.transform, pickableItem.KeepWorldPosition);
            }

            Debug.Log(hit.collider.name);
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Food>() || hit.collider.GetComponent<Weapon>())
            {
                inHandItem = hit.collider.gameObject;
                inHandItem.transform.position = Vector3.zero;
                // inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                return;
            }
        }
    }
    private void Update()
    {  
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {print("a");
           // hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }

        if (inHandItem != null)
        {
            return;
        }

        if (Physics.Raycast(
            this.transform.position, transform.TransformDirection(Vector3.forward),
            out hit,
            hitRange,
            pickableLayerMask))
        {
        }
    }

}
