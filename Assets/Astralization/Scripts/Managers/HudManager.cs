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
    public void SetHudState(Utils.PlayerHelper.States hudKey, bool condition)
    {
        switch (hudKey)
        {
            case Utils.PlayerHelper.States.Exorcism: ShowExorcism(condition); break;
            case Utils.PlayerHelper.States.Hiding: ShowHidingHud(condition); break;
            case Utils.PlayerHelper.States.Dialogue: ShowDialogue(condition); break;
        }
    }

    public void ShowDialogue(bool isShowDialogue)
    {
        //Debug.Log("[HUD SYSTEM] Set dialogue box visibility to " + isShowDialogue);
        _dialogueManager.ShowDialogueBox(isShowDialogue);
    }

    public void NextDialogue()
    {
        //Debug.Log("[HUD SYSTEM] Dialogue Next Line");
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
    #endregion

    #region MonoBehaviour
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
}
