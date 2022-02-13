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
    private ExorcismBar _exorcismBar;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        GameManager.ShowExorcismHudEvent += ShowExorcism;
    }

    private void OnDisable()
    {
        GameManager.ShowDialogueHudEvent -= ShowDialogue;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
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

    public void ShowExorcism(bool isShowExorcism)
    {
        //Debug.Log("[START EXORCISM HUD] isShowExorcism: " + isShowExorcism);
        _exorcismBar.ShowBar(isShowExorcism);
    }
}
