using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UiInputHandler : MonoBehaviour
{
    #region Variable
    public GameObject UiCanvas;
    private GraphicRaycaster _uiRaycaster;

    private PointerEventData _clickData;
    private List<RaycastResult> _clickResults;

    public static event Action DialogueChoiceOneHudEvent;
    public static event Action DialogueChoiceTwoHudEvent;
    #endregion

    void Start()
    {
        _uiRaycaster = UiCanvas.GetComponent<GraphicRaycaster>();
        _clickData = new PointerEventData(EventSystem.current);
        _clickResults = new List<RaycastResult>();
    }

    void Update()
    {
        // use isPressed if you wish to ray cast every frame:
        //if(Mouse.current.leftButton.isPressed)

        // use wasReleasedThisFrame if you wish to ray cast just once per click:
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            CheckUiElementsClicked();
        }
    }

    void CheckUiElementsClicked()
    {
        /** Get all the UI elements clicked, using the current mouse position and raycasting. **/

        _clickData.position = Mouse.current.position.ReadValue();
        _clickResults.Clear();

        _uiRaycaster.Raycast(_clickData, _clickResults);

        foreach (RaycastResult _result in _clickResults)
        {
            GameObject _uiElement = _result.gameObject;
            switch (_uiElement.name)
            {
                case "Choice1": DialogueChoiceOneHudEvent?.Invoke(); break;
                case "Choice2": DialogueChoiceTwoHudEvent?.Invoke(); break;
            }
        }
    }
}