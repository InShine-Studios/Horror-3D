using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Ink.Runtime;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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
    #region Events
    public static event Action FinishDialogueEvent;
    public static event Action<bool> DialogueChoiceSetInputEvent;
    #endregion

    #region Variables
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
    private bool _dialogIsTyping;
    private string _currentDialogLine;

    [Space]
    [Header("Buttons")]
    [SerializeField]
    private GameObject _buttonChoiceOne;
    [SerializeField]
    private GameObject _buttonChoiceTwo;
    [SerializeField]
    private GameObject _buttonContinue;

    [Space]
    [Header("Event System")]
    [SerializeField]
    private EventSystem _eventSystem;
    #endregion

    #region SetGet
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

    #region MonoBehaviour
    private void Awake()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
    }
    #endregion

    #region Handler
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

    private void SetUpDialogue()
    {
        _dialogueText.text = string.Empty;
        _dialogueStory = new Story(_dialogueJson.text);
        ShowChoiceButton(false);
        _dialogIsTyping = false;
        _currentDialogLine = "";
    }

    private IEnumerator TypeLine()
    {
        //To type each character 1 by 1
        foreach (char c in _currentDialogLine.ToCharArray())
        {
            _dialogueText.text += c;
            yield return new WaitForSeconds(_textSpeed);
        }
        _dialogIsTyping = false;
    }

    private void TypeLineHelper()
    {
        if (_dialogIsTyping)
        {
            StopAllCoroutines();
            _dialogueText.text = _currentDialogLine;
            _dialogIsTyping = false;
        }
        else
        {
            _dialogIsTyping = true;
            _currentDialogLine = _dialogueStory.Continue();
            StartCoroutine(TypeLine());
        }
    }

    public void NextLine()
    {
        Debug.Log(_dialogueStory.canContinue);
        if (_dialogueStory.canContinue)
        {
            _dialogueText.text = string.Empty;
            TypeLineHelper();
        }
        else
        {
            StopAllCoroutines();
            FinishDialogueEvent?.Invoke();
            ShowDialogueBox(false);
        }

        if (_dialogueStory.currentChoices.Count != 0)
        {
            ShowChoices();
        }
    }
    #endregion

    #region Choice
    // Create then show the choices on the screen until one got selected
    private void ShowChoices()
    {
        Debug.Log("[DIALOGUE MANAGER] Show Choice");
        DialogueChoiceSetInputEvent?.Invoke(false);
        List<Choice> _choices = _dialogueStory.currentChoices;

        _buttonChoiceOne.GetComponentInChildren<Text>().text = _choices[0].text;
        _buttonChoiceTwo.GetComponentInChildren<Text>().text = _choices[1].text;

        ShowChoiceButton(true);
    }

    public void ChoiceOnePressed()
    {
        Debug.Log("[DIALOGUE MANAGER] Choice 1 pressed");
        HideChoiceAndNextLine(0);
    }

    public void ChoiceTwoPressed()
    {
        Debug.Log("[DIALOGUE MANAGER] Choice 2 pressed");
        HideChoiceAndNextLine(1);
    }

    private void HideChoiceAndNextLine(int _index)
    {
        _dialogueStory.ChooseChoiceIndex(_index);
        ShowChoiceButton(false);

        DialogueChoiceSetInputEvent?.Invoke(true);
        NextLine();
    }

    private void ShowChoiceButton(bool isAskingChoice)
    {
        SetActiveButton(_buttonChoiceOne, isAskingChoice, ChoiceOnePressed);
        SetActiveButton(_buttonChoiceTwo, isAskingChoice, ChoiceTwoPressed);
        SetActiveButton(_buttonContinue, !isAskingChoice, NextLine);
        if (!isAskingChoice)
        {
            _eventSystem.SetSelectedGameObject(_buttonContinue);
        }
    }
    #endregion

    #region Buttons
    private void SetActiveButton(GameObject button, bool isActive, UnityAction handler)
    {
        button.SetActive(isActive);
        if (isActive)
        {
            button.GetComponent<Button>().onClick.AddListener(handler);
        }
        else
        {
            button.GetComponent<Button>().onClick.RemoveAllListeners();
        }
    }
    #endregion
}
