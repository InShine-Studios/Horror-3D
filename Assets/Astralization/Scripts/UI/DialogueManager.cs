using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _dialogueText;

    public Animator animator;

    #region Enable - Disable
    private void OnEnable()
    {
        NpcController.NpcInteractionEvent += ShowDialogBox;
    }

    private void OnDisable()
    {
        NpcController.NpcInteractionEvent -= ShowDialogBox;
    }
    #endregion

    public void ShowDialogBox(bool isInteractWithNpc)
    {
        if (isInteractWithNpc)
            animator.SetBool("IsOpen", true);
        else
            animator.SetBool("IsOpen", false);
        //Debug.Log("isInteractWithNpc: " + isInteractWithNpc);
    }
}
