using UnityEngine;
using ElRaccoone.Tweens;
using System.Collections;
using UnityEngine.UI;

public interface IItemHudDisplay
{
    void Expand();
    void Init(int numSlot, int activeIdx);
    void SelectActiveSlot(int index);
    void SetItemLogo(int index, Sprite logo);
    Image GetItemLogo(int index);
    Image GetSelectedItemLogo();
}

/*
 * Class to control Item HUD
 */
public class ItemHudDisplay : MonoBehaviour, IItemHudDisplay
{
    #region Variables
    [SerializeField]
    private ItemSlot _itemSlotPrefab;
    [SerializeField]
    private Vector3 _itemSlotGap = new Vector3(10f, 0f, 0f);
    [SerializeField]
    private Vector3 _itemSlotStartingOffset = new Vector3(25f, 20f, 0f);
    [SerializeField]
    private float _tweenDuration = 0.5f;
    [SerializeField]
    private float _expandDuration = 3f;

    private ItemSlot[] itemSlots;
    private int currentActiveIdx;
    private bool isExpanded = false;
    private bool onTransition = false;
    #endregion

    #region SetGet
    public void SetItemLogo(int index, Sprite logo)
    {
        itemSlots[index].SetItemImage(logo);
    }

    public Image GetItemLogo(int index)
    {
        return itemSlots[index].GetItemImage();
    }

    public Image GetSelectedItemLogo()
    {
        return itemSlots[currentActiveIdx].GetItemImage();
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

    #region SlotManager
    public void SelectActiveSlot(int index)
    {
        if (onTransition) return;

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

    #region Transition
    public void Expand()
    {
        if (onTransition || isExpanded) return;

        onTransition = true;
        StartCoroutine(UpdateSelectedSlot());

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
                StartCoroutine(AutoShrink());
            }
        );
    }

    private void Shrink()
    {
        onTransition = true;
        StartCoroutine(UpdateSelectedSlot());

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
            }
            else
            {
                itemSlots[i].transform.localPosition = CalculateExpandedPosition(_itemSlotStartingOffset, _itemSlotGap, 0);
                itemSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator UpdateSelectedSlot()
    {
        yield return new WaitUntil(() => !onTransition);
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i != currentActiveIdx)
            {
                itemSlots[i].SetSelected(false);
            }
        }
    }

    private IEnumerator AutoShrink()
    {
        yield return new WaitForSeconds(_expandDuration);
        Shrink();
    }
    #endregion

    #region PositionCalculation
    private Vector3 CalculateExpandedPosition(Vector3 offset, Vector3 gap, int index)
    {
        return offset + ((gap + new Vector3(ItemSlot.GetScaledRadius(), 0, 0)) * index);
    }
    #endregion
}
