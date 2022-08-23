using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    #region Variables
    private float horizontalSpeed = 15f;
    private float verticalSpeed = 15f;
    private float clampAngle = 80f;

    private Vector3 startingRotation;
    private Vector2 _deltaInput;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Handler
    public void SetMouseDeltaInput(Vector2 deltaInput)
    {
        _deltaInput = deltaInput;
    }
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (startingRotation == null) startingRotation = transform.localRotation.eulerAngles;
                startingRotation.x += _deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += _deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
    #endregion
}
