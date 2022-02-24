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
        GameManager.HudMapEvent += SetActiveHud;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        HideInputHandler.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
    }

    private void OnDisable()
    {
        GameManager.HudMapEvent -= SetActiveHud;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
        HideInputHandler.StopHidingHudEvent -= ShowHidingHud;
        Inventory.ItemLogoEvent -= UpdateLogo;
    }
    #endregion

    public void SetActiveHud(string hudMap, bool state)
    {
        switch (hudMap) // RACE CONDITION
        {
            case "Exorcism": ShowExorcism(state); break;
            case "Hiding": ShowHidingHud(state); break;
            case "Dialogue": ShowDialogue(state); break;
        }
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
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
