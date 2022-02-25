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
    #region Variables
    [Tooltip("The inventory of the player for this item detector")]
    private Inventory _inventory;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        detectionTag = "Item";
        _inventory = GetComponent<Inventory>();
    }
    #endregion

    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[ITEM] Player picked " + closest.name);
        _inventory.PickItem((Item)closest);
    }
}
