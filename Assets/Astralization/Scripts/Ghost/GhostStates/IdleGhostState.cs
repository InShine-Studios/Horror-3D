using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleGhostState : GhostState
{
    #region Variables
    [SerializeField]
    [Tooltip("True if ghost can rotate")]
    private bool _enableRotate;
    [SerializeField]
    [Tooltip("Rotation Speed of ghost")]
    private float _rotateSpeed = 0.1f;
    private Quaternion targetRotation;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        targetRotation = transform.rotation;
    }

    protected void Update()
    {
        if (_enableRotate)
        {
            if (targetRotation == transform.rotation)
            {
                targetRotation = Quaternion.Euler(0f, Utils.Randomizer.GetFloat(-180f, 180f), 0f);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.time * _rotateSpeed);
        }
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        _enableRotate = true;
    }

    public override void Exit()
    {
        base.Exit();
        _enableRotate = false;
    }

    public override void ChangeToWanderInSeconds(float delay)
    {
        Utils.DelayerHelper.Delay(delay, () => owner.ChangeState<WanderGhostState>());
    }
    #endregion
}
