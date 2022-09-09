using UnityEngine;
using Cinemachine;

/*
 * Class for process camera closets POV
 */
public class CinemachinePOVExtension : CinemachineExtension
{
    #region Variables
    private ClosetCameraSetting _closetCameraSetting;
    private Vector2 _deltaInput;
    #endregion

    #region SetGet
    public void SetMouseDeltaInput(Vector2 deltaInput)
    {
        _deltaInput = deltaInput;
    }

    public void SetClosetCameraSetting(ClosetCameraSetting closetCameraSetting)
    {
        _closetCameraSetting = closetCameraSetting;
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
                _closetCameraSetting.StartingDirection.x += _deltaInput.x * _closetCameraSetting.VerticalSpeed * Time.deltaTime;
                _closetCameraSetting.StartingDirection.y += _deltaInput.y * _closetCameraSetting.HorizontalSpeed * Time.deltaTime;
                _closetCameraSetting.StartingDirection.x = Mathf.Clamp(_closetCameraSetting.StartingDirection.x, _closetCameraSetting.ClampAngleX.x, _closetCameraSetting.ClampAngleX.y);
                _closetCameraSetting.StartingDirection.y = Mathf.Clamp(_closetCameraSetting.StartingDirection.y, _closetCameraSetting.ClampAngleY.x, _closetCameraSetting.ClampAngleY.y);
                state.RawOrientation = Quaternion.Euler(-_closetCameraSetting.StartingDirection.y, _closetCameraSetting.StartingDirection.x, 0f);
            }
        }
    }
    #endregion
}
