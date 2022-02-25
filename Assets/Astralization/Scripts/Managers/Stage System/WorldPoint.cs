using UnityEngine;

/*
 * A class that can keep data about a world point
 */
public class WorldPoint : MonoBehaviour
{
    #region Variables
    public string PointName;

    public float Radius;
    #endregion

    #region SetGet
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
        this.PointName = pointName;
        this.Radius = radius;
    }
}
