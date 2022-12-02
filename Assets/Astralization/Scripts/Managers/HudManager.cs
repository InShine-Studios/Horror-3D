using UnityEngine;
using System.Collections;

/*
 * Class to manage general HUD stuff.
 * All class related to HUD should be managed by this class.
 */
public class HudManager : MonoBehaviour
{
    #region Variables
    private DialogueManager _dialogueManager;
    private HidingOverlay _hidingManager;
    private ItemHudDisplay _itemHud;
    private ExorcismBar _exorcismBar;
    private TimeslotHud _timeslotHud;
    #endregion

    #region SetGet
    private void SetHudState(Utils.UiHelper.UiType hudKey, bool condition)
    {
        switch (hudKey)
        {
            case Utils.UiHelper.UiType.Dialogue: ShowDialogue(condition); break;
            case Utils.UiHelper.UiType.ExorcismBar: ShowExorcism(condition); break;
            case Utils.UiHelper.UiType.HidingOverlay: ShowHidingHud(condition); break;
        }
    }

    private void ShowDialogue(bool isShowDialogue)
    {
        //Debug.Log("[HUD SYSTEM] Set dialogue box visibility to " + isShowDialogue);
        _dialogueManager.ShowDialogueBox(isShowDialogue);
    }

    private void ShowHidingHud(bool isHiding)
    {
        _hidingManager.StartAnim(isHiding);
    }

    private void ShowExorcism(bool isShowExorcism)
    {
        _exorcismBar.ShowBar(isShowExorcism);
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _dialogueManager = GetComponentInChildren<DialogueManager>();
        _hidingManager = GetComponentInChildren<HidingOverlay>();
        _itemHud = GetComponentInChildren<ItemHudDisplay>();
        _exorcismBar = GetComponentInChildren<ExorcismBar>();
        _timeslotHud = GetComponentInChildren<TimeslotHud>();
    }
    private void OnEnable()
    {
        GameManager.HudEvent += SetHudState;
        PlayerHidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.InventoryHudEvent += HandleInventoryEvent;
        TimeslotStateMachine.UpdateTimeslotHudEvent += UpdateTimeslotDisplay;
    }

    private void OnDisable()
    {
        GameManager.HudEvent -= SetHudState;
        PlayerHidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.InventoryHudEvent -= HandleInventoryEvent;
        TimeslotStateMachine.UpdateTimeslotHudEvent -= UpdateTimeslotDisplay;
    }
    #endregion

    #region EventHandler
    private void HandleInventoryEvent(InventoryHudEventArgs args)
    {
        switch (args.GetType().Name)
        {
            case nameof(InitInventoryHudEventArgs):
                _itemHud.Init(args.InventoryLength, args.CurrentActiveIdx);
                break;
            case nameof(UpdateHudLogoEventArgs):
                _itemHud.SetItemLogoAnimator(args.LogoAnimatorIdx, args.HudLogoAnimatorController, args.HudLogoAnimationParam);
                break;
            case nameof(ChangeActiveItemAnimEventArgs):
                _itemHud.SetItemLogoAnimation(args.CurrentActiveIdx, args.HudLogoAnimationParam);
                break;
            case nameof(ChangeActiveItemIdxEventArgs):
                _itemHud.SelectActiveSlot(args.CurrentActiveIdx);
                _itemHud.Expand();
                break;
            default:
                break;
        }
    }

    private void UpdateTimeslotDisplay(DateTimeslot dateTimeslot)
    {
        _timeslotHud.SetDateTimeslot(dateTimeslot);
    }
    #endregion
}
