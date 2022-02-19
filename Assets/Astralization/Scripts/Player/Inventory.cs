using System;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IInventory
{
    void DiscardItemInput();
    int GetActiveIdx();
    IItem GetActiveItem();
    IItem GetItemByIndex(int idx);
    int GetLength();
    int GetNumOfItem();
    float GetScrollStep();
    void PickItem(Item item);
    void ScrollActiveItem(Vector2 scrollVector);
    void UseActiveItem();
    string GetActiveItemName();
}

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
public class Inventory : MonoBehaviour, IInventory
{
    #region Variables - Item List
    [Header("Item List")]
    [Tooltip("The list of items")]
    private Item[] _items;

    [Tooltip("Inventory Length")]
    public int InvenLength = 3;

    [Tooltip("The current active item")]
    private Item _activeItem = null;

    [Tooltip("The current active index")]
    private int _activeIdx = 0;

    [Tooltip("The number of item in inventory")]
    private int _numOfItem = 0;
    #endregion

    #region Variables - Item position adjustment
    [Space]
    [Tooltip("Active item height offset")]
    public Vector3 ActiveItemYOffset = new Vector3(0, -0.5f, 0);
    #endregion

    #region Variables - Input System
    [Header("Input System")]
    [Tooltip("Mouse scroll sensitivity in integer")]
    public float ScrollSensitivity = 1;

    [Tooltip("Scroll value step for each strength")]
    private const int _scrollStep = 120;
    #endregion

    #region Events
    public static event Action<bool, Sprite> ItemLogoEvent;

    public static event Action<Item> DiscardItemEvent;

    #endregion

    private void Awake()
    {
        _items = new Item[InvenLength];
    }

    #region Setter Getter
    public int GetLength() { return InvenLength; }
    public int GetNumOfItem() { return _numOfItem; }
    public int GetActiveIdx() { return _activeIdx; }
    public IItem GetActiveItem() { return _activeItem; }
    public IItem GetItemByIndex(int idx) { return _items[idx]; }
    public float GetScrollStep() { return _scrollStep * ScrollSensitivity; }
    public string GetActiveItemName() { return _activeItem.name; }

    #endregion

    #region Pick - Discard
    public void PickItem(Item item)
    {
        if (_numOfItem == InvenLength)
        {
            Debug.Log("[INVENTORY] Cannot pick item, inventory full");
        }
        else
        {
            int pickedIdx = _activeIdx;

            if (_activeItem)
            {
                for (int i = 0; i < InvenLength; i++)
                {
                    // Find empty slot in inventory,
                    // search from activeIdx to the end of array
                    // and continue from the front of the array
                    int cur = (_activeIdx + i) % InvenLength;
                    if (!_items[cur])
                    {
                        _items[cur] = item;
                        pickedIdx = cur; // For logs
                        break;
                    }
                }

                _numOfItem++;
                item?.HideItem();
            }
            else
            {
                _items[_activeIdx] = item;
                _activeItem = item;
                _numOfItem++;

                ItemLogoEvent?.Invoke(true, _activeItem.GetItemLogo());
            }

            // Put item as child of Inventory
            item.gameObject.transform.parent = this.transform;
            item.transform.position = this.transform.position + ActiveItemYOffset;  // Reposition object to middle body of player
            item.transform.rotation = this.transform.rotation;  // Reset item rotation
            item.OnInteraction();

            //Debug.Log("[INVENTORY] Pick " + item.name + " on position " + pickedIdx);
        }
    }

    private void DiscardItem()
    {
        //Debug.Log("[INVENTORY] Discard " + _activeItem.name);

        // Activate collider and mesh renderer
        _activeItem.Discard();

        // Reposition item to world
        DiscardItemEvent?.Invoke(_activeItem);
        _activeItem.transform.position -= ActiveItemYOffset + new Vector3(0, this.transform.position.y, 0);

        // Reset active item state
        _activeItem = null;
        _items[_activeIdx] = null;
        _numOfItem--;

        ItemLogoEvent?.Invoke(false, null);
    }

    public void DiscardItemInput()
    {
        if (_activeItem) DiscardItem();
        else
        {
            Debug.Log("[INVENTORY] No item to discard, not holding an item");
        }
    }
    #endregion

    #region Change Active Item
    public void ScrollActiveItem(Vector2 scrollVector)
    {
        float scrollValue = scrollVector.y;
        int indexShift = (int) (scrollValue / GetScrollStep());
        int newIdx = Utils.MathCalcu.mod(_activeIdx - indexShift, InvenLength);
        ChangeActiveItem(newIdx);

        //Debug.Log("[INVENTORY] Change active item to " + (activeItem ? activeItem.name : "nothing") + " with index " + activeIdx);
    }

    private void ChangeActiveItem(int newIdx)
    {
        // Hide active item
        _activeItem?.HideItem();

        // Change active item
        _activeIdx = newIdx;
        _activeItem = _items[_activeIdx];

        // Show active item
        _activeItem?.ShowItem();

        if(_activeItem) ItemLogoEvent?.Invoke(true, _activeItem.GetItemLogo());
        else ItemLogoEvent?.Invoke(false, null);
    }
    #endregion

    public void UseActiveItem()
    {
        _activeItem?.Use();

        if (!_activeItem) Debug.Log("[ITEM] Missing active item");
        else if (_activeItem.IsDiscardedWhenUsed()) DiscardItem();
    }
}
