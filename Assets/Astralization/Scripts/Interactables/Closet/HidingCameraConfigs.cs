using System;
using UnityEngine;

/*
 * Class for hiding camera configs
 */
[Serializable]
public class HidingCameraConfigs : MonoBehaviour
{
    #region Variables
    [Header("Camera Configs")]
    [Tooltip("Limit for X rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 ClampDownUp;
    [Tooltip("Limit for Y rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 ClampLeftRight;
    [Tooltip("3D Vector indicates the camera direction")]
    public Vector3 StartingDirection;
    [Tooltip("3D Vector indicates the camera position when start hiding")]
    public Vector3 StartingPosition;
    #endregion

    #region Constructor
    public HidingCameraConfigs(Vector2 clampDownUp, Vector2 clampLeftRight, Vector3 startingDirection, Vector3 startingPosition)
    {
        this.ClampDownUp = clampDownUp;
        this.ClampLeftRight = clampLeftRight;
        this.StartingDirection = startingDirection;
        this.StartingPosition = startingPosition;
    }
    #endregion
}
