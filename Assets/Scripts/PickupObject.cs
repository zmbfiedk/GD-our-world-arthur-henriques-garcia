using UnityEngine;

public class PickupObject : MonoBehaviour
{
    public Transform holdPoint; // Where the object is held
    public float pickupRange = 2f; // Distance to pick up objects
    public float throwForce = 10f; // Base throw force
    public float maxThrowForce = 20f; // Maximum throw force
    public float chargeTime = 1f; // Time to fully charge a throw
    public float throwThreshold = 0.2f; // Time required to start charging a throw
    public LayerMask pickableLayer;

    private GameObject heldObject;
    private Rigidbody heldRb;
    private float throwCharge;
    private bool isCharging;
    private float holdDuration;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                isCharging = true;
                throwCharge = throwForce;
                holdDuration = 0f;
            }
        }

        if (Input.GetKey(KeyCode.E) && isCharging)
        {
            holdDuration += Time.deltaTime;
            if (holdDuration >= throwThreshold)
            {
                throwCharge += Time.deltaTime * (maxThrowForce - throwForce) / chargeTime;
                throwCharge = Mathf.Clamp(throwCharge, throwForce, maxThrowForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.E) && isCharging)
        {
            if (holdDuration < throwThreshold)
            {
                DropObject();
            }
            else
            {
                ThrowObject();
            }
            isCharging = false;
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, pickableLayer))
        {
            if (hit.collider != null)
            {
                heldObject = hit.collider.gameObject;
                heldRb = heldObject.GetComponent<Rigidbody>();

                if (heldRb != null)
                {
                    heldRb.isKinematic = true;
                    heldObject.transform.position = holdPoint.position;
                    heldObject.transform.parent = holdPoint;
                }
            }
        }
    }

    void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            heldRb.isKinematic = false;

            heldObject = null;
            heldRb = null;
        }
    }

    void ThrowObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.parent = null;
            heldRb.isKinematic = false;
            heldRb.AddForce(transform.forward * throwCharge, ForceMode.Impulse);

            heldObject = null;
            heldRb = null;
        }
    }
}
