using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * The Inventory for a player.
 * The length of the inventory can be set from invenLength.
 * 
 * Uses an array of Item objects to keep the objects.
 * PickItem() will be called by the ItemDetector class when an item is to be picked.
 * DiscardItem() will be called when the action is called.
 * ScrollActiveItem() will be called in Update() and read value of mouse scroll
 * Active item sprite direction and aim direction is handled separately
 */
public class Inventory : MonoBehaviour
{
    #region Variables - Item List
    [Header("Item List")]
    [Tooltip("The list of items")]
    private Item[] items;

    [Tooltip("Inventory Length")]
    public int invenLength = 3;

    [Tooltip("The current active item")]
    private Item activeItem = null;

    [Tooltip("The current active index")]
    private int activeIdx = 0;

    [Tooltip("The number of item in inventory")]
    private int numOfItem = 0;

    [Tooltip("Overworld Item in Level")]
    public GameObject overworldItem;
    #endregion

    #region Variables - Item position adjustment
    [Space]
    [Tooltip("Active item height offset")]
    public Vector3 activeItemYOffset = new Vector3(0, -0.5f, 0);
    #endregion

    #region Variables - Input System
    [Header("Input System")]
    [Tooltip("Mouse scroll sensitivity in integer")]
    public int scrollSensitivity = 1;

    [Tooltip("Scroll value step for each strength")]
    private const int scrollStep = 120;
    #endregion

    private void Awake()
    {
        items = new Item[invenLength];
    }

    #region Pick - Discard
    public void PickItem(Item item)
    {
        if (numOfItem == invenLength)
        {
            Debug.Log("[INVENTORY] Cannot pick item, inventory full");
        }
        else
        {
            int pickedIdx = activeIdx;

            if (activeItem)
            {
                for (int i = 0; i < invenLength; i++)
                {
                    // Find empty slot in inventory,
                    // search from activeIdx to the end of array
                    // and continue from the front of the array
                    int cur = (activeIdx + i) % invenLength;
                    if (!items[cur])
                    {
                        items[cur] = item;
                        pickedIdx = cur; // For logs
                        break;
                    }
                }

                numOfItem++;
            }
            else
            {
                items[activeIdx] = item;
                activeItem = item;
                numOfItem++;

                // TODO: Update Inventory HUD
            }

            // Put item as child of Inventory
            item.gameObject.transform.parent = this.transform;
            item.transform.position = this.transform.position + activeItemYOffset;  // Reposition object to middle body of player
            item.transform.rotation = this.transform.rotation;  // Reset item rotation
            item.OnInteraction();

            //Debug.Log("[INVENTORY] Pick " + item.name + " on position " + pickedIdx);
        }
    }

    public void DiscardItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (activeItem)
            {
                //Debug.Log("[INVENTORY] Discard " + activeItem.name);

                // Activate collider and mesh renderer
                activeItem.SetCollider(true);
                activeItem.SetMeshRenderer(true);

                // Reposition item to world
                activeItem.gameObject.transform.parent = overworldItem.transform;
                activeItem.transform.position -= activeItemYOffset + new Vector3(0, this.transform.position.y, 0);

                // Reset active item state
                activeItem = null;
                items[activeIdx] = null;
                numOfItem--;

            }
            else
            {
                Debug.Log("[INVENTORY] No item to discard, not holding an item");
            }
        }
    }
    #endregion

    #region Change item
    public void ScrollActiveItem(InputAction.CallbackContext ctx)
    {
        float scrollValue = ctx.ReadValue<float>();
        int indexShift = (int)scrollValue / (scrollStep * scrollSensitivity);
        int newIdx = Utils.MathCalcu.mod(activeIdx - indexShift, invenLength);
        ChangeActiveItem(newIdx);

        //Debug.Log("[INVENTORY] Change active item to " + (activeItem ? activeItem.name : "nothing") + " with index " + activeIdx);
    }

    private void ChangeActiveItem(int newIdx)
    {
        // Hide active item
        activeItem?.HideItem();

        // Change active item
        activeIdx = newIdx;
        activeItem = items[activeIdx];

        // Show active item
        activeItem?.ShowItem();

        // TODO: Change active item display on HUD
    }
    #endregion

    public void UseActiveItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            activeItem?.Use();
            if (!activeItem) Debug.Log("[ITEM] Missing active item");
        }
    }
}
