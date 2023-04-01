using Astralization.Items;
using Astralization.Player;
using UnityEngine;

namespace Astralization.Managers
{

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

        #region Item Handler
        private void ReceiveDiscardedItem(Item discardedItem)
        {
            AddItemChild(discardedItem);
        }

        public void AddItemChild(Item item)
        {
            item.gameObject.transform.parent = transform;
            item.UpdateMarker();
        }
        #endregion
    }
}