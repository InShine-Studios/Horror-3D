using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    [Tooltip("The Player Input component")]
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        GameManager.PlayerActionMapEvent += SetPlayerActionMap;
    }

    private void OnDisable()
    {
        GameManager.PlayerActionMapEvent -= SetPlayerActionMap;
    }

    public void SetPlayerActionMap(string actionMap)
    {
        _playerInput.SwitchCurrentActionMap(actionMap);
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
}
