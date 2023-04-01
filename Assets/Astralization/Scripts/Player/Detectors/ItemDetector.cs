using Astralization.Items;
using Astralization.Utils.Calculation;
using System.Collections.Generic;
using UnityEngine;

namespace Astralization.Player.Detectors
{

    /*
     * Subclass of ObjectDetector class.
     * Used to detect Item objects with the "PickItem" action.
     * 
     * Will call the Inventory to pick this item.
     */
    [RequireComponent(typeof(CapsuleCollider))]
    public class ItemDetector : MonoBehaviour
    {
        #region Variables
        [Tooltip("The inventory of the player for this item detector")]
        private Inventory _inventory;

        [Header("Dependant on Detectors")]
        [Tooltip("Tag to distinguish interactable types")]
        protected string detectionTag;

        [Tooltip("Current closest object to this detector")]
        protected List<Item> nearbyItems;

        [Tooltip("Current closest object to this detector")]
        protected Item closestItem;
        #endregion

        #region SetGet
        public Item GetClosest()
        {
            return closestItem;
        }
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            detectionTag = "Item";
            _inventory = GetComponent<Inventory>();
            nearbyItems = new List<Item>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(detectionTag))
            {
                Item item = other.GetComponent<Item>();
                // Add interactable to list
                nearbyItems.Add(item);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(detectionTag))
            {
                Item item = other.GetComponent<Item>();
                // Remove interactable from list
                nearbyItems.Remove(item);

                if (item.Equals(closestItem))
                {
                    // Turn off icon
                    closestItem.ShowMarker(false);
                    closestItem = null;
                }
            }
        }

        private void OnEnable()
        {
            PlayerMovement.FindClosest += UpdateClosestItem;
        }

        private void OnDisable()
        {
            PlayerMovement.FindClosest -= UpdateClosestItem;
        }
        #endregion

        #region Handler
        protected virtual void InteractClosest(Item closest)
        {
            //Debug.Log("[PLAYER INTERACTION] Picked " + closest.name);
            _inventory.PickItem(closest);
            nearbyItems.Remove(closest);
            closestItem = null;
            UpdateClosestItem();
        }

        private void UpdateClosestItem()
        {
            float minDist = Mathf.Infinity;
            Item newClosest = null;

            for (int i = 0; i < nearbyItems.Count; i++)
            {
                Item cur = nearbyItems[i];

                // Calculate minimum distance and update closest interactable
                float curDist = GeometryCalcu.GetDistance3D(transform.position, cur.transform.position);
                if (curDist < minDist)
                {
                    minDist = curDist;
                    newClosest = cur;
                }
            }

            if (closestItem && !newClosest.Equals(closestItem))
            {
                closestItem.ShowMarker(false);
                //Debug.Log("[PLAYER INTERACTION] Updated closest interactable to " + closestInteractable.name);
            }
            closestItem = newClosest; // newClosest can be null when no nearby
            closestItem?.ShowMarker(true);
        }

        public void CheckInteraction()
        {
            //Debug.Log("[PLAYER INTERACTION] Initiate interaction");
            if (closestItem)
            {
                InteractClosest(closestItem);
            }
        }
        #endregion
    }
}