using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class hand : MonoBehaviour
{
    //silinecek kod
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

    //[SerializeField] private string selectableTag = "counter";
    [SerializeField] private Material highlightMaterial;
    [SerializeField] private Material defauldMaterial;

    private Transform _selection;

    public GameObject spawnBurger;

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

        if (inHandItem != null && hit.collider != null)
        {

            inHandItem = spawnBurger;
            pickUpParent.transform.SetParent(inHandItem.transform);
            inHandItem.transform.SetParent(null);
            inHandItem = null;

            Rigidbody rb = spawnBurger.GetComponent<Rigidbody>();
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
            Rigidbody rb = spawnBurger.GetComponent<Rigidbody>();
            //
            var selection = hit.transform;
            if (selection.gameObject.tag == "counter")
            {
                int adet = 1;
                for (int i = adet; i > 0;)
                {
                inHandItem = Instantiate(spawnBurger, pickUpParent.position, Quaternion.identity); 
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpParent.transform, false);
                if (rb != null)
                {
                    rb.isKinematic = true;
                      
                }
                return;
                }
             
            }
        }
    }
    private void Update()
    {
        //material deðiþiklikleri****
        if(_selection != null)
        {
           var selectionRenderer = _selection.GetComponent<Renderer>();
            selectionRenderer.material = defauldMaterial;
            _selection = null;
        }
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            var selection = hit.transform;
            if(selection.gameObject.tag== "counter")
            {
                var selectionRenderer = selection.GetComponent<Renderer>();
               
                if (selectionRenderer == null)
                {
                selectionRenderer.material = highlightMaterial;
                }
                _selection = selection;
            }
           
          

             hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
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
