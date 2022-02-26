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
    #endregion

    #region MonoBehaviour
    private void OnEnable()
    {
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        GameManager.StartHidingHudEvent += ShowHidingHud;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
        HideInputHandler.StopHidingHudEvent += ShowHidingHud;
        Inventory.ItemLogoEvent += UpdateLogo;
        GameManager.ShowExorcismHudEvent += ShowExorcism;
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
}
