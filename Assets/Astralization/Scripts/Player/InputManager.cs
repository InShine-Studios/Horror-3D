using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to manage general input.
 * All undirect input flow should be managed by this class.
 */
public class InputManager : StateMachine
{

    [Tooltip("The Player Input component")]
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        ChangeState<MovementState>();
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
        // TODO ChangeState<MovementState>();
        _playerInput.SwitchCurrentActionMap(actionMap);
        //Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
}
