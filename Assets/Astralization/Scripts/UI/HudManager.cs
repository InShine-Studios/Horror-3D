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
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        GameManager.StartHidingHudEvent += ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        HideInputHandler.StopHidingHudEvent += ShowHidingHud;
    }

    private void OnDisable()
    {
        GameManager.ShowDialogueHudEvent -= ShowDialogue;
        GameManager.StartHidingHudEvent -= ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
        HideInputHandler.StopHidingHudEvent -= ShowHidingHud;
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
}
