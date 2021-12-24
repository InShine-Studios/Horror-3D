using UnityEngine;

// TODO: Docs & Tooltip
public class PlayerAnimation : MonoBehaviour
{
    [Header("Animation")]
    private Animator animator;

    public PlayerMovement playerMovement;
    public PlayerRotation playerRotation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetPlayerDir();
        SetPlayerMoveAnim();
    }

    #region Set Anim
    private void SetPlayerDir()
    {
        float currentAngle = playerRotation.transform.localEulerAngles.y;

        if (currentAngle >= 135 && currentAngle < 225)
        {
            animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Down);
        }
        else if (currentAngle >= 45 && currentAngle < 135)
        {
            animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Right);
        }
        else if (currentAngle >= 315 || currentAngle < 45)
        {
            animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Up);
        }
        else if (currentAngle >= 225 && currentAngle < 315)
        {
            animator.SetInteger("Direction", (int)Utils.PlayerHelper.Direction.Left);
        }
    }

    void SetPlayerMoveAnim()
    {
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsWalking", false);
        animator.SetBool("IsSprinting", false);

        if (playerMovement.GetMoveDirection().magnitude == 0)
        {
            animator.SetBool("IsIdle", true);
        }
        //else if (isSprinting)
        //{
        //    animator.SetBool("IsSprinting", true);
        //}
        else
        {
            animator.SetBool("IsWalking", true);
        }
    }
    #endregion

    //#region Sprint
    // TODO Sprint with cooldown?
    //private void SprintPressed(InputAction.CallbackContext ctx)
    //{
    //    //Debug.Log(this.name + " started sprinting");
    //    isSprinting = true;
    //}

    //private void SprintReleased(InputAction.CallbackContext ctx)
    //{
    //    //Debug.Log(this.name + " no longer sprinting");
    //    isSprinting = false;
    //}
    //#endregion
}
