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
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[INTERACTABLE] Player interacted with " + closest.name);
        closest.OnInteraction();
    }

    protected override Collider[] FindOverlaps()
    {
        CapsuleCollider interactZone = GetComponent<CapsuleCollider>();
        return Utils.GeometryCalcu.FindOverlapsFromCollider(transform, interactZone);
    }
}
