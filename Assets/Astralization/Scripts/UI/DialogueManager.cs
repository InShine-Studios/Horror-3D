using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Ink.Runtime;

public interface IDialogueManager
{
    void SetDialogJson(TextAsset newDialogueJson);
    Animator GetAnimator();
    bool IsDialogBoxOpen();
    void ShowDialogueBox(bool isInteractWithNpc);
    Story GetDialogStory();
}

/*
 * Class to manage dialogues.
 * Use Setter and Getter to access the variables.
 * Is subscribed to NpcController to change action map, and DialogueInvoker to continue the dialogue.
 */
public class DialogueManager : MonoBehaviour, IDialogueManager
{
    #region Variable
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _dialogueText;
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private TextAsset _dialogueJson;
    private Story _dialogueStory;
    
    [SerializeField]
    private float _textSpeed;

    private bool _dialogBoxOpen;
    private string _defaultActionMap = "Default";
    #endregion


    public static event Action<string> FinishDialogueEvent;

    private void Awake()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
        
    }

    #region Setter Getter
    public void SetDialogJson(TextAsset newDialogueJson)
    {
        _dialogueJson = newDialogueJson;
    }

    public Animator GetAnimator()
    {
        return _animator;
    }

    public bool IsDialogBoxOpen()
    {
        return _dialogBoxOpen;
    }

    public Story GetDialogStory()
    {
        return _dialogueStory;
    }
    #endregion

    #region Dialogue Setup/Show
    public void ShowDialogueBox(bool isShowDialogue)
    {
        if (isShowDialogue)
        {
            SetUpDialogue();
            _animator.SetBool("IsOpen", true);
            _dialogBoxOpen = true;
            NextLine();
        }
        else
        {
            _animator.SetBool("IsOpen", false);
            _dialogBoxOpen = false;
        }
    }

    void SetUpDialogue()
    {
        _dialogueText.text = string.Empty;
        _dialogueStory = new Story(_dialogueJson.text);
    }
    #endregion

    #region TypeLine
    IEnumerator TypeLine()
    {
        //To type each character 1 by 1
        foreach (char c in _dialogueStory.Continue().ToCharArray())
        {
            _dialogueText.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    #endregion

    #region Next
    public void NextLine()
    {
        if (_dialogueStory.canContinue)
        {
            _dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StopAllCoroutines();
            FinishDialogueEvent?.Invoke(_defaultActionMap);
            ShowDialogueBox(false);
        }
    }
    #endregion
}
