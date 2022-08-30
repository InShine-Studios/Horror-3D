using System;
using UnityEngine;

[Serializable]
public class ClosetCameraSetting : MonoBehaviour
{
    [Header("Interactable Icons")]
    public float horizontalSpeed;
    public float verticalSpeed;
    [Tooltip("Limit for X rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 clampAngleX;
    [Tooltip("Limit for Y rotation, 2D Vector where X is min degree and Y is max degree")]
    public Vector2 clampAngleY;
    [Tooltip("3D Vector indicates the camera direction")]
    public Vector3 startingDirection;
    [Tooltip("3D Vector indicates the camera position when start hiding")]
    public Vector3 startingPosition;

    public ClosetCameraSetting(float horizontalSpeed, float verticalSpeed, Vector2 clampAngleX, Vector2 clampAngleY, Vector3 startingDirection, Vector3 startingPosition)
    {
        this.horizontalSpeed = horizontalSpeed;
        this.verticalSpeed = verticalSpeed;
        this.clampAngleX = clampAngleX;
        this.clampAngleY = clampAngleY;
        this.startingDirection = startingDirection;
        this.startingPosition = startingPosition;
    }
}
