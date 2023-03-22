using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterNavigation : MonoBehaviour
{
    #region Event
    public static event Action<int> ChangeChapterEvent;

    #endregion

    #region Constants
    private const string BACK_BUTTON_NAME = "LeftArrow";
    private const string FORWARD_BUTTON_NAME = "RightArrow";
    #endregion

    #region Variables
    private Button _backButton;
    private Button _forwardButton;
    private Text _text;
    private int _currentChapter = 1;
    #endregion

    #region SetGet

    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _text = GetComponentInChildren<Text>();
        _backButton = transform.Find(BACK_BUTTON_NAME).GetComponent<Button>();
        _forwardButton = transform.Find(FORWARD_BUTTON_NAME).GetComponent<Button>();
        SetUp();
    }
    #endregion

    #region SetUpTeardown
    public void SetUp()
    {
        _backButton.onClick.AddListener(ChangeChapterBackward);
        _forwardButton.onClick.AddListener(ChangeChapterForward);
    }

    public void TearDown()
    {
        _backButton.onClick.RemoveAllListeners();
        _forwardButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region EventInvoker
    private void InvokeChangeChapterEvent(int jumpIdx)
    {
        ChangeChapterEvent?.Invoke(jumpIdx);

        _currentChapter = Mathf.Clamp(_currentChapter + jumpIdx, 1, 3);
        _text.text = "Chapter " + _currentChapter;
    }

    public void ChangeChapterForward()
    {
        InvokeChangeChapterEvent(1);
    }

    public void ChangeChapterBackward()
    {
        InvokeChangeChapterEvent(-1);
    }
    #endregion
}
