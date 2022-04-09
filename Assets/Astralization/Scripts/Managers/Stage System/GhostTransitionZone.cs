using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TransitionEndpointList
{
    public List<TransitionEndpoint> list;
}

[Serializable]
public class TransitionEndpoint
{
    public string AreaName;
    public Vector3 Position;
}

public class GhostTransitionZone : MonoBehaviour
{
    #region Variables
    public TransitionEndpointList Endpoints;

    public TransitionEndpoint EnterPoint { get; private set; }
    public TransitionEndpoint ExitPoint { get; private set; }
    #endregion

    #region SetGet
    public void SetZoneName()
    {
        string zoneName = "Zone ";
        for (int j = transform.childCount - 1; j >= 0; --j)
        {
            GameObject endpointGameObject = transform.GetChild(j).gameObject;
            endpointGameObject.name = GetComponent<GhostTransitionZone>().Endpoints.list[j].AreaName;
            zoneName += endpointGameObject.name;
            if (j > 0) zoneName += " - ";
        }
        name = zoneName;
    }
    public void SetZonePosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetZoneLocalPosition(Vector3 pos)
    {
        transform.localPosition = pos;
    }

    public Vector3 GetZoneLocalPosition()
    {
        return transform.localPosition;
    }

    public void SetZoneColliderCenter(Vector3 center)
    {
        GetComponent<BoxCollider>().center = center;
    }

    public Vector3 GetZoneColliderCenter()
    {
        return GetComponent<BoxCollider>().center;
    }

    public void SetZoneColliderSize(Vector3 size)
    {
        GetComponent<BoxCollider>().size = size;
    }

    public Vector3 GetZoneColliderSize()
    {
        return GetComponent<BoxCollider>().size;
    }

    public void SetEndpointPosition(string name, Vector3 pos)
    {
        Transform endpoint = transform.Find(name);
        endpoint.localPosition = pos;
    }
    #endregion

    #region MonoBehaviour
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ghost"))
        {
            int endpointIndex = GetEnteringPointIndex(other.transform.position);
            EnterPoint = Endpoints.list[endpointIndex];
            ExitPoint = Endpoints.list[Utils.MathCalcu.mod(endpointIndex + 1, 2)];
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
    public void Load(Vector3 zonePos, Vector3 zoneCenter, Vector3 zoneSize, List<TransitionEndpoint> endpoints)
    {
        SetZoneLocalPosition(zonePos);
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        boxCollider.center = zoneCenter;
        boxCollider.size = zoneSize;
        Endpoints.list = endpoints;

        for (int i = transform.childCount - 1; i >= 0; --i)
        {
            Transform endpoint = transform.GetChild(i);
            endpoint.name = endpoints[i].AreaName;
            endpoint.localPosition = endpoints[i].Position;
        }
    }
    #endregion

    #region DistanceCalculation
    private int GetEnteringPointIndex(Vector3 position)
    {
        int minDistanceIndex = -1;
        float minDistance = float.MaxValue;
        for (int i = Endpoints.list.Count; i >= 0; --i)
        {
            float currDistance = Utils.GeometryCalcu.GetDistance3D(position, Endpoints.list[0].Position);
            if (currDistance < minDistance)
            {
                minDistance = currDistance;
                minDistanceIndex = i;
            }
        }
        return minDistanceIndex;
    }
    #endregion
}
