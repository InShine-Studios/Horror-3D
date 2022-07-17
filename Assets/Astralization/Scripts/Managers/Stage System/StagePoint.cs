using System;
using UnityEngine;

#region SerializableClass
[Serializable]
public class StagePointFieldValue
{
    public string PointName;
    public Vector3 LocalPosition;
    public float Radius;

    public StagePointFieldValue(string pointName, Vector3 position, float radius)
    {
        PointName = pointName;
        LocalPosition = position;
        Radius = radius;
    }
}
#endregion

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
    public Vector3 GetPosition()
    {
        return transform.position;
    }
    #endregion

    #region SaveLoad
    public void Load(Vector3 localPos, string pointName, float radius)
    {
        transform.localPosition = localPos;
        PointName = pointName;
        Radius = radius;
    }
    
    public void Load(StagePointFieldValue fieldValue)
    {
        transform.localPosition = fieldValue.LocalPosition;
        PointName = fieldValue.PointName;
        Radius = fieldValue.Radius;
    }
    #endregion
}
