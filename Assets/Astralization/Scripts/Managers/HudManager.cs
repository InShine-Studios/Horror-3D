using UnityEngine;
using System;

/*
 * Class to manage general HUD stuff.
 * All class related to HUD should be managed by this class.
 */
public class HudManager : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private DialogueManager _dialogueManager;
    [SerializeField]
    private HidingOverlay _hidingManager;
    [SerializeField]
    private ItemHudDisplay _itemHud;
    [SerializeField]
    private ExorcismBar _exorcismBar;
    #endregion

    #region SetGet
    private void SetHudState(Utils.PlayerHelper.States hudKey, bool condition)
    {
        switch (hudKey)
        {
            case Utils.PlayerHelper.States.Exorcism: ShowExorcism(condition); break;
            case Utils.PlayerHelper.States.Hiding: ShowHidingHud(condition); break;
        }
    }

    private void SetHudState(Utils.UiHelper.States hudKey, bool condition)
    {
        switch (hudKey)
        {
            case Utils.UiHelper.States.Dialogue: ShowDialogue(condition); break;
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

    private void UpdateLogo(bool state, Sprite logo)
    {
        //TODO
    }

    private void ShowExorcism(bool isShowExorcism)
    {
        _exorcismBar.ShowBar(isShowExorcism);
    }

    private void GenerateItemHud(int inventoryLength, int activeIdx)
    {
        _itemHud.Init(inventoryLength, activeIdx);
    }

    private void ToggleItemHudDisplay()
    {
        _itemHud.ToggleDisplay();
    }

    private void UpdateActiveItem(int activeIdx)
    {
        _itemHud.SelectActiveSlot(activeIdx);
    }
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        GameManager.HudPlayerEvent += SetHudState;
        GameManager.HudUiEvent += SetHudState;
        HidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
        Inventory.InitItemHudEvent += GenerateItemHud;
        Inventory.ToggleItemHudDisplayEvent += ToggleItemHudDisplay;
        Inventory.UpdateActiveItemIndexEvent += UpdateActiveItem;
    }

    private void OnDisable()
    {
        GameManager.HudPlayerEvent -= SetHudState;
        GameManager.HudUiEvent -= SetHudState;
        HidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
        Inventory.InitItemHudEvent -= GenerateItemHud;
        Inventory.ToggleItemHudDisplayEvent -= ToggleItemHudDisplay;
        Inventory.UpdateActiveItemIndexEvent += UpdateActiveItem;
    }
    #endregion
}
