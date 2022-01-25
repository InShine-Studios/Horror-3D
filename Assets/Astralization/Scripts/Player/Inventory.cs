using UnityEngine;
using UnityEngine.InputSystem;

public interface IInventory
{
    void DiscardItem(InputAction.CallbackContext ctx);
    int GetActiveIdx();
    IItem GetActiveItem();
    IItem GetItemByIndex(int idx);
    int GetNumOfItem();
    int GetScrollStep();
    void PickItem(Item item);
    void ScrollActiveItem(InputAction.CallbackContext ctx);
    void UseActiveItem(InputAction.CallbackContext ctx);
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

    [Tooltip("Overworld Item in Level")]
    public GameObject OverworldItem;

    [Tooltip("Hud for Item in Level")]
    [SerializeField]
    private ItemHud _itemHud;
    #endregion

    #region Variables - Item position adjustment
    [Space]
    [Tooltip("Active item height offset")]
    public Vector3 ActiveItemYOffset = new Vector3(0, -0.5f, 0);
    #endregion

    #region Variables - Input System
    [Header("Input System")]
    [Tooltip("Mouse scroll sensitivity in integer")]
    public int ScrollSensitivity = 1;

    [Tooltip("Scroll value step for each strength")]
    private const int _scrollStep = 120;
    #endregion

    private void Awake()
    {
        _items = new Item[InvenLength];
    }

    #region Setter Getter
    public int GetNumOfItem() { return _numOfItem; }
    public int GetActiveIdx() { return _activeIdx; }
    public IItem GetActiveItem() { return _activeItem; }
    public IItem GetItemByIndex(int idx) { return _items[idx]; }
    public int GetScrollStep() { return _scrollStep * ScrollSensitivity; }

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
            }
            else
            {
                _items[_activeIdx] = item;
                _activeItem = item;
                _numOfItem++;

                _itemHud.ActivateHud(_activeItem.GetItemLogo());
            }

            // Put item as child of Inventory
            item.gameObject.transform.parent = this.transform;
            item.transform.position = this.transform.position + ActiveItemYOffset;  // Reposition object to middle body of player
            item.transform.rotation = this.transform.rotation;  // Reset item rotation
            item.OnInteraction();

            //Debug.Log("[INVENTORY] Pick " + item.name + " on position " + pickedIdx);
        }
    }

    public void DiscardItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (_activeItem)
            {
                //Debug.Log("[INVENTORY] Discard " + activeItem.name);

                // Activate collider and mesh renderer
                _activeItem.SetCollider(true);
                _activeItem.SetMeshRenderer(true);

                // Reposition item to world
                _activeItem.gameObject.transform.parent = OverworldItem.transform;
                _activeItem.transform.position -= ActiveItemYOffset + new Vector3(0, this.transform.position.y, 0);

                // Reset active item state
                _activeItem = null;
                _items[_activeIdx] = null;
                _activeIdx = -1;
                _numOfItem--;

                _itemHud.DeactivateHud();

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
        int indexShift = (int)scrollValue / (_scrollStep * ScrollSensitivity);
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

        if (_activeItem) _itemHud.ActivateHud(_activeItem.GetItemLogo());
        else _itemHud.DeactivateHud();
    }
    #endregion

    public void UseActiveItem(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _activeItem?.Use();
            if (!_activeItem) Debug.Log("[ITEM] Missing active item");
        }
    }
}
