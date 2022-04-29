using UnityEngine;
using ElRaccoone.Tweens;

/*
 * Class to control Item HUD
 */
public class ItemHudDisplay : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private ItemSlot _itemSlotPrefab;
    [SerializeField]
    private Vector3 _itemSlotGap = new Vector3(10f,0f,0f);
    [SerializeField]
    private Vector3 _itemSlotStartingOffset = new Vector3(25f,20f,0f);
    [SerializeField]
    private float _tweenDuration = 0.1f;

    private ItemSlot[] itemSlots;
    private int currentActiveIdx;
    private bool isExpanded = false;
    private bool onTransition = false;
    #endregion

    #region SetGet
    public void SelectActiveSlot(int index)
    {
        itemSlots[currentActiveIdx].SetSelected(false);
        
        if (!isExpanded)
        {
            itemSlots[currentActiveIdx].gameObject.SetActive(false);
            itemSlots[index].gameObject.SetActive(true);
        
        }
        itemSlots[index].SetSelected(true);
        currentActiveIdx = index;
    }
    #endregion

    #region MonoBehaviour
    #endregion

    #region Initialization
    public void Init(int numSlot, int activeIdx)
    {
        currentActiveIdx = activeIdx;
        GenerateSlot(numSlot);
        SelectActiveSlot(activeIdx);
    }
    private void GenerateSlot(int numSlot)
    {
        itemSlots = new ItemSlot[numSlot];
        for (int i = 0; i < numSlot; i++)
        {
            ItemSlot instance = Instantiate(_itemSlotPrefab, transform);
            instance.name = "Slot " + (i + 1);
            instance.transform.localPosition = CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, 0);
            instance.SetQuickslotNum(i + 1);
            if (i != currentActiveIdx) instance.gameObject.SetActive(false);
            itemSlots[i] = instance;
        }
    }
    #endregion

    #region Transition
    private void Expand()
    {
        ItemSlot activeSlot = itemSlots[currentActiveIdx];
        activeSlot.transform.TweenLocalPosition(
            CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, currentActiveIdx),
            _tweenDuration
        ).SetOnComplete(
            () =>
            {
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if (i != currentActiveIdx)
                    {
                        itemSlots[i].gameObject.SetActive(true);
                        itemSlots[i].transform.localPosition = CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, i);
                    }
                }
                onTransition = false;
                isExpanded = true;
            }
        );
    }

    private void Shrink()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i == currentActiveIdx)
            {
                itemSlots[i].transform.TweenLocalPosition(
                    CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, 0),
                    _tweenDuration
                    ).SetOnComplete(
                        () => 
                        {
                            isExpanded = false;
                            onTransition = false;
                        });
            } else
            {
                itemSlots[i].transform.localPosition = CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, 0);
                itemSlots[i].gameObject.SetActive(false);
            }
        }
    }

    public void ToggleDisplay()
    {
        if (onTransition) return; //TODO: race condition bug, tab still spammable

        onTransition = true;
        if (isExpanded) Shrink();
        else Expand();
    }
    #endregion

    #region PositionCalculation
    private Vector3 CalculateExpandedPosition(Vector3 offset, Vector3 gap, int index)
    {
        return offset + ((gap + new Vector3(ItemSlot.GetScaledRadius(), 0, 0)) * index);
    }
    #endregion
}
