using UnityEngine;

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
    private Inventory _inventory;

    [Tooltip("Detector zone for item")]
    private CapsuleCollider _detectorZone;

    private void Start()
    {
        detectionTag = "Item";
        _detectorZone = GetComponent<CapsuleCollider>();
        _inventory = GetComponent<Inventory>();
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[ITEM] Player picked " + closest.name);
        _inventory.PickItem((Item)closest);
    }

    protected override Collider[] FindOverlaps()
    {
        return Utils.GeometryCalcu.FindOverlapsFromCollider(transform, _detectorZone);
    }
}
