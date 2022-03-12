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
    void ShowDialogueBox(bool isShowDialogue);
    Story GetDialogStory();
}

/*
 * Class to manage dialogues.
 * Use Setter and Getter to access the variables.
 * Is subscribed to VictimController to change action map, and DialogueInvoker to continue the dialogue.
 */
public class DialogueManager : MonoBehaviour, IDialogueManager
{
    #region Events
    public static event Action FinishDialogueEvent;
    #endregion

    #region Variables
    private Text _nameText;
    private Text _dialogueText;
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
    private GameObject _buttonChoiceOne;
    private GameObject _buttonChoiceTwo;
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
    private void Start()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
    }

    private void Awake()
    {
        _buttonChoiceOne = transform.Find("Choice1").gameObject;
        _buttonChoiceTwo = transform.Find("Choice2").gameObject;
        _buttonContinue = transform.Find("Continue").gameObject;
        _nameText = transform.Find("Name").GetComponent<Text>();
        _dialogueText = transform.Find("Dialogue").GetComponent<Text>();
        _animator = transform.GetComponent<Animator>();
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
            TearDownDialogue();
            _animator.SetBool("IsOpen", false);
            _dialogBoxOpen = false;
        }
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

    #region SetUpTeardown
    private void SetUpDialogue()
    {
        _dialogueText.text = string.Empty;
        _dialogueStory = new Story(_dialogueJson.text);
        _dialogIsTyping = false;
        _currentDialogLine = "";
        ShowChoiceButton(false);

        _buttonChoiceOne.GetComponent<Button>().onClick.AddListener(ChoiceOnePressed);
        _buttonChoiceTwo.GetComponent<Button>().onClick.AddListener(ChoiceTwoPressed);
        _buttonContinue.GetComponent<Button>().onClick.AddListener(NextLine);
    }

    private void TearDownDialogue()
    {
        _buttonChoiceOne.GetComponent<Button>().onClick.RemoveAllListeners();
        _buttonChoiceTwo.GetComponent<Button>().onClick.RemoveAllListeners();
        _buttonContinue.GetComponent<Button>().onClick.RemoveAllListeners();
    }
    #endregion

    #region Choice
    // Create then show the choices on the screen until one got selected
    private void ShowChoices()
    {
        //Debug.Log("[DIALOGUE MANAGER] Show Choice");
        List<Choice> _choices = _dialogueStory.currentChoices;

        _buttonChoiceOne.GetComponentInChildren<Text>().text = _choices[0].text;
        _buttonChoiceTwo.GetComponentInChildren<Text>().text = _choices[1].text;

        ShowChoiceButton(true);
    }

    public void ChoiceOnePressed()
    {
        //Debug.Log("[DIALOGUE MANAGER] Choice 1 pressed");
        HideChoiceAndNextLine(0);
    }

    public void ChoiceTwoPressed()
    {
        //Debug.Log("[DIALOGUE MANAGER] Choice 2 pressed");
        HideChoiceAndNextLine(1);
    }

    private void HideChoiceAndNextLine(int _index)
    {
        _dialogueStory.ChooseChoiceIndex(_index);
        ShowChoiceButton(false);
        NextLine();
    }

    private void ShowChoiceButton(bool isAskingChoice)
    {
        _buttonChoiceOne.SetActive(isAskingChoice);
        _buttonChoiceTwo.SetActive(isAskingChoice);
        _buttonContinue.SetActive(!isAskingChoice);
        if (!isAskingChoice)
        {
            _eventSystem.SetSelectedGameObject(_buttonContinue);
        }
    }
    #endregion
}
