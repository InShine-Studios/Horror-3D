using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeNavigation : MonoBehaviour
{
    #region Event
    public static event Action<int> ChangeNodeEvent;

    #endregion

    #region Constants
    private const string BACK_BUTTON_NAME = "LeftArrow";
    private const string FORWARD_BUTTON_NAME = "RightArrow";
    #endregion

    #region Variables
    private Button _backButton;
    private Button _forwardButton;
    #endregion

    #region SetGet

    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _backButton = transform.Find(BACK_BUTTON_NAME).GetComponent<Button>();
        _forwardButton = transform.Find(FORWARD_BUTTON_NAME).GetComponent<Button>();
        SetUp();
    }
    #endregion

    #region SetUpTeardown
    public void SetUp()
    {
        _backButton.onClick.AddListener(ChangeNodeBackward);
        _forwardButton.onClick.AddListener(ChangeNodeForward);
    }

    public void TearDown()
    {
        _backButton.onClick.RemoveAllListeners();
        _forwardButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region EventInvoker
    private void InvokeChangeNodeEvent(int jumpIdx)
    {
        ChangeNodeEvent?.Invoke(jumpIdx);
    }

    public void ChangeNodeForward()
    {
        InvokeChangeNodeEvent(1);
    }

    public void ChangeNodeBackward()
    {
        InvokeChangeNodeEvent(-1);
    }
    #endregion
}
