using UnityEngine;
using Cinemachine;

[ExecuteInEditMode]
[SaveDuringPlay]
[AddComponentMenu("")]

public class CameraOffset : CinemachineExtension
{
    [Tooltip("Offset the camera's position by this much (camera space)")]
    public Vector3 Offset = Vector3.zero;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Aim)
        {
            state.PositionCorrection += state.FinalOrientation * Offset;
        }
    }
}
