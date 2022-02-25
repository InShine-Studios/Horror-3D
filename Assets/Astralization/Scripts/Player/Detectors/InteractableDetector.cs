using UnityEngine;

/*
 * Subclass of ObjectDetector class.
 * Used to detect Interactable objects with the "Interact" action.
 * 
 * Will call the OnInteraction function on the Interactable object.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class InteractableDetector : ObjectDetector
{
    protected override void Start()
    {
        base.Start();
        detectionTag = "Interactable";
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[INTERACTABLE] Player interacted with " + closest.name);
        closest.OnInteraction();
    }
}
