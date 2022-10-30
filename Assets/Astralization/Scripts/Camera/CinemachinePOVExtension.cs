using UnityEngine;
using Cinemachine;
using UnityEngine.Assertions;

/*
 * Class for process camera closets POV
 */
public class CinemachinePOVExtension : CinemachineExtension
{
    #region Variables
    private HidingCameraConfigs _povCameraConfig;
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
        _povCameraConfig = hidingCameraConfigs;
        _currentDirection = _povCameraConfig.StartingDirection;
    }

    private void ResetDeltaInput()
    {
        _deltaInput = Vector2.zero;
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
    }
    #endregion

    #region Callback
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (_povCameraConfig == null)
        {
            //Debug.Log("Camera config is null");
            return;
        }

        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                _currentDirection.x += _deltaInput.x * _verticalSpeed * Time.deltaTime;
                _currentDirection.y += _deltaInput.y * _horizontalSpeed * Time.deltaTime;
                _currentDirection.x = Mathf.Clamp(_currentDirection.x, _povCameraConfig.ClampDownUp.x, _povCameraConfig.ClampDownUp.y);
                _currentDirection.y = Mathf.Clamp(_currentDirection.y, _povCameraConfig.ClampLeftRight.x, _povCameraConfig.ClampLeftRight.y);
                state.RawOrientation = Quaternion.Euler(-_currentDirection.y, _currentDirection.x, 0f);

                ResetDeltaInput();
            }
        }
    }
    #endregion

    #region Handler
    public void ResetCameraPosition()
    {
        _currentDirection = _povCameraConfig.StartingDirection;
    }
    #endregion
}
