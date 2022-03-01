﻿using UnityEngine;
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

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.HudEvent += SetHudState;
        DialogueState.NextDialogueHudEvent += NextDialogue;
        HidingState.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
    }

    private void OnDisable()
    {
        GameManager.HudEvent -= SetHudState;
        DialogueState.NextDialogueHudEvent -= NextDialogue;
        HidingState.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
    }
    #endregion

    public void SetHudState(Utils.PlayerHelper.States hudKey, bool condition)
    {
        switch (hudKey)
        {
            case Utils.PlayerHelper.States.Exorcism: ShowExorcism(condition); break;
            case Utils.PlayerHelper.States.Hiding: ShowHidingHud(condition); break;
            case Utils.PlayerHelper.States.Dialogue: ShowDialogue(condition); break;
        }
    }

    #region HUDfunction
    public void ShowDialogue(bool isShowDialogue)
    {
        //Debug.Log("[START DIALOGUE HUD] isShowDialogue: " + isShowDialogue);
        _dialogueManager.ShowDialogueBox(isShowDialogue);
    }

    public void NextDialogue()
    {
        //Debug.Log("[NEXT DIALOGUE HUD]");
        _dialogueManager.NextLine();
    }

    public void ShowHidingHud(bool isHiding)
    {
        _hidingManager.StartAnim(isHiding);
    }

    public void UpdateLogo(bool state, Sprite logo)
    {
        _itemLogo.UpdateLogo(state, logo);
    }

    public void ShowExorcism(bool isShowExorcism)
    {
        _exorcismBar.SetExorcismBar(isShowExorcism);
    }
    #endregion
}
