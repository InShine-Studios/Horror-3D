using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using Ink.Runtime;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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
    private string _choiceOne = "Choice1";
    private string _choiceTwo = "Choice2";
    private string _continue = "Continue";
    #endregion

    public static event Action<string> FinishDialogueEvent;
    public static event Action<bool> DialogueChoiceSetInputEvent;

    [SerializeField]
    private GameObject _uiCanvas;
    private GraphicRaycaster _uiRaycaster;

    private PointerEventData _clickData;
    private List<RaycastResult> _clickResults;

    #region Awake
    private void Awake()
    {
        _nameText.text = "Budi";
        _dialogueText.text = string.Empty;
        _uiRaycaster = _uiCanvas.GetComponent<GraphicRaycaster>();
        _clickData = new PointerEventData(EventSystem.current);
        _clickResults = new List<RaycastResult>();
    }
    #endregion

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

        GameObject _buttonChoiceOne = transform.Find(_choiceOne).gameObject;
        GameObject _buttonChoiceTwo = transform.Find(_choiceTwo).gameObject;
        GameObject _buttonContinue = transform.Find(_continue).gameObject;

        _buttonChoiceOne.GetComponentInChildren<Text>().text = _choices[0].text;
        _buttonChoiceTwo.GetComponentInChildren<Text>().text = _choices[1].text;

        _buttonChoiceOne.SetActive(true);
        _buttonChoiceTwo.SetActive(true);
        _buttonContinue.SetActive(false);
    }

    public void ChoiceOnePressed()
    {
        Debug.Log("[DIALOGUE MANAGER] Choice 1 pressed");
        HideChoiceAndNextLine(0);
    }

    public void ChoicetTwoPressed()
    {
        Debug.Log("[DIALOGUE MANAGER] Choice 2 pressed");
        HideChoiceAndNextLine(1);
    }

    private void HideChoiceAndNextLine(int _index)
    {
        _dialogueStory.ChooseChoiceIndex(_index);
        transform.Find(_choiceOne).gameObject.SetActive(false);
        transform.Find(_choiceTwo).gameObject.SetActive(false);
        transform.Find(_continue).gameObject.SetActive(true);

        DialogueChoiceSetInputEvent?.Invoke(true);
        NextLine();
    }

    #endregion

    public void DialogueClickTriggered(InputAction.CallbackContext ctx)
    {
        // use isPressed if you wish to ray cast every frame:
        //if(Mouse.current.leftButton.isPressed)

        // use wasReleasedThisFrame if you wish to ray cast just once per click:
        if (ctx.performed)
        {

            _clickData.position = Mouse.current.position.ReadValue();
            _clickResults.Clear();

            _uiRaycaster.Raycast(_clickData, _clickResults);

            foreach (RaycastResult _result in _clickResults)
            {
                GameObject _uiElement = _result.gameObject;
                switch (_uiElement.name)
                {
                    case "Choice1": ChoiceOnePressed(); break;
                    case "Choice2": ChoicetTwoPressed(); break;
                }
            }
        }
    }
}
