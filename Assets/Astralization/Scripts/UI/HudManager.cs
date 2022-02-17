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
    private ExorcismBar _exorcismBar;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        GameManager.StartHidingHudEvent += ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        HideInputHandler.StopHidingHudEvent += ShowHidingHud;
        GameManager.ShowExorcismHudEvent += ShowExorcism;
        ExorcismBar.ExorcismUpdateSliderEvent += UpdateSlider;
        ExorcismItem.ExorcismSetMinSliderEvent += SetMinValue;
    }

    private void OnDisable()
    {
        GameManager.ShowDialogueHudEvent -= ShowDialogue;
        GameManager.StartHidingHudEvent -= ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
        HideInputHandler.StopHidingHudEvent -= ShowHidingHud;
        GameManager.ShowExorcismHudEvent -= ShowExorcism;
        ExorcismBar.ExorcismUpdateSliderEvent -= UpdateSlider;
        ExorcismItem.ExorcismSetMinSliderEvent -= SetMinValue;
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

    public void ShowExorcism(bool isShowExorcism)
    {
        _exorcismBar.ShowBar(isShowExorcism);
    }

    public void UpdateSlider(float holdTime)
    {
        _exorcismBar.SetSliderValue(holdTime);
    }

    public void SetMinValue(float minValue)
    {
        _exorcismBar.SetSliderMinValue(minValue);
    }
}
