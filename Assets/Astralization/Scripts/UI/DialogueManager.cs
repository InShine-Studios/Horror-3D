using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public interface IDialogueManager
{
    Animator GetAnimator();
    bool GetDialogBox();
    int GetIndex();
    void ShowDialogBox(bool isInteractWithNpc);
}

public class DialogueManager : MonoBehaviour, IDialogueManager
{
    [SerializeField]
    private Text _nameText;
    [SerializeField]
    private Text _dialogueText;
    [SerializeField]
    private Animator _animator;

    public string[] lines;
    private int _index;
    public float TextSpeed;

    private PlayerInput _playerInput;
    private InputAction _next;
    private bool _dialogBoxOpen;

    public static event Action<bool> FinishDialogue;

    private void Awake()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
        
    }

    #region Enable - Disable
    private void OnEnable()
    {
        NpcController.NpcInteractionEvent += ShowDialogBox;
        DialogueInvoker.StartDialogue += Next;
    }

    private void OnDisable()
    {
        NpcController.NpcInteractionEvent -= ShowDialogBox;
        DialogueInvoker.StartDialogue -= Next;
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

    public void Next()
    {
        if (_dialogueText.text == lines[_index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            _dialogueText.text = lines[_index];
        }
    }

    public void ShowDialogBox(bool isInteractWithNpc)
    {
        if (isInteractWithNpc)
        {
            SetUpDialogue();
            _animator.SetBool("IsOpen", true);
            _dialogBoxOpen = true;
        }
        else
        {
            _animator.SetBool("IsOpen", false);
            _dialogBoxOpen = false;
            //Debug.Log("isInteractWithNpc: " + isInteractWithNpc);
        }
    }

    void SetUpDialogue()
    {
        _index = 0;
        _dialogueText.text = string.Empty;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        //To type each character 1 by 1
        foreach (char c in lines[_index].ToCharArray())
        {
            _dialogueText.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }

    void NextLine()
    {
        if (_index < lines.Length - 1)
        {
            _index++;
            _dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            FinishDialogue?.Invoke(false);
            ShowDialogBox(false);
        }
    }
}
