using UnityEngine;

/*
 * Class to keep all items in the overworld.
 * AddItemChild() to add a new items as child of the OverworldItem.
 */
public class ItemManager : MonoBehaviour
{
    #region MonoBehaviour
    private void OnEnable()
    {
        Inventory.DiscardItemEvent += ReceiveDiscardedItem;
    }

    private void OnDisable()
    {
        Inventory.DiscardItemEvent -= ReceiveDiscardedItem;
    }
    #endregion

    private void ReceiveDiscardedItem(Item discardedItem)
    {
        AddItemChild(discardedItem);
    }

    public void AddItemChild(Item item)
    {
        item.gameObject.transform.parent = this.transform;
    }
}
