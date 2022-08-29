using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtension : CinemachineExtension
{
    #region Variables
    [SerializeField]
    private float _horizontalSpeed = 15f;
    private float _verticalSpeed = 15f;
    private Vector2 _clampAngleX;
    private Vector2 _clampAngleY;
    private Vector3 _startingDirection;
    private Vector2 _deltaInput;
    #endregion

    #region SetGet
    public void SetMouseDeltaInput(Vector2 deltaInput)
    {
        _deltaInput = deltaInput;
    }

    public void SetHorizontalSpeed(float horizontalSpeed)
    {
        _horizontalSpeed = horizontalSpeed;
    }

    public void SetVerticalSpeed(float verticalSpeed)
    {
        _verticalSpeed = verticalSpeed;
    }

    public void SetClampAngleX(Vector2 clampAngleX)
    {
        _clampAngleX = clampAngleX;
    }

    public void SetClampAngleY(Vector2 clampAngleY)
    {
        _clampAngleY = clampAngleY;
    }

    public void SetStartingDirection(Vector3 startingDirection)
    {
        _startingDirection = startingDirection;
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Handler
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                _startingDirection.x += _deltaInput.x * _verticalSpeed * Time.deltaTime;
                _startingDirection.y += _deltaInput.y * _horizontalSpeed * Time.deltaTime;
                _startingDirection.x = Mathf.Clamp(_startingDirection.x, _clampAngleX.x, _clampAngleX.y);
                _startingDirection.y = Mathf.Clamp(_startingDirection.y, _clampAngleY.x, _clampAngleY.y);
                state.RawOrientation = Quaternion.Euler(-_startingDirection.y, _startingDirection.x, 0f);
            }
        }
    }
    #endregion
}
