using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public interface IDialogueManager
{
    Animator GetAnimator();
    bool GetDialogBox();
    int GetIndex();
    void ShowDialogueBox(bool isInteractWithNpc);
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

    public string[] lines;
    private int _index;
    
    [SerializeField]
    private float _textSpeed;

    private bool _dialogBoxOpen;
    private string _playerActionMap = "Player";
    #endregion


    public static event Action<string> FinishDialogueEvent;

    private void Awake()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
        
    }

    #region Enable - Disable
    private void OnEnable()
    {
        HudManager.StartDialogueManagerEvent += ShowDialogueBox;
        HudManager.NextDialogueManagerEvent += NextLine;
    }

    private void OnDisable()
    {
        HudManager.StartDialogueManagerEvent -= ShowDialogueBox;
        HudManager.NextDialogueManagerEvent -= NextLine;
    }
    #endregion

    #region Getter
    public Animator GetAnimator()
    {
        return _animator;
    }

    public bool GetDialogBox()
    {
        return _dialogBoxOpen;
    }

    public int GetIndex()
    {
        return _index;
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
        _index = 0;
        _dialogueText.text = string.Empty;
    }
    #endregion

    #region TypeLine
    IEnumerator TypeLine(int idx)
    {
        //To type each character 1 by 1
        foreach (char c in lines[idx].ToCharArray())
        {
            _dialogueText.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
    }
    #endregion

    #region Next
    void NextLine()
    {
        if (_index < lines.Length)
        {
            _dialogueText.text = string.Empty;
            StartCoroutine(TypeLine(_index));
            _index++;
        }
        else
        {
            StopAllCoroutines();
            FinishDialogueEvent?.Invoke(_playerActionMap);
            ShowDialogueBox(false);
        }
    }
    #endregion
}
