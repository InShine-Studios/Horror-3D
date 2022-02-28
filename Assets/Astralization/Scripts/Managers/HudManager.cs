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
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        GameManager.StartHidingHudEvent += ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        HideInputHandler.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
        GameManager.ShowExorcismHudEvent += ShowExorcism;
        UiInputHandler.DialogueChoiceOneHudEvent += DialogueChoiceOnePressed;
        UiInputHandler.DialogueChoiceTwoHudEvent += DialogueChoiceTwoPressed;
    }

    private void OnDisable()
    {
        GameManager.ShowDialogueHudEvent -= ShowDialogue;
        GameManager.StartHidingHudEvent -= ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
        HideInputHandler.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
        GameManager.ShowExorcismHudEvent -= ShowExorcism;
    }
    #endregion

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
        _exorcismBar.ShowBar(isShowExorcism);
    }

    public void DialogueChoiceOnePressed()
    {
        //Debug.Log("[CHOICE 1 DIALOGUE HUD]");
        _dialogueManager.ChoiceOnePressed();
    }

    public void DialogueChoiceTwoPressed()
    {
        //Debug.Log("[CHOICE 2 DIALOGUE HUD]");
        _dialogueManager.ChoicetTwoPressed();
    }
}
