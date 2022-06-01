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

    private void UpdateLogo(int index, Sprite logo)
    {
        _itemHud.SetItemLogo(index, logo);
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
        GameManager.HudEvent += SetHudState;
        PlayerHidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
        Inventory.InitItemHudEvent += GenerateItemHud;
        Inventory.ToggleItemHudDisplayEvent += ToggleItemHudDisplay;
        Inventory.UpdateActiveItemIndexEvent += UpdateActiveItem;
    }

    private void OnDisable()
    {
        GameManager.HudEvent -= SetHudState;
        PlayerHidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
        Inventory.InitItemHudEvent -= GenerateItemHud;
        Inventory.ToggleItemHudDisplayEvent -= ToggleItemHudDisplay;
        Inventory.UpdateActiveItemIndexEvent -= UpdateActiveItem;
    }
    #endregion
}
