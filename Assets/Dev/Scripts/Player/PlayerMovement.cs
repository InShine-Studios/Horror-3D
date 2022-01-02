using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Class to control the player movement.
 * Using Input Actions.
 */
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerBase))]
public class PlayerMovement : MonoBehaviour
{
    #region Movement Variables
    [Header("External Variables")]
    [Tooltip("The Controller component")]
    private CharacterController controller;
    [Tooltip("The PlayerBase for constants")]
    private PlayerBase playerBase;

    [Space][Header("Movement Constants")]
    [Tooltip("The movement speed used")]
    private float movementSpeed;
    [Tooltip("The movement input from input actions")]
    private Vector2 movementInput = new Vector2(0, 0);
    [Tooltip("The move direction generated")]
    private Vector3 moveDirection;

    [Tooltip("Sprinting boolean")]
    private bool isSprinting;
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
        if (inputVal.performed)
            movementInput = inputVal.ReadValue<Vector2>();
            moveDirection = new Vector3(0, 0) { x = movementInput.x, z = movementInput.y };
            //Debug.Log("[PLAYER] Movement direction: " + moveDirection);
    }

    //TODO Sprint with cooldown?
    public void SprintPressed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isSprinting = true;
            //Debug.Log(this.name + " started sprinting " + isSprinting);
    }

    public void SprintReleased(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
            isSprinting = false;
            //Debug.Log(this.name + " no longer sprinting " + isSprinting);
    }
    #endregion

    private void MovePlayer()
    {
        movementSpeed = playerBase.GetPlayerMovementSpeed();
        if (isSprinting)
            movementSpeed = playerBase.GetSprintSpeed();
        controller.SimpleMove(movementSpeed * Time.deltaTime * moveDirection);
    }
}
