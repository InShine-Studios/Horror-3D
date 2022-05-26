using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Constants
    private const string IsIdle = "IsIdle";
    private const string IsWalking = "IsWalking";
    private const string IsSprinting = "IsSprinting";
    private const string IsHoldingItem = "IsHoldingItem";
    #endregion

    #region Variables
    private Animator _animator;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    #endregion

    #region AnimHandler
    public void SetMovementAnim(bool isMoving, bool isSprinting)
    {
        bool isMovingAndSprinting = isMoving & isSprinting;
        _animator.SetBool(IsIdle, !isMoving);
        _animator.SetBool(IsWalking, isMoving ^ isMovingAndSprinting);
        _animator.SetBool(IsSprinting, isMovingAndSprinting);
    }

    public void SetHoldingItemAnim(bool isHoldingItem)
    {
       _animator.SetBool(IsHoldingItem, isHoldingItem);
    }
    #endregion
}
