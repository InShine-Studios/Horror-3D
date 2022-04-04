using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostIdleState : GhostState
{
    #region Variables
    [SerializeField]
    [Tooltip("True if ghost can rotate")]
    private bool _enableRotate;
    [SerializeField]
    [Tooltip("Rotation Speed of ghost")]
    private float _rotateSpeed = 0.025f;
    private Quaternion targetRotation;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        targetRotation = transform.rotation;
        debugMaterial = Resources.Load("EvidenceItem/MAT_Thermometer_Base", typeof(Material)) as Material;
    }

    protected void Update()
    {
        if (_enableRotate)
        {
            RotateGhost();
        }
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        _enableRotate = true;
        float randomDelay = Utils.Randomizer.GetFloat(2f, 5f);
        ChangeToWanderInSeconds(randomDelay);
    }

    public override void Exit()
    {
        base.Exit();
        _enableRotate = false;
    }
    #endregion

    #region MovementHelper
    private void RotateGhost()
    {
        Quaternion delta = Utils.GeometryCalcu.GetAngleDelta(transform.rotation, targetRotation);
        if (delta.eulerAngles.y <= 5f)
        {
            targetRotation = Quaternion.Euler(0f, Utils.Randomizer.GetFloat(-180f, 180f), 0f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotateSpeed);
    }
    #endregion

    #region StateTransitionHelper
    private void ChangeToWanderInSeconds(float delay)
    {
        StartCoroutine(
            Utils.DelayerHelper.Delay(delay, () => owner.ChangeState<GhostChasingState>())
        );
    }
    #endregion
}
