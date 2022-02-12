using System;
using UnityEngine;


public class RoomPoint : MonoBehaviour
{
    public string pointName;

    public float radius;

    #region Set Get
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }

    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }
    #endregion

    public void Load(Vector3 pos, string pointName, float radius)
    {
        SetLocalPosition(pos);
        this.pointName = pointName;
        this.radius = radius;
    }
}