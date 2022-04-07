using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransitionEndpoint
{
    public string AreaName;
    public Vector3 Position;
}

public class GhostTransitionZone : MonoBehaviour
{
    #region Variables
    public List<TransitionEndpoint> Endpoints { get; private set; }

    public TransitionEndpoint EnterPoint { get; private set; }
    public TransitionEndpoint ExitPoint { get; private set; }
    #endregion

    #region SetGet
    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public Vector3 GetZoneLocalPosition()
    {
        return transform.localPosition;
    }

    public void SetZoneLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public void SetEndpointPosition(Vector3 pos)
    {
        //transform.localPosition = pos; TODO
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        Endpoints = new List<TransitionEndpoint>();
        Transform[] endpoints = GetComponentsInChildren<Transform>();
        for (int i = 0; i < endpoints.Length; i++)
        {
            if (!endpoints[i].CompareTag("GhostTransitionZone"))
            {
                TransitionEndpoint newEndpoint = new TransitionEndpoint
                {
                    AreaName = endpoints[i].name,
                    Position = endpoints[i].position
                };
                Endpoints.Add(newEndpoint);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            int endpointIndex = GetEnteringPointIndex(other.transform.position);
            EnterPoint = Endpoints[endpointIndex];
            ExitPoint = Endpoints[Utils.MathCalcu.mod(endpointIndex + 1, 2)];
            //Debug.Log(EnterPoint.AreaName);
            //Debug.Log(ExitPoint.AreaName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            EnterPoint = null;
            ExitPoint = null;
        }
    }
    #endregion

    #region Loader
    public void Load(Vector3 pos, string pointName, float radius)
    {
        //SetLocalPosition(pos);
        //this.PointName = pointName;
        //this.Radius = radius;
    }
    #endregion

    #region DistanceCalculation
    private int GetEnteringPointIndex(Vector3 position)
    {
        bool isFirstEndpoint = Utils.GeometryCalcu.GetDistance3D(position, Endpoints[0].Position) <
            Utils.GeometryCalcu.GetDistance3D(position, Endpoints[1].Position);

        if (isFirstEndpoint) return 0;
        else return 1;
    }
    #endregion
}
