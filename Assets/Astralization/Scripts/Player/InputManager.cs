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
        Debug.Log("[INPUT MAP] actionMap: " + actionMap);
        if (actionMap.Equals("Player"))
        {

            ChangeState<MovementState>();
        }
        else if (actionMap.Equals("Dialogue"))
        {
            ChangeState<DialogueState>();
        }
        _playerInput.SwitchCurrentActionMap(actionMap);
        Debug.Log("[INPUT MAP] New Map: " + _playerInput.currentActionMap);
    }
}
