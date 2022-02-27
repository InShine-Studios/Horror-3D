using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to accept player input and continue with the dialogue
 */
public class DialogueInputHandler : MonoBehaviour
{
    #region Events
    public static event Action NextDialogueHudEvent;
    #endregion

    #region Handler
    public void NextDialogue()
    {
        NextDialogueHudEvent?.Invoke();
    }
    #endregion
}
