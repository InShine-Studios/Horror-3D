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
    #region Constants
    private enum ExpandDirection
    {
        Left = -1,
        Right = 1
    }
    #endregion

    #region Variables
    [SerializeField]
    private ItemSlot _itemSlotPrefab;
    [SerializeField]
    private ExpandDirection _expandDirection = ExpandDirection.Left;
    [SerializeField]
    private Vector3 _itemSlotGap = new Vector3(10f, 0f, 0f);
    [SerializeField]
    private Vector3 _itemSlotStartingPosition = new Vector3(1750f, 20f, 0f);
    [SerializeField]
    private float _tweenDuration = 0.5f;
    [SerializeField]
    private float _expandDuration = 3f;

    private ItemSlot[] _itemSlots;
    private int _currentActiveIdx;
    private bool _isExpanded = false;
    private bool _onTransition = false;
    #endregion

    #region SetGet
    public void SetItemLogo(int index, Sprite logo)
    {
        _itemSlots[index].SetItemImage(logo);
    }

    public Image GetItemLogo(int index)
    {
        return _itemSlots[index].GetItemImage();
    }

    public Image GetSelectedItemLogo()
    {
        return _itemSlots[_currentActiveIdx].GetItemImage();
    }
    #endregion

    #region MonoBehaviour
    #endregion

    #region Initialization
    public void Init(int numSlot, int activeIdx)
    {
        _currentActiveIdx = activeIdx;
        GenerateSlot(numSlot);
        SelectActiveSlot(activeIdx);
    }
    private void GenerateSlot(int numSlot)
    {
        _itemSlots = new ItemSlot[numSlot];
        for (int i = 0; i < numSlot; i++)
        {
            ItemSlot instance = Instantiate(_itemSlotPrefab, transform);
            instance.name = "Slot " + (i + 1);
            instance.transform.localPosition = CalculateExpandedPosition(_itemSlotStartingPosition, _itemSlotGap, AdjustIndexByDirection(0));
            instance.SetQuickslotNum(i + 1);
            if (i != _currentActiveIdx) instance.gameObject.SetActive(false);
            _itemSlots[i] = instance;
        }
    }
    #endregion

    #region SlotManager
    public void SelectActiveSlot(int index)
    {
        if (_onTransition) return;

        _itemSlots[_currentActiveIdx].SetSelected(false);

        if (!_isExpanded)
        {
            _itemSlots[_currentActiveIdx].gameObject.SetActive(false);
            _itemSlots[index].gameObject.SetActive(true);

        }
        _itemSlots[index].SetSelected(true);
        _currentActiveIdx = index;
    }
    #endregion

    #region Transition
    public void Expand()
    {
        if (_onTransition || _isExpanded) return;

        _onTransition = true;
        StartCoroutine(UpdateSelectedSlot());

        ItemSlot activeSlot = _itemSlots[_currentActiveIdx];
        activeSlot.transform.TweenLocalPosition(
            CalculateExpandedPosition(_itemSlotStartingPosition, _itemSlotGap, _currentActiveIdx),
            _tweenDuration
        ).SetOnComplete(
            () =>
            {
                for (int i = 0; i < _itemSlots.Length; i++)
                {
                    if (i != _currentActiveIdx)
                    {
                        _itemSlots[i].gameObject.SetActive(true);
                        _itemSlots[i].transform.localPosition = CalculateExpandedPosition(_itemSlotStartingPosition, _itemSlotGap, i);
                    }
                }
                _onTransition = false;
                _isExpanded = true;
                StartCoroutine(AutoShrink());
            }
        );
    }

    private void Shrink()
    {
        _onTransition = true;
        StartCoroutine(UpdateSelectedSlot());

        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (i == _currentActiveIdx)
            {
                _itemSlots[i].transform.TweenLocalPosition(
                    CalculateExpandedPosition(_itemSlotStartingPosition, _itemSlotGap, AdjustIndexByDirection(0)),
                    _tweenDuration
                    ).SetOnComplete(
                        () =>
                        {
                            _isExpanded = false;
                            _onTransition = false;
                        });
            }
            else
            {
                _itemSlots[i].transform.localPosition = CalculateExpandedPosition(_itemSlotStartingPosition, _itemSlotGap, AdjustIndexByDirection(0));
                _itemSlots[i].gameObject.SetActive(false);
            }
        }
    }

    private IEnumerator UpdateSelectedSlot()
    {
        yield return new WaitUntil(() => !_onTransition);
        for (int i = 0; i < _itemSlots.Length; i++)
        {
            if (i != _currentActiveIdx)
            {
                _itemSlots[i].SetSelected(false);
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
    private Vector3 CalculateExpandedPosition(Vector3 startPosition, Vector3 gap, int index)
    {
        int directionMultiplier = (int)_expandDirection * AdjustIndexByDirection(index);
        return startPosition + ((gap + new Vector3(ItemSlot.GetScaledRadius(), 0, 0)) * directionMultiplier);
    }

    private int AdjustIndexByDirection(int index)
    {
        if (_expandDirection == ExpandDirection.Left)
        {
            return (_itemSlots.Length - index - 1);
        }
        else
        {
            return index;
        }
    }
    #endregion
}
