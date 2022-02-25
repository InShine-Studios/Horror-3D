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

    protected override void Start()
    {
        base.Start();
        detectionTag = "Item";
        _inventory = GetComponent<Inventory>();
    }

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[ITEM] Player picked " + closest.name);
        _inventory.PickItem((Item)closest);
    }
}
