using UnityEngine;
using System;

/*
 * Class to manage general HUD stuff.
 * All class related to HUD should be managed by this class.
 */
public class HudManager : MonoBehaviour
{
    #region Variables
    public static event Action<bool> StartDialogueManagerEvent;
    public static event Action NextDialogueManagerEvent;
    #endregion

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ShowDialogueHudEvent += ShowDialogue;
        DialogueInputHandler.NextDialogueHudEvent += NextDialogue;
    }

    private void OnDisable()
    {
        GameManager.ShowDialogueHudEvent -= ShowDialogue;
        DialogueInputHandler.NextDialogueHudEvent -= NextDialogue;
    }
    #endregion

    public void ShowDialogue(bool isShowDialogue)
    {
        //Debug.Log("[START DIALOGUE HUD] isShowDialogue: " + isShowDialogue);
        StartDialogueManagerEvent?.Invoke(isShowDialogue);
    }

    public void NextDialogue()
    {
        //Debug.Log("[NEXT DIALOGUE HUD]");  
        NextDialogueManagerEvent?.Invoke();
    }
}
