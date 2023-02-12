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
    private MindMapCanvas _mindMapCanvas;
    private MindMapModal _mindMapModal;
    private Canvas _canvas;
    #endregion

    #region SetGet
    private void SetHudState(Utils.UiHelper.UiType hudKey, bool condition)
    {
        HideAll();
        switch (hudKey)
        {
            case Utils.UiHelper.UiType.Dialogue: ShowDialogue(condition); break;
            case Utils.UiHelper.UiType.ExorcismBar: ShowExorcism(condition); break;
            case Utils.UiHelper.UiType.HidingOverlay: ShowHidingHud(condition); break;
            case Utils.UiHelper.UiType.MindMap: ShowMindMap(condition); break;
            case Utils.UiHelper.UiType.Default: ShowDefaultHud(condition); break;
        }
    }

    private void HideAll()
    {
        _itemHud.EnableCanvas(false);
        _timeslotHud.EnableCanvas(false);
        //_hidingManager.gameObject.SetActive(false);
        _dialogueManager.ShowDialogueBox(false);
        _exorcismBar.EnableCanvas(false);
        _mindMapCanvas.EnableCanvas(false);
    }

    private void ShowDefaultHud(bool isShown)
    {
       _itemHud.EnableCanvas(isShown);
       _timeslotHud.EnableCanvas(isShown);
    }

    private void ShowDialogue(bool isShown)
    {
        //Debug.Log("[HUD SYSTEM] Set dialogue box visibility to " + isShowDialogue);
        _dialogueManager.ShowDialogueBox(isShown);
    }

    private void ShowHidingHud(bool isHiding)
    {
        _hidingManager.StartAnim(isHiding);
    }

    private void ShowExorcism(bool isShown)
    {
        _exorcismBar.ShowBar(isShown);
    }

    private void ShowMindMap(bool isShown)
    {
        _mindMapCanvas.EnableCanvas(isShown);
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
        _mindMapCanvas = GetComponentInChildren<MindMapCanvas>();
        _mindMapModal = GetComponentInChildren<MindMapModal>();
        _canvas = GetComponent<Canvas>();
    }
    private void OnEnable()
    {
        GameManager.HudEvent += SetHudState;
        PlayerHidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.InventoryHudEvent += HandleInventoryEvent;
        TimeslotStateMachine.UpdateTimeslotHudEvent += UpdateTimeslotDisplay;
        MindMapTree.ActivatedModal += ActivatedModal;
        MindMapTree.SetNodeInfo += SetNodeInfo;
    }

    private void OnDisable()
    {
        GameManager.HudEvent -= SetHudState;
        PlayerHidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.InventoryHudEvent -= HandleInventoryEvent;
        TimeslotStateMachine.UpdateTimeslotHudEvent -= UpdateTimeslotDisplay;
        MindMapTree.ActivatedModal -= ActivatedModal;
        MindMapTree.SetNodeInfo -= SetNodeInfo;
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

    private void ActivatedModal(bool is_active)
    {
        _mindMapModal.ActivatedModal(is_active);
    }

    private void SetNodeInfo(MindMapNode node)
    {
        _mindMapModal.SetNodeInfo(node);
    }
    #endregion
}
