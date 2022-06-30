using System;
using UnityEngine;

#region EventArgs
// TODO: pindahin ke utils
public abstract class InventoryHudEventArgs: EventArgs
{
    public int InventoryLength;
    public int CurrentActiveIdx;
    public int LogoAnimatorIdx;
    public RuntimeAnimatorController HudLogoAnimatorController;
    public int HudLogoAnimationParam;
}

public class InitInventoryHudEventArgs : InventoryHudEventArgs
{
    public InitInventoryHudEventArgs(int inventoryLenght, int currentActiveIdx)
    {
        InventoryLength = inventoryLenght;
        CurrentActiveIdx = currentActiveIdx;
    }
}

public class UpdateHudLogoEventArgs : InventoryHudEventArgs
{
    public UpdateHudLogoEventArgs(int logoAnimatorIdx, RuntimeAnimatorController hudLogoAnimatorController, int hudLogoAnimationParam = -1)
    {
        LogoAnimatorIdx = logoAnimatorIdx;
        HudLogoAnimatorController = hudLogoAnimatorController;
        HudLogoAnimationParam = hudLogoAnimationParam;
    }
}

public class ChangeActiveItemIdxEventArgs : InventoryHudEventArgs
{
    public ChangeActiveItemIdxEventArgs(int currentActiveIdx)
    {
        CurrentActiveIdx = currentActiveIdx;
    }
}

public class ChangeActiveItemAnimEventArgs : InventoryHudEventArgs
{
    public ChangeActiveItemAnimEventArgs(int hudLogoAnimationParam)
    {
        HudLogoAnimationParam = hudLogoAnimationParam;
    }
}

public class ToggleExpandShrinkEventArgs : InventoryHudEventArgs
{
    public ToggleExpandShrinkEventArgs(){ }
}
#endregion

public interface IInventory
{
    int Size { get; }
    void DiscardItemInput();
    int GetActiveIdx();
    IItem GetActiveItem();
    string GetActiveItemName();
    IItem GetItemByIndex(int idx);
    int GetNumOfItem();
    float GetScrollStep();
    void PickItem(Item item);
    void ScrollActiveItem(Vector2 scrollVector);
    void SetActiveItemByQuickSlot(int newIdx);
    void SetLength(int invenLength);
    void ToggleItemHudDisplay();
    void UseActiveItem();
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
    #region Events
    public static event Action<InventoryHudEventArgs> InventoryHudEvent;
    public static event Action<Item> DiscardItemEvent;
    #endregion

    #region Variables - Dependencies
    private PlayerAnimation _animation;
    #endregion

    #region Variables - Item List
    [Header("Item List")]
    [Tooltip("The list of items")]
    private Item[] _items;

    [Range(3, 5)]
    [SerializeField]
    [Tooltip("Inventory Length")]
    private int _size = 3;
    public int Size { get { return _size; } }

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

    #region SetGet
    public void SetLength(int invenLength)
    {
        _size = invenLength;
        _items = new Item[_size];
    }
    public int GetNumOfItem() { return _numOfItem; }
    public int GetActiveIdx() { return _activeIdx; }
    private void SetActiveItem(int newIdx)
    {
        // Hide active item
        _activeItem?.ShowItem(false);

        // Change active item
        _activeIdx = newIdx;
        _activeItem = _items[_activeIdx];

        // Show active item
        _activeItem?.ShowItem(true);

        _animation.SetHoldingItemAnim(_activeItem != null);

        InvokeHudEvent(new ChangeActiveItemIdxEventArgs(newIdx));
    }
    public IItem GetActiveItem() { return _activeItem; }
    public IItem GetItemByIndex(int idx) { return _items[idx]; }
    public float GetScrollStep() { return _scrollStep * ScrollSensitivity; }
    public string GetActiveItemName() { return _activeItem.name; }
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        _animation = transform.GetComponentInParent<PlayerAnimation>();
        _items = new Item[_size];
        InvokeHudEvent(new InitInventoryHudEventArgs(_size,0));
    }
    #endregion

    #region EventHandler
    private void InvokeHudEvent(InventoryHudEventArgs args)
    {
        InventoryHudEvent.Invoke(args);
    }
    #endregion

    #region Pick - Discard
    public void PickItem(Item item)
    {
        if (_numOfItem == _size)
        {
            Debug.Log("[INVENTORY] Cannot pick item, inventory full");
        }
        else
        {
            int pickedIdx = _activeIdx;

            if (_activeItem)
            {
                for (int i = 0; i < _size; i++)
                {
                    // Find empty slot in inventory,
                    // search from start to the end of array
                    if (!_items[i])
                    {
                        _items[i] = item;
                        pickedIdx = i; // For logs
                        InvokeHudEvent(new UpdateHudLogoEventArgs(i, item.GetHudLogoAnimatorController(), item.LogoState));
                        break;
                    }
                }

                item?.ShowItem(false);
            }
            else
            {
                _items[_activeIdx] = item;
                _activeItem = item;

                InvokeHudEvent(new UpdateHudLogoEventArgs(_activeIdx, item.GetHudLogoAnimatorController(), item.LogoState));
            }

            // Put item as child of Inventory
            item.gameObject.transform.parent = this.transform;
            item.transform.position = this.transform.position + ActiveItemYOffset;  // Reposition object to middle body of player
            item.transform.rotation = this.transform.rotation;  // Reset item rotation
            item.Pick();

            _numOfItem++;
            _animation.SetHoldingItemAnim(true);

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

        _animation.SetHoldingItemAnim(false);

        InvokeHudEvent(new UpdateHudLogoEventArgs(_activeIdx,null));
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
        int indexShift = (int)(scrollValue / GetScrollStep());
        int newIdx = Utils.MathCalcu.mod(_activeIdx - indexShift, _size);
        SetActiveItem(newIdx);

        //Debug.Log("[INVENTORY] Change active item to " + (activeItem ? activeItem.name : "nothing") + " with index " + activeIdx);
    }
    public void SetActiveItemByQuickSlot(int newIdx)
    {
        if (newIdx >= _size)
        {
            Debug.Log("[INVENTORY] IndexOutOfRangeError: Trying to quickslot with index out of range");
        }
        else
        {
            SetActiveItem(newIdx);
            //Debug.Log("[INVENTORY] Change item by quickslot to index " + _activeIdx);
        }
    }
    #endregion

    #region Use Active Item
    public void UseActiveItem()
    {
        _activeItem?.Use();
        InvokeHudEvent(new ChangeActiveItemAnimEventArgs(_activeItem.LogoState));

        if (!_activeItem) Debug.Log("[INVENTORY] Missing active item");
        else if (_activeItem.IsDiscardedWhenUsed()) DiscardItem();
    }
    #endregion

    #region ItemHud
    public void ToggleItemHudDisplay()
    {
        InvokeHudEvent(new ToggleExpandShrinkEventArgs());
    }
    #endregion
}
