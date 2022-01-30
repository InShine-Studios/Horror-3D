using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _dialogueText;

    public Animator animator;

    private void Awake()
    {
        _nameText.text = "Ini Budi";
        _dialogueText.text = "Lorem ipsum dolor sit amet, consectetur adipiscing " +
            "elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " +
            "Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi " +
            "ut aliquip ex ea commodo consequat!";
    }

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
