using UnityEngine;

/*
 * Class to control player animation states.
 * Takes the current movement magnitude from PlayerMovement.
 * Takes the current rotation angle from PlayerRotation.
 */
public class PlayerAnimation : MonoBehaviour
{
    #region Variables
    [Header("Player Parts")]
    [Tooltip("The Animator for this player")]
    private Animator _animator;
    [Tooltip("The movement component for the speed")]
    public PlayerMovement PlayerMovement;
    [Tooltip("The rotation component for the angle")]
    public PlayerRotation PlayerRotation;
    #endregion

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetPlayerDir();
        SetPlayerMoveAnim();
    }

    #region Set Anim
    private void SetPlayerDir()
    {
        float currentAngle = PlayerRotation.transform.localEulerAngles.y;

        if (currentAngle >= 135 && currentAngle < 225)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Down);
        }
        else if (currentAngle >= 45 && currentAngle < 135)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Right);
        }
        else if (currentAngle >= 315 || currentAngle < 45)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Up);
        }
        else if (currentAngle >= 225 && currentAngle < 315)
        {
            _animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Left);
        }
    }

    void SetPlayerMoveAnim()
    {
        _animator.SetBool("IsIdle", false);
        _animator.SetBool("IsWalking", false);
        _animator.SetBool("IsSprinting", false);

        if (PlayerMovement.GetMoveDirection().magnitude == 0)
        {
            _animator.SetBool("IsIdle", true);
        }
        else if (PlayerMovement.GetSprintBool())
        {
            _animator.SetBool("IsSprinting", true);
        }
        else
        {
            _animator.SetBool("IsWalking", true);
        }
    }
    #endregion
}
