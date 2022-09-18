using UnityEngine;
using Cinemachine;

/*
 * Class for process camera closets POV
 */
public class CinemachinePOVExtension : CinemachineExtension
{
    #region Variables
    private HidingCameraConfigs _hidingCameraConfigs;
    private Vector3 _currentDirection;
    private Vector2 _deltaInput;
    private float _horizontalSpeed = 15f;
    private float _verticalSpeed = 15f;
    #endregion

    #region SetGet
    public void SetMouseDeltaInput(Vector2 deltaInput)
    {
        _deltaInput = deltaInput;
    }

    public void SetClosetCameraSetting(HidingCameraConfigs hidingCameraConfigs)
    {
        _hidingCameraConfigs = hidingCameraConfigs;
        _currentDirection = _hidingCameraConfigs.StartingDirection;
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
                _currentDirection.x += _deltaInput.x * _verticalSpeed * Time.deltaTime;
                _currentDirection.y += _deltaInput.y * _horizontalSpeed * Time.deltaTime;
                _currentDirection.x = Mathf.Clamp(_currentDirection.x, _hidingCameraConfigs.ClampDownUp.x, _hidingCameraConfigs.ClampDownUp.y);
                _currentDirection.y = Mathf.Clamp(_currentDirection.y, _hidingCameraConfigs.ClampLeftRight.x, _hidingCameraConfigs.ClampLeftRight.y);
                state.RawOrientation = Quaternion.Euler(-_currentDirection.y, _currentDirection.x, 0f);
            }
        }
    }
    #endregion
}
