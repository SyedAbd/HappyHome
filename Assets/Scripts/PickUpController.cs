using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController: MonoBehaviour
{
    public float pickupRange = 3f; 
    public Transform pickupPoint; 
    public LayerMask pickupLayer; 
    [SerializeField]private GameObject heldObject;

    public float dropForce = 5f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickUpObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    void TryPickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickupLayer))
        {
            if (hit.collider != null)
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
    }

    void PickUpObject(GameObject obj)
    {
        if (obj.GetComponent<Rigidbody>())
        {
            Rigidbody objRb = obj.GetComponent<Rigidbody>();
            objRb.isKinematic = true; 
            objRb.useGravity = false;
            obj.transform.position = pickupPoint.position;
            obj.transform.SetParent(pickupPoint);

            heldObject = obj;
        }
    }

    void DropObject()
    {
        Rigidbody heldRb = heldObject.GetComponent<Rigidbody>();
        heldRb.isKinematic = false;
        heldRb.useGravity = true;

        heldObject.transform.SetParent(null);
        heldRb.AddForce(transform.forward * dropForce, ForceMode.Impulse);

        heldObject = null; 
    }
}

