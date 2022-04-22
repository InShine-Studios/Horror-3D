using UnityEngine;

/*
 * Class to control Item HUD
 */
public class ItemHud : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private ItemSlot _itemSlotPrefab;
    [SerializeField]
    private Vector3 _itemSlotGap = new Vector3(10f,0f,0f);
    [SerializeField]
    private Vector3 _itemSlotStartingOffset = new Vector3(25f,20f,0f);

    private ItemSlot[] itemSlots;
    private int currentActiveIdx = 0;
    #endregion

    #region SetGet
    public void SetActiveSlot(int index)
    {
        itemSlots[currentActiveIdx].SetActive(false);
        itemSlots[index].SetActive(true);
        currentActiveIdx = index;
    }
    #endregion

    #region MonoBehaviour
    #endregion

    #region Initialization
    public void Init(int numSlot, int activeIdx)
    {
        GenerateSlot(numSlot);
        SetActiveSlot(activeIdx);
    }
    private void GenerateSlot(int numSlot)
    {
        itemSlots = new ItemSlot[numSlot];
        for (int i = 0; i < numSlot; i++)
        {
            ItemSlot instance = Instantiate(_itemSlotPrefab, transform);
            instance.name = "Slot " + (i + 1);
            instance.SetPositionOnCanvas(_itemSlotStartingOffset, _itemSlotGap, i);
            instance.SetQuickslotNum(i + 1);
            itemSlots[i] = instance;
        }
    }
    #endregion
}
