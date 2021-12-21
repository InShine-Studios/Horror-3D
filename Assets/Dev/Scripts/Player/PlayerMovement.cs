using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    #region Movement Values
    private CharacterController controller;
    private PlayerBase playerBase;
    private float movementSpeed;

    private Vector2 movementInput = new Vector2(0, 0);
    private Vector3 moveDirection;
    #endregion

    private void Awake()
    {
        playerBase = GetComponent<PlayerBase>();
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        movementSpeed = playerBase.GetPlayerMovementSpeed();
    }

    private void Update()
    {
        MovePlayer();
    }

    public Vector3 GetMoveDirection()
    {
        return moveDirection;
    }

    #region Input System
    // Read movement input and set move direction
    public void OnMovementInput(InputAction.CallbackContext inputVal)
    {
        movementInput = inputVal.ReadValue<Vector2>();
        moveDirection = new Vector3(0, 0) { x = movementInput.x, z = movementInput.y };
        //Debug.Log("[PLAYER] Movement direction: " + moveDirection);
    }
    #endregion

    private void MovePlayer()
    {
        controller.SimpleMove(movementSpeed * Time.deltaTime * moveDirection);
    }
}
