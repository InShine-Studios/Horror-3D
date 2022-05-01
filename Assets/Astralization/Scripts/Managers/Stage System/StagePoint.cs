using UnityEngine;

/*
 * A class that can keep data about a stage point
 */
public class StagePoint : MonoBehaviour
{
    #region Variables
    public string PointName;

    public float Radius;
    #endregion

    #region SetGet
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public Vector3 GetLocalPosition()
    {
        return transform.localPosition;
    }
    #endregion

    #region Loader
    public void Load(Vector3 pos, string pointName, float radius)
    {
        SetLocalPosition(pos);
        PointName = pointName;
        Radius = radius;
    }
    #endregion
}
