using System;
using UnityEngine;

/*
 * Class to keep value for camera
 */
[Serializable]
public class ClosetCameraSetting : MonoBehaviour
{
    #region Variables
    [Header("Interactable Icons")]
    public float HorizontalSpeed;
    public float VerticalSpeed;
    [Tooltip("Limit for X rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 ClampAngleX;
    [Tooltip("Limit for Y rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 ClampAngleY;
    [Tooltip("3D Vector indicates the camera direction")]
    public Vector3 StartingDirection;
    [Tooltip("3D Vector indicates the camera position when start hiding")]
    public Vector3 StartingPosition;
    #endregion

    #region Handler
    public ClosetCameraSetting(float horizontalSpeed, float verticalSpeed, Vector2 clampAngleX, Vector2 clampAngleY, Vector3 startingDirection, Vector3 startingPosition)
    {
        this.HorizontalSpeed = horizontalSpeed;
        this.VerticalSpeed = verticalSpeed;
        this.ClampAngleX = clampAngleX;
        this.ClampAngleY = clampAngleY;
        this.StartingDirection = startingDirection;
        this.StartingPosition = startingPosition;
    }
    #endregion
}
