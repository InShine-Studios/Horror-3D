using System;
using UnityEngine;

[Serializable]
public struct RoomCoordinate
{
    public Vector3 coordinate;
    public string name;
    public float radius;
}

public class RoomPoint : MonoBehaviour
{
    public string pointName;

    public float radius;

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void Load(Vector3 pos, string pointName, float radius)
    {
        SetPosition(pos);
        this.pointName = pointName;
        this.radius = radius;
    }
}