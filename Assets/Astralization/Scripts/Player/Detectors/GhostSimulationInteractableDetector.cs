using UnityEngine;

/*
 * Subclass of ObjectDetector class.
 * Used to temporary simulate Ghost "Interact" action.
 * 
 * Will call the onGhostInteraction function on the Interactable object.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class GhostSimulationInteractableDetector : ObjectDetector
{
    private void Start()
    {
        detectionTag = "Item";
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[INTERACTABLE] Player interacted with " + closest.name);
        if (closest is EvidenceItem)
        {
            EvidenceItem castedClosest = (EvidenceItem) closest;
            castedClosest.OnGhostInteraction();
        }
    }

    protected override Collider[] FindOverlaps()
    {
        CapsuleCollider interactZone = GetComponent<CapsuleCollider>();
        return Utils.GeometryCalcu.FindOverlapsFromCollider(transform, interactZone);
    }
}
