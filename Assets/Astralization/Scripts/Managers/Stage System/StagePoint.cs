using System;
using UnityEngine;

[Serializable]
public class StagePointData
{
    public string PointName;
    public Vector3 Position;
    public float Radius;

    public StagePointData(string pointName, Vector3 position, float radius)
    {
        PointName = pointName;
        Position = position;
        Radius = radius;
    }
}

/*
 * A class that can keep data about a stage point
 */
public class StagePoint : MonoBehaviour
{
    #region Variables
    public string PointName;

    public float Radius;

    private StageBuilder _builder;
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

    #region MonoBehaviour
    private void Awake()
    {
        _builder = transform.GetComponentInParent<StageBuilder>();
    }
    #endregion

    #region SaveLoad
    public void Save()
    {
        if (_builder == null) _builder = GetComponentInParent<StageBuilder>();

        _builder.AddStagePoint(new StagePointData(PointName,transform.position,Radius));
    }
    public void Load(Vector3 pos, string pointName, float radius)
    {
        SetLocalPosition(pos);
        PointName = pointName;
        Radius = radius;
    }

    public void Display(StagePointData stagePointData)
    {
        PointName = stagePointData.PointName;
        Radius = stagePointData.Radius;
        transform.position = stagePointData.Position;
    }
    #endregion
}
