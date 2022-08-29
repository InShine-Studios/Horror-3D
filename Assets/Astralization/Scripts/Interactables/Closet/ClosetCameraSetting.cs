using System;
using UnityEngine;

[Serializable]
public class ClosetCameraSetting : MonoBehaviour
{
    public float horizontalSpeed;
    public float verticalSpeed;
    public Vector2 clampAngleX;
    public Vector2 clampAngleY;
    public Vector3 startingDirection;
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
