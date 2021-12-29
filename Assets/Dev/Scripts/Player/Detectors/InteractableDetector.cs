using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Subclass of ObjectDetector class.
 * Used to detect Interactable objects with the "Interact" action.
 * 
 * Will call the OnInteraction function on the Interactable object.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class InteractableDetector : ObjectDetector
{
    private void Awake()
    {
        detectionTag = "Interactable";
        playerInput = GetComponentInParent<PlayerInput>();
        playerInput.actions["Interact"].performed += CheckInteraction;
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[INTERACTABLE] Player interacted with " + closest.name);
        closest.OnInteraction();
    }

    protected override Collider[] FindOverlaps()
    {
        CapsuleCollider CapsuleDetector = GetComponent<CapsuleCollider>();
        Vector3 startCapsulePos = transform.position + new Vector3(0f, CapsuleDetector.height / 2f) +
            CapsuleDetector.center;
        Vector3 finalCapsulePos = transform.position - new Vector3(0f, CapsuleDetector.height / 2f) +
            CapsuleDetector.center;
        return Physics.OverlapCapsule(startCapsulePos, finalCapsulePos, CapsuleDetector.radius);
    }
}
