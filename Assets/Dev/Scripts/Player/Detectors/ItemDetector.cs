using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Subclass of ObjectDetector class.
 * Used to detect Item objects with the "PickItem" action.
 * 
 * Will call the Inventory to pick this item.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class ItemDetector : ObjectDetector
{
    [Tooltip("The inventory of the player for this item detector")]
    private Inventory inventory;

    [Tooltip("Detector zone for item")]
    private CapsuleCollider detectorZone;

    private void Start()
    {
        detectionTag = "Item";
        detectorZone = GetComponent<CapsuleCollider>();
        inventory = GetComponent<Inventory>();
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[ITEM] Player picked " + closest.name);
        inventory.PickItem((Item)closest);
    }

    protected override Collider[] FindOverlaps()
    {
        return Utils.GeometryCalcu.FindOverlapsFromCollider(transform, detectorZone);
    }
}
