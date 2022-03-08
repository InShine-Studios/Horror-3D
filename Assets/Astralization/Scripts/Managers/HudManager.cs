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
    private ItemLogo _itemLogo;
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
            case Utils.PlayerHelper.States.Dialogue: ShowDialogue(condition); break;
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
        _itemLogo.UpdateLogo(state, logo);
    }

    private void ShowExorcism(bool isShowExorcism)
    {
        _exorcismBar.ShowBar(isShowExorcism);
    }
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        GameManager.HudEvent += SetHudState;
        HidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
    }

    private void OnDisable()
    {
        GameManager.HudEvent -= SetHudState;
        HidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
    }
    #endregion
}
